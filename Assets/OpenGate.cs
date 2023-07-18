using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpenGate : MonoBehaviour
{

    

    public void OpenThatGate()
    {
        this.gameObject.transform.DOMoveY(6, 1);
    }




}
