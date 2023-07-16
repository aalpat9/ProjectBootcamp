using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewAI : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] waypoints;
    public int waypointIndex;
    Vector3 target;
    private bool caughtPlayer;
    


    [Header("Searching")]
    [SerializeField] private float searchInterval = 5;
    [SerializeField] private float searchTimer;
    [SerializeField] private float lookRadiusY = -90;

    [SerializeField] private float attackInterval = 2;
    [SerializeField] private float attackTimer;
    private Animator anim;


    public void DisableRobot()
    {
        enabled = false;
    }

    private void EnableRobot()
    {
        enabled = true;
    }


    public enum AIState { Patrolling, Chasing, Attacking, Searching, Dead }
    bool enabled = true;
    [SerializeField]public AIState currentState;

    private EnemyVision eVision;

    private void Start()
    {
        anim = GetComponent<Animator>();

        eVision = GetComponent<EnemyVision>();
        agent = GetComponent<NavMeshAgent>();

        //Makinayi baslat
        UpdateDestination();
        currentState = AIState.Patrolling;
    }

    private void Update()
    {
        if (enabled)
        {

            //Playeri gorurse
            if (eVision.detected == true && currentState != AIState.Attacking)
            {
                currentState = AIState.Chasing;
            }
            //else if (currentState != AIState.Patrolling)
            //{
            //    currentState = AIState.Searching;
            //}




            //State'e göre aksiyon uygular
            switch (currentState)
            {
                case AIState.Patrolling:
                    Patrolling();
                    break;
                case AIState.Chasing:
                    Chasing();
                    break;
                case AIState.Attacking:
                    Attack();
                    break;
                case AIState.Searching:
                    Search();
                    break;
                case AIState.Dead:
                    Die();
                    break;
                default:
                    break;
            }
        }

    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

    private void Attack()
    {
        if (attackTimer <= attackInterval)
        {
            attackTimer += Time.deltaTime;
        }
        else
        {
            anim.SetTrigger("onAttack");
            currentState = AIState.Patrolling;
            

        }
    }


    #region Searching

    private void Search()
    {
        if (searchTimer <= searchInterval)
        {
            float lerpValue = Mathf.LerpAngle(lookRadiusY, -lookRadiusY, searchTimer / searchInterval);
            gameObject.transform.rotation = Quaternion.Euler(0f, lerpValue, 0f);
            searchTimer += Time.deltaTime;
        }
        else
        {
            anim.Play("LookAround");
            currentState = AIState.Patrolling;

        }
    }

    #endregion

    #region Chasing

    private void Chasing()
    {

        target = eVision.lastSeenPos;
        agent.SetDestination(target);
        if (Vector3.Distance(transform.position, target) < 2)
        {
            if (Vector3.Distance(transform.position, eVision.player.transform.position) < 3)
            {

                attackTimer = 0;
                currentState = AIState.Attacking;
            }
            else
            {
                searchTimer = 0;
                currentState = AIState.Searching;
            }
        }
    }



    //Eger playeri yakalarsa burasi cagirilir
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            caughtPlayer = true;

            attackTimer = 0;
            currentState = AIState.Attacking;

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            caughtPlayer = false;
        }
    }

    #endregion

    #region Patrolling
    private void Patrolling()
    {

        //Eger waytpointe yakinsa indexi arttirir ve yenisini bulur
        if (Vector3.Distance(transform.position, target) < 2)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
    }

    private void UpdateDestination()
    {

        if (waypoints.Length < 1)
        {
            Debug.LogError(this.gameObject.name + "'in waypointleri assignlanmamis.");
        }
        //Animasyon oynat
        anim.SetTrigger("isWalking");

        //Sonraki waypointi bul
        target = waypoints[waypointIndex].position;

        //Oraya doðru ilerle
        agent.SetDestination(target);
    }

    private void IterateWaypointIndex()
    {
        //Waypoint indexi arttýrýr
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
    #endregion
}
