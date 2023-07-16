using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    public int health;


    public void Damaged(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Player is dead");
        }
    }


}
