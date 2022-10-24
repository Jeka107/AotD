using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Audio;

public class OptionsButton : MonoBehaviour
{
    public CinemachineVirtualCamera optionsCamera;
    public CinemachineVirtualCamera mainCamera;
    public GameObject backButton;
    public GameObject startButton;
    public GameObject optionsButton;
    public GameObject exitButton;
    public GameObject volumeSlider;
    public AudioMixer mixer;

    //[SerializeField] private int priorityNew = 10;
    //[SerializeField] private int prioritySec = 11;
 
    public void Options()
    {
        optionsCamera.Priority = 11 ;
        mainCamera.Priority = 10;

        backButton.SetActive(true);

        startButton.SetActive(false);
        optionsButton.SetActive(false);
        exitButton.SetActive(false);
        StartCoroutine(waitForButtons());
    }
    public void Back()
    {
        optionsCamera.Priority = 10;
        mainCamera.Priority = 11;

        backButton.SetActive(false);
        volumeSlider.SetActive(false);
        startButton.SetActive(true);
        optionsButton.SetActive(true);
        exitButton.SetActive(true);
    }
    public void setVolume(float volume)
    {
        mixer.SetFloat("volume", volume);
    }
    IEnumerator waitForButtons()
    {
        yield return new WaitForSecondsRealtime(2f);
        volumeSlider.SetActive(true);
        
    }
}
