using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{

    public List<Image> healthBars = new List<Image>();


    public void LoseHealth()
    {
        if (healthBars.Count > 0)
        {

            Destroy(healthBars[healthBars.Count - 1]);
            healthBars.Remove(healthBars[healthBars.Count - 1]);
        }
    }




}
