using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Grabbable
{



    public Axe()
    {
        throwCoefficient = 10f;
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
        playerController.playerAnim.SetTrigger("onChop");

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IDamagable>(out IDamagable hit))
        {
            if (hit.Type != IDamagable.DamagableType.WOOD)
            { return; }
            hit.Damage();
            LoseDurability(1);
        }

    }


}
