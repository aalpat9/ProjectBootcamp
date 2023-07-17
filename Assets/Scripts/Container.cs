using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Container : MonoBehaviour, IDamagable
{
    [SerializeField] GameObject healthBar;
    public int Health { get; set; }
    public IDamagable.DamagableType Type { get; set; }
    public IDamagable.DamagableType type;
    public int health;


    private void Start()
    {
        Health = health;
        Type = type;
        InitializeHealthBar();
        
    }

    public void Damage()
    {
        //Canýný azalt
        Health -= 1;

        //Vurulduðu zaman titreme ve kýzarma efekti
        gameObject.transform.DOShakePosition(0.3f, new Vector3(0.1f, 0, 0.1f), 25);
        gameObject.GetComponent<MeshRenderer>().material.DOColor(Color.red, 0.3f).From();


        if(Health <= 0)
        {
            
            Destroy(this.gameObject);
            SpawnItems();

            return;
        }

        healthBar.GetComponent<ObjectHealthBar>().LoseHealthPoint();

    }

    private void InitializeHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.GetComponent<ObjectHealthBar>().Initialize(Health);
        }
    }

    [SerializeField] private GameObject itemToSpawn;

    private void SpawnItems()
    {
        if (itemToSpawn != null)
        {
            Instantiate(itemToSpawn, this.transform.position, this.transform.rotation);
        }
       
    }

    

}
