using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceCanvasScript : MonoBehaviour
{
    [SerializeField] private NotebookSystem notebook;
    [SerializeField] private GameObject blcok;
    private void Awake()
    {
        if (notebook.tapes.Count == 3 && notebook.notes.Count == 3)
        {
            blcok.SetActive(false);
        }
    }
    public void NormalENding()
    {
        SceneManager.LoadScene("NormalEnding");
    }

    public void RealEnding()
    {
        SceneManager.LoadScene("RealEnding");
    }
}
