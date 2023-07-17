using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Act7Manager : MonoBehaviour
{
    public int deadEnemies = 0;
    
    bool finished = false;
    private GameEvent onDialogue;

    public void incrementDeadEnemies()
    {
        deadEnemies += 1;
        if (deadEnemies >= 7)
        {
            Debug.Log("All dead");
        }
    }

   

}
