using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    public int health;
    public int maxHealth = 5;

    public void Damaged(int damage)
    {
        health -= damage;

        HealthManager.instance.UpgradeHealthPoints();

        if (health <= 0)
        {
            GameManager.instance.ResetLevel();
        }
    }

    public void AddHealth(int amount)
    {
        health += amount;

        if (health >= maxHealth)
        {
            health = maxHealth;
        }

        HealthManager.instance.UpgradeHealthPoints();
    }

}
