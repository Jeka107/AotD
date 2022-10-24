using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAfterCutscene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        SceneManager.LoadScene("MainLevel");
    }

    
}
