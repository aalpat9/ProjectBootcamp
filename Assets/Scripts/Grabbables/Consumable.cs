using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Grabbable
{

    public enum ConsumableType { Health, Energy };
    public ConsumableType type;
    public bool isDrink;
    public float amount = 1;

    public Consumable()
    {
        throwCoefficient = 30f;
    }

    public override void Use()
    {
        

        //Consumable t�r�ne g�re anim oynat ve sonradan can/enerji ver
        switch (type)
        {
            case ConsumableType.Health:
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUnit>().AddHealth(1);
                if (isDrink)
                {
                    base.playerController.playerAnim.SetTrigger("onDrink");
                }
                else
                {
                    base.playerController.playerAnim.SetTrigger("onEat");
                }
                break;
            case ConsumableType.Energy:
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUnit>().AddHealth(1);
                if (isDrink)
                {
                    base.playerController.playerAnim.SetTrigger("onDrink");
                }
                else
                {
                    base.playerController.playerAnim.SetTrigger("onEat");
                }
                break;
            default:
                break;
        }

        //Durability azalt
        LoseDurability(1);

      
        Debug.Log("restored " + amount + " health.");
        Debug.Log(base.currentDurability + "/" + base.maxDurability);
    }





}
