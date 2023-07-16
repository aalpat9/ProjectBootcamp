
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakEnemy : MonoBehaviour
{

    public GameObject[] enemyParts;


    public float breakForce = 10f;
    public float breakTorque = 10f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
           BreakEnemyPieces();
        }

    }

    public void BreakEnemyPieces()
    {
        foreach (GameObject part in enemyParts)
        {
            Rigidbody rb = part.GetComponent<Rigidbody>();

            rb.isKinematic = false;
            rb.AddForce(Vector3.up * breakForce, ForceMode.Impulse);
            rb.AddTorque(Random.insideUnitSphere * breakTorque, ForceMode.Impulse);
        }
    }
}




