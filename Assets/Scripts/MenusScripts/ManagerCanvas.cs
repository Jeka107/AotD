using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class ManagerCanvas : MonoBehaviour
{
    [SerializeField] private GameObject pauseGameLabel;
    [SerializeField] private GameObject controlScemehe;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject monster;
    [SerializeField] private GameObject timeline;
    [SerializeField] private GameObject gameoverCanvas;
    [SerializeField] public GamePlayCanvas canvas;

    [Header("Inventory")]
    [SerializeField] private GameObject inventoryBackground;
    [SerializeField] private InventoryUI inventoryUI;

    [Header("NoteBook")]
    [SerializeField] private GameObject notebookContent;

    private StarterAssets.FirstPersonController controller;
    /*private bool gameIsPaused = false;*/

    public void Start()
    {
        controller = player.GetComponent<StarterAssets.FirstPersonController>();
        controller.enabled = true;
        Time.timeScale = 1f;
    }
    public void Pause()//Pause game , turn on pauseLabel
    {
        controller.enabled = false;
        pauseGameLabel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0f;
    }

    public void Resume()//Resume game , turn off PauseLabel
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        controller.enabled = true;
        player.GetComponent<PlayerActions>().gameIsPaused = false;
        pauseGameLabel.SetActive(false);
        controlScemehe.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestratGame()//restart Scene/Level
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Invoke("RestartCommand", 1f);
       
    }
    public void CheckpointButton()//restart Scene/Level
    {
        if (player.GetComponent<CheckpointScript>().firstFriend)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            controller.enabled = true;
            pauseGameLabel.SetActive(false);
            controlScemehe.SetActive(false);
            canvas.TextOff();

            monster.GetComponent<Animator>().SetBool("monsterAttack", false);
            monster.GetComponent<MonsterAI>().ResetPlayableDirector();

            Time.timeScale = 1f;
            monster.SetActive(false);
            Invoke("MonsterWait", 5);
            player.transform.position = player.GetComponent<CheckpointScript>().lastCheckpoint;
           player.GetComponent<PlayerActions>().gameIsPaused = false;
        }
    }
    public void QuitGame()
    {
        Debug.Log("Quiting...");
        Application.Quit();
    }

    public void NotebookStatus(bool status)//open or close notebook
    {
        notebookContent.SetActive(status);

        if (status)
        {
            Time.timeScale = 0f;
            controller.enabled = false;
            player.GetComponent<PlayerActions>().gameIsPaused = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;   
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            player.GetComponent<PlayerActions>().gameIsPaused = false;
            controller.enabled = true;
            Time.timeScale = 1f;
        }
    }

    public void RestartCommand()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        controller.enabled = true;
    }
    private void MonsterWait()
    {
        monster.SetActive(true);
    }
}
    
