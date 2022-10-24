using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;

    public void StartGame(int sceneIndex)
    {

        SceneManager.LoadScene("Cutscene1");
        loadingScreen.SetActive(true);

    }
    /*StartCoroutine(LoadAsynchronously(sceneIndex));

}
IEnumerator LoadAsynchronously(int sceneIndex)
{
    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

    while (!operation.isDone)
    {
        float progress = Mathf.Clamp01(operation.progress / .9f);
        loadingBar.value = progress;
    }
    yield return null;
}
}*/
    public void ExitGame()
    {
        Application.Quit();
    }
}
