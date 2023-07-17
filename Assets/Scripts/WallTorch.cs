using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTorch : MonoBehaviour, IInteractable
{
    [SerializeField] private Light light;
    private AudioSource audioSource;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Interact(int id)
    {

        light.gameObject.SetActive(true);
        audioSource.Play();
    }

   


}
