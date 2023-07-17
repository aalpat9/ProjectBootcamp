using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DropArea : MonoBehaviour
{
    [SerializeField] private string itemName;
    public Condition condition;
    public UnityEvent onSuccesful;
    [SerializeField] private bool disappearAfterOutOfView;
    [SerializeField] private GameObject player;
    [SerializeField] private float disappearDistance = 15;
    private GameObject _droppedItem;
    public UnityEvent onDisappear;
    private bool isDropped;
    [SerializeField] private GameObject bone;
    private AudioSource audioSource;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Grabbable>(out Grabbable droppedItem))
        {
   
            if (droppedItem.itemName == itemName)
            {
                _droppedItem = droppedItem.gameObject;
                Debug.Log("Feeding succesful");
                condition.isMet = true;
                onSuccesful.Invoke();
                isDropped = true;
            }

        }
    }

    private void Update()
    {
        if (disappearAfterOutOfView && isDropped)
        {
            if (Vector3.Distance(this.transform.position, player.transform.position) > disappearDistance)
            {
                onDisappear.Invoke();
                disappearAfterOutOfView = false;
                Vector3 meatPos = _droppedItem.transform.position;
                Destroy(_droppedItem);
                Instantiate(bone, meatPos, Quaternion.identity);
                audioSource.Play();
            }
        }
    }




}
