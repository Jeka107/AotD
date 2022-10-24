using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene("Cutscene1");    
    }

}
