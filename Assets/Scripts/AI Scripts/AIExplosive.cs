using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIExplosive : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player"){
            StartCoroutine(ExplosionDelay());
            // PATLAMA EFEKTI
            // PATLAMA SESI
        }
    }

    IEnumerator ExplosionDelay(){
        yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);
    }
}
