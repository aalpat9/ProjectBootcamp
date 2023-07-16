using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class AIAgent : MonoBehaviour, IDamagable
{
    // AI fields
    public AIStateMachine stateMachine;
    [SerializeField] public AiStateId initialState;
    [SerializeField] public AiStateId chaseState;
    public Transform[] points;
    public NavMeshAgent navMeshAgent;
    public AISensor sensor;

    // Damageable fields
    [SerializeField]
    public int Health { get; set; }
    public IDamagable.DamagableType Type { get; set; }
    public IDamagable.DamagableType type;
    public int health;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        sensor = GetComponent<AISensor>();
        stateMachine = new AIStateMachine(this);
        stateMachine.RegisterState(new AIChasePlayerState());
        stateMachine.RegisterState(new AIPatrolState());
        stateMachine.RegisterState(new AIDeathState());
        stateMachine.ChangeState(initialState);
    }

    void Update()
    {
        stateMachine.Update();
    }

    public void Damage()
    {
        //Can�n� azalt
        Health -= 1;

        //Vuruldu�u zaman titreme ve k�zarma efekti
        gameObject.transform.DOShakePosition(0.3f, new Vector3(0.1f, 0, 0.1f), 25);
        gameObject.GetComponent<MeshRenderer>().material.DOColor(Color.red, 0.3f).From();


        if(Health <= 0)
        {
            
            return;
        }

        // healthBar.GetComponent<ObjectHealthBar>().LoseHealthPoint();
    }

    [Header("Body Parts")]
    [SerializeField] Rigidbody headRb;
    [SerializeField] MeshCollider headCollider;
    [SerializeField] Rigidbody bodyRb;
    [SerializeField] MeshCollider bodyCollider;
    [SerializeField] Rigidbody handsRb;
    [SerializeField] MeshCollider handsCollider;
    [SerializeField] Rigidbody legsRb;
    [SerializeField] MeshCollider legsCollider;
    [SerializeField] Rigidbody feetRb;
    [SerializeField] MeshCollider feetCollider;

    private void Die()
    {

    }

}
