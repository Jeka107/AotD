using TMPro;
using UnityEngine;

public class SafeCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI codeText;
    string codeTextValue = "";
    public PlayerActions playerActions;
    public GameObject safeCanvas;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource correctSound = null;
    [SerializeField] private AudioSource wrongSound = null;
    private void Awake()
    {
        player.GetComponent<PlayerActions>().gameIsPaused = false;
    }
    private void Update()
    {
        codeText.text = codeTextValue;
        if (codeTextValue == "1115")
        {
            playerActions.hitObject.GetComponent<LockedSafeScript>().isSafeOpen = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            player.GetComponent<StarterAssets.FirstPersonController>().enabled = true;
            player.GetComponent<StarterAssets.StarterAssetsInputs>().enabled = true;
            player.GetComponent<PlayerActions>().flashlightHand.SetActive(true);
            player.GetComponent<PlayerActions>().enabled = true;
            player.GetComponent<PlayerActions>().gameIsPaused = false;
            Time.timeScale = 1f;
            safeCanvas.SetActive(false);
            correctSound.Play();
        }
        if (codeTextValue.Length >= 4&& codeTextValue != "1115")
        {
            codeTextValue = "";
                wrongSound.Play();    
        
        }
    }
    public void AddDigit(string digit)
    {
        codeTextValue += digit;
    }
    public void closeSafeCanvas()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        player.GetComponent<StarterAssets.FirstPersonController>().enabled = true;
        player.GetComponent<StarterAssets.StarterAssetsInputs>().enabled = true;
        player.GetComponent<PlayerActions>().flashlightHand.SetActive(true);
        player.GetComponent<PlayerActions>().enabled = true;
        player.GetComponent<PlayerActions>().gameIsPaused = false;
        safeCanvas.SetActive(false);
       
    }

}
