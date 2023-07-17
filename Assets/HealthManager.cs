using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    PlayerUnit player;
    [SerializeField] GameObject healthPointPrefab;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUnit>();
        SpawnHealthPoints();
    }

    private void SpawnHealthPoints()
    {
        for (int i = 0; i < player.health; i++)
        {
            Instantiate(healthPointPrefab, transform);
        }
    }
}
