using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamagable
{

    public string name;
    public int health;
    public int Health { get; set; }
    public IDamagable.DamagableType Type { get; set; }

    

    [SerializeField] private Unit[] otherUnits;

    [SerializeField] private BossHealthBar healthBar;

    [SerializeField] private NewAI newAI;

    private void Awake()
    {
        Health = 8;
        Type = IDamagable.DamagableType.ENEMY;

    }


    public void Damage()
    {

        health -= 1;
        GetComponent<Animator>().Play("GetHit");
        healthBar.LoseHealth();
        if (health <= 0)
        {
            Invoke("Die", 2);
            GetComponent<Animator>().Play("Die");
            

        }

    }

    [SerializeField] GameObject director2;
    void Die()
    {
        newAI.Die();
        director2.SetActive(true);
    }




}
