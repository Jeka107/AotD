using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class SkipCutsceneScript : MonoBehaviour
{
    [SerializeField] public PlayableDirector playableDirector;
    public void Skip()
    {
        SceneManager.LoadScene("MainLevel");
        playableDirector.Stop();
        playableDirector.time = 0;
        playableDirector.Evaluate();

    }
}
