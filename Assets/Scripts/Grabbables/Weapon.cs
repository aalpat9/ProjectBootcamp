using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Grabbable
{

    private AudioSource audioSource;

    public Weapon()
    {
        throwCoefficient = 10f;
    }
    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public override void OnGrabbed()
    {
        base.OnGrabbed();
    }

    public override void OnDropped()
    {
        base.OnDropped();
    }

    public override void Use()
    {
        playerController.playerAnim.SetTrigger("onAttack");
        audioSource.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IDamagable>(out IDamagable hit))
        {
            
            hit.Damage();
            LoseDurability(1);
        }

       

    }


}
