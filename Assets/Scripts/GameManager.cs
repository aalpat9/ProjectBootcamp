using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton instance

    public static GameManager instance;
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

    #endregion



    public List<InteractCondition> interactConditions = new List<InteractCondition>();

    private void Start()
    {
        //Oyundaki g�revleri s�f�rlar (Sebebi ise scriptable objectler her instancede ayn� kal�yor)
        FindInteractConditions();
        ResetConditions();
    }

    private void FindInteractConditions()
    {

        InteractCondition[] foundInteractConditions = FindObjectsOfType<InteractCondition>();

        interactConditions.Clear();
        interactConditions.AddRange(foundInteractConditions);
    }

    private void ResetConditions()
    {
        foreach (InteractCondition interactCondition in interactConditions)
        {
            interactCondition.ResetAllConditions();
        }
    }

}




