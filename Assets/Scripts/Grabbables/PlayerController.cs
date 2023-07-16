using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(LineRenderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform rightHand;

    [Tooltip("F�rlatma ve yere b�rakmay� ay�ran delay s�resi")]
    [SerializeField] private float delayForThrowing = 0.3f;
    private float holdStartTime = 0;
    public Animator playerAnim;
    private Rigidbody _rb;
    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        trajectoryLine = GetComponent<LineRenderer>();
    }

    float useDelay;


    private void Update()
    {
        useDelay -= Time.deltaTime;
        NewHandleThrowing();

        HandleInteraction();
        HandleItemPickup();
        HandleItemUsage();

    }

    private void HandleInteraction()
    {
        if (useDelay <= 0)
        {

            if (interactablesInRadius.Count > 0 && rightHand.childCount <= 0)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    FindNearestInteractable().GetComponent<IInteractable>().Interact();
                    useDelay = 1;
                }

            }
        }
    }

    //private void HandleThrowing()
    //{
    //    if (useDelay <= 0)
    //    {
    //        if (rightHand.childCount > 0)
    //        {
    //            if (Input.GetKeyDown(KeyCode.Space))
    //            {
    //                holdStartTime = Time.time;
    //                playerAnim.SetBool("isThrowing", true);
    //                Debug.Log("being pressed");
    //            }

    //            if (Input.GetKeyUp(KeyCode.Space))
    //            {
    //                float passedTime = Time.time - holdStartTime;

    //                if (passedTime < delayForThrowing)
    //                {
    //                    playerAnim.SetBool("isThrowing", false);
    //                    Drop();
    //                }
    //                else if (passedTime > delayForThrowing)
    //                {
    //                    playerAnim.SetBool("isThrowing", false);
    //                    if (passedTime > 1f) passedTime = 1f;
    //                    Throw(passedTime);
    //                }
    //            }
    //        }
    //    }
    //}

    private bool isThrowing;
    float throwDuration;
    float maxThrowDuration = 1;
    Vector3 throwDirection;
    [SerializeField] LineRenderer trajectoryLine;
    float throwForce = 10f;

    private void NewHandleThrowing()
    {
        if (useDelay <= 0)
        {
            if (rightHand.childCount > 0)
            {
                // Check for throw input
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartThrow();
                }
                else if (Input.GetKey(KeyCode.Space))
                {
                    ContinueThrow();
                }
                else if (Input.GetKeyUp(KeyCode.Space))
                {
                    EndThrow();
                }
            }
        }
        if (isThrowing && trajectoryLine.enabled)
        {
            UpdateTrajectoryLinePositions();
        }
    }

    private void UpdateTrajectoryLinePositions()
    {
        // Update the starting position of the trajectory line
        trajectoryLine.SetPosition(0, rightHand.position);

        // Update the ending position of the trajectory line based on the current throw distance
        float throwDistance = throwDuration / maxThrowDuration * throwForce;
        Vector3 lineEndPos = rightHand.position + throwDirection * throwDistance;
        trajectoryLine.SetPosition(0, lineEndPos);
    }

    private void StartThrow()
    {

        isThrowing = true;
        throwDuration = 0;
        throwDirection = transform.forward;

        trajectoryLine.enabled = true;
        trajectoryLine.positionCount = 1;
        UpdateTrajectoryLinePositions();
    }

    private void ContinueThrow()
    {
        if (isThrowing)
        {
            throwDuration += Time.deltaTime;
            float normalizedDuration = Mathf.Clamp01(throwDuration / maxThrowDuration);
            float throwDistance = normalizedDuration * throwForce;

            // Update the trajectory line position
            Vector3 lineEndPosition = transform.position + throwDirection * throwDistance;
            trajectoryLine.positionCount = 2;
            trajectoryLine.SetPosition(1, lineEndPosition);
        }
    }
    private void EndThrow()
    {
        if (isThrowing)
        {
            isThrowing = false;

            // Disable the trajectory line
            trajectoryLine.enabled = false;

            // Get the object to throw
            Grabbable objectToThrow = rightHand.GetChild(0).GetComponent<Grabbable>();
            Rigidbody throwableRb = objectToThrow.GetComponent<Rigidbody>();

            //OnDropped'i çağırır
            rightHand.GetChild(0).GetComponent<Grabbable>().OnDropped();

            //PlayerControlleri objeden al�r
            rightHand.GetChild(0).GetComponent<Grabbable>().playerController = null;

            //Elin childini birakir
            var child = rightHand.GetChild(0);
            child.transform.SetParent(null);
            EnableDisablePhysics(child.gameObject, false);


            // Throw animation trigger
            playerAnim.SetTrigger("onThrow");

            // Set force to add (direction and magnitude)
            throwableRb.AddForce(throwDirection * throwForce, ForceMode.Impulse);




        }
    }



    private void HandleItemUsage()
    {
        if (useDelay <= 0)
        {

            if (rightHand.childCount > 0)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    useDelay = 1;
                    rightHand.GetChild(0).GetComponent<Grabbable>().Use();
                }
            }
        }
    }

    private void HandleItemPickup()
    {
        if (objectsInRadius.Count > 0 && rightHand.childCount == 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                useDelay = 1;
                PickUp(FindNearestItem());
            }
        }
    }


    #region Trigger K�sm�

    [SerializeField] public List<GameObject> objectsInRadius = new List<GameObject>();
    [SerializeField] public List<GameObject> interactablesInRadius = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<Grabbable>(out Grabbable triggerObject))
        {

            objectsInRadius.Add(other.gameObject);


        }
        if (other.TryGetComponent<IInteractable>(out IInteractable interactableObject))
        {
            interactablesInRadius.Add(other.gameObject);
        }


    }


    private GameObject FindNearestItem()
    {

        GameObject nearestItem = null;
        float nearestDistance = Mathf.Infinity;

        foreach (GameObject go in objectsInRadius)
        {

            float distance = Vector3.Distance(transform.position, go.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestItem = go;
            }

        }

        return nearestItem;

    }
    public GameObject FindNearestInteractable()
    {

        GameObject nearestInteractable = null;
        float nearestDistance = Mathf.Infinity;

        foreach (GameObject go in interactablesInRadius)
        {

            float distance = Vector3.Distance(transform.position, go.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestInteractable = go;
            }

        }

        return nearestInteractable;

    }

    private void OnTriggerExit(Collider other)
    {

        if (objectsInRadius.Contains(other.gameObject))
        {

            objectsInRadius.Remove(other.gameObject);
        }
        if (interactablesInRadius.Contains(other.gameObject))
        {
            interactablesInRadius.Remove(other.gameObject);
        }
    }

    #endregion
    private void PickUp(GameObject pickedObject)
    {


        //PlayerControlleri objeye ge�irir
        pickedObject.GetComponent<Grabbable>().playerController = this;

        //OnGrabbed'i cagirir
        pickedObject.GetComponent<Grabbable>().OnGrabbed();


        //Pick up animation trigger
        playerAnim.SetTrigger("onPickup");

        //Elin childi yapar
        pickedObject.transform.SetParent(rightHand);
        pickedObject.transform.localPosition = Vector3.zero;
        pickedObject.transform.localRotation = Quaternion.identity;

        //Rigid body ve colllider
        EnableDisablePhysics(pickedObject, true);
    }

    public void Drop()
    {
        //OnDropped'i cagirir
        rightHand.GetChild(0).GetComponent<Grabbable>().OnDropped();

        //PlayerControlleri objeden al�r
        rightHand.GetChild(0).GetComponent<Grabbable>().playerController = null;



        //Elin childini birakir
        var child = rightHand.GetChild(0);
        child.transform.SetParent(null);

        EnableDisablePhysics(child.gameObject, false);

    }

    private void EnableDisablePhysics(GameObject pickedObject, bool toggle)
    {

        pickedObject.GetComponent<Rigidbody>().isKinematic = toggle;

        if (pickedObject.GetComponent<MeshCollider>() != null)
        {
            pickedObject.GetComponent<MeshCollider>().enabled = !toggle;
        }
        else
        {
            pickedObject.GetComponentInChildren<MeshCollider>().enabled = !toggle;
        }
    }




    public void AttackEnter()
    {
        if (rightHand.GetChild(0) != null)
        {
            rightHand.GetChild(0).GetComponentInChildren<MeshCollider>().enabled = true;
        }


    }

    public void AttackExit()
    {
        if (rightHand.childCount > 0)
        {
            rightHand.GetChild(0).GetComponentInChildren<MeshCollider>().enabled = false;
        }
    }

}

