using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsteractivator : MonoBehaviour
{
    public GameObject boss;
    public GameObject Ui;

    public void ActivateMonster()
    {
        boss.SetActive(true);
    }

    public void ActivateUI()
    {
        Ui.SetActive(true);
    }

}
