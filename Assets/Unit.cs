using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IDamagable
{
    public string name;
    public int health;
    public int Health { get; set; }
    public IDamagable.DamagableType Type { get; set; }

    [SerializeField] private NewAI newAI;

    private void Awake()
    {
        Health = health;
        Type = IDamagable.DamagableType.ENEMY;
        newAI = GetComponent<NewAI>();
    }


    public void Damage()
    {
        health -= 1;
        if (health <= 0)
        {
            newAI.currentState = NewAI.AIState.Dead;
        }

    }
}
