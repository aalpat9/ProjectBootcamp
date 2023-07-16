using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour, IInteractable
{

    [TextArea(5, 5)]
    [SerializeField] string dialogue;
    public GameEvent onDialogue;
    [SerializeField] int doorId;

    public void Interact(int id)
    {
        if (id == doorId)
        {
            OpenDoor();
        }
        else
        {
            onDialogue.Raise(this, dialogue);
        }
        
    }


    private void OpenDoor()
    {
        this.gameObject.GetComponent<MeshCollider>().enabled = false;
        this.gameObject.transform.DORotate(new Vector3(0, 180, 0), 1);
        //ses burda oynar
    }




}
