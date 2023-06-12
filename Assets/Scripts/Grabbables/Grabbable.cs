using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{

    public int maxDurability;
    public int currentDurability;
    public bool isBroken = false;
    //Bunu buraya yazmadan yapman�n yolunu bulabilir miyim acaba
    public PlayerController playerController;

    private void Start()
    {
        currentDurability = maxDurability;
    }

    public virtual void Use()
    {
       
    }

    public void CheckIfBroken()
    {
        if (currentDurability <= 0)
        {

            isBroken = true;
        }
    }

}