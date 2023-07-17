using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Grabbable : MonoBehaviour
{
    public Sprite icon;
    public string itemName;

    public int maxDurability;
    public int currentDurability;
    //Bunu buraya yazmadan yapmanin yolunu bulabilir miyim acaba
    public PlayerController playerController;

    //Events
    [Header("Events")]
    public GameEvent onItemGrabbed;
    public GameEvent onItemDropped;
    public GameEvent onDurabilityLost;

    [Tooltip("Fırlatma katsayısı")]
    [SerializeField] public float throwCoefficient;

    private void Start()
    {
        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        if(sphereCollider == null)
        {
            sphereCollider = GetComponentInChildren<SphereCollider>();
        }
        try
        {
            sphereCollider.isTrigger = true;
            sphereCollider.radius = 0.75f;
        }
        catch (System.Exception)
        {
            Debug.Log("No sphere collider detected");
            throw;
        }
        currentDurability = maxDurability;
    }

    public virtual void OnGrabbed()
    {
        //Objeyi atesle
        onItemGrabbed.Raise(this, this);
      
    }

    public virtual void Use()
    {
       
    }

    public virtual void OnDropped()
    {
        onItemDropped.Raise(null, null);
    }

    public void LoseDurability(int amount)
    {
        currentDurability -= amount;
        onDurabilityLost.Raise(null, amount);
        if (CheckIfBroken())
        {
            if (this.gameObject == null)
            {
                return;
            }
            playerController.objectsInRadius.Remove(this.gameObject);
            playerController.Drop();
            Destroy(this.gameObject);
        }
    }

    public bool CheckIfBroken()
    {
        if (currentDurability <= 0)
        {
            return true;

        }
        return false;
    }

}