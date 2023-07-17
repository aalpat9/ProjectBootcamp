using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            DontDestroyOnLoad(this.gameObject);
        }
    }

    #endregion

    public void TransiteScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public List<InteractCondition> interactConditions = new List<InteractCondition>();

    private void Start()
    {
        //Oyundaki görevleri sýfýrlar (Sebebi ise scriptable objectler her instancede ayný kalýyor)
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

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

   
}




