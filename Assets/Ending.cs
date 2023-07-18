using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{

    [SerializeField] Image backing;

    [SerializeField] TMP_Text theEnd;
    [SerializeField] TMP_Text atLeast;

    [SerializeField] Color colora;

    private void Start()
    {
        ShowEnding();
    }

    private void ShowEnding()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(5)
            .Append(backing.DOColor(Color.black, 1))
            .AppendCallback(() => { theEnd.gameObject.SetActive(true); })
            .Append(backing.DOColor(colora, 2))
            .AppendInterval(2)
            .AppendCallback(() => { atLeast.gameObject.SetActive(true); })
            .AppendInterval(2)
            .AppendCallback(() => { SceneManager.LoadScene("menu"); });
            
        


    }

}
