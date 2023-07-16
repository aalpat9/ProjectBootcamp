using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    }

    float useDelay;
    

    private void Update()
    {
        useDelay -= Time.deltaTime;
        HandleThrowing();

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

    private void HandleThrowing()
    {
        if (useDelay <= 0)
        {
            if (rightHand.childCount > 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    holdStartTime = Time.time;
                    playerAnim.SetBool("isThrowing", true);
                    Debug.Log("being pressed");
                }

                if (Input.GetKeyUp(KeyCode.Space))
                {
                    float passedTime = Time.time - holdStartTime;
                   
                    if (passedTime < delayForThrowing)
                    {
                        playerAnim.SetBool("isThrowing", false);
                        Drop();
                    }
                    else if (passedTime > delayForThrowing)
                    {
                        playerAnim.SetBool("isThrowing", false);
                        if (passedTime > 1f) passedTime = 1f;
                        Throw(passedTime);
                    }
                }
            }
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

    private void Throw(float throwForce)
    {
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
        Vector3 forceToAdd = _rb.transform.forward * throwForce * objectToThrow.throwCoefficient;

        throwableRb.AddForce(forceToAdd, ForceMode.Impulse);


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

