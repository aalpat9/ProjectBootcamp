using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class InteractableObject2 : MonoBehaviour, IInteractable
{
    [TextArea(5, 5)]
    [SerializeField] string dialogue;
    public GameEvent onDialogue;
    [Tooltip("Bu seye interact etmek icin belirli kosullari tamamlamak gerekiyorsa koyun")]
    public InteractCondition interactCondition;
    public UnityEvent onConditionsMet;
    [Tooltip("Bu seye interact etmek bir gorevi tamamlayacaksa bunu koyun")]
    public Condition condition;

    public void Interact(int id)
    {
      
        onDialogue.Raise(this, dialogue);
        onConditionsMet.Invoke();
       
    }





}