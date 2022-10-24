using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OnStartScript : MonoBehaviour
{
    public CinemachineVirtualCameraBase CM_cam;
    [SerializeField] private GameObject timeline;
    [SerializeField] private GameObject canvas;
    [SerializeField] private AudioSource mainBackgroundSound;

    public void Start()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void StartGameNew()
    {
        Time.timeScale = 1f;
        CM_cam.m_Priority = 12;
        mainBackgroundSound.Pause();
        timeline.SetActive(true);
        canvas.SetActive(false);

    }

   
}
