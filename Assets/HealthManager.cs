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

    public static HealthManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public List<GameObject> healthPoints = new List<GameObject>();
    private void SpawnHealthPoints()
    {
        for (int i = 0; i < player.health; i++)
        {
            GameObject healthPoint = Instantiate(healthPointPrefab, transform);
            healthPoints.Add(healthPoint);
        }
    }

    public void UpgradeHealthPoints()
    {
        foreach (GameObject hp in healthPoints)
        {
            Destroy(hp);
        }
        for (int i = 0; i < player.health; i++)
        {
            GameObject healthPoint = Instantiate(healthPointPrefab, transform);
            healthPoints.Add(healthPoint);
        }

    }


}
