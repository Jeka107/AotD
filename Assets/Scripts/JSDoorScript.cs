using System.Collections;
using UnityEngine;
using Cinemachine;

public class JSDoorScript : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] public GameObject firstBlocker;
    [SerializeField] private GhoulMonster monster;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerRoot;
    [SerializeField] private GameObject doorShutCollider;
    [SerializeField] private GameObject mockCollider;
    [SerializeField] private GameObject alexis;
    [SerializeField] public GameObject alexisTimeline;

    [Header("Cameras")]
    [SerializeField] private CinemachineVirtualCamera monsterCamera;

    [Header("Wait Time")]
    [SerializeField] private float waitToScream;
    [SerializeField] private float waitToAttack;
    [SerializeField] private float waitToFade;
    [SerializeField] private float waitToButton;
    [Header("Audio")]
    [SerializeField] private AudioSource monsterScream;


    [Header("Light")]
    [SerializeField] private GameObject light;

    [HideInInspector] public bool jsReady = false;
    [HideInInspector] public bool happened = false;

    [Header("Canvas")]

    [SerializeField] public GameObject fadeCanvas;
    [SerializeField] public GameObject menuButton;

   
    private void Update()
    {
        if (jsReady == true)
        {
            /*GetComponent<MeshCollider>().enabled = true;*/

            if (GetComponent<Animator>().GetBool("openDoor") == true)
            {
                if (happened == false)
                {
                    happened = true;
                    firstBlocker.SetActive(false);
                    alexisTimeline.SetActive(true);
                    alexis.GetComponent<Animator>().SetBool("run", true);
                 /*   player.GetComponent<StarterAssets.FirstPersonController>().enabled = false;
                    
                    StartCoroutine(MonsterAttack());*/
                }

            }
        }
        if (alexis.GetComponent<FreeAlexisScript>().alexisIsDown == true)
        {
            GetComponent<MeshCollider>().enabled = true;
            mockCollider.SetActive(false);
        }
    }
    IEnumerator MonsterAttack()
    {
        yield return new WaitForSeconds(waitToScream);
        light.SetActive(true);
        monsterCamera.enabled = true;
        monsterCamera.m_Priority = 11;
        monster.PlayScream();
        yield return new WaitForSeconds(waitToAttack);
        monster.GetComponent<Animator>().SetBool("monsterAttack", true);
        yield return new WaitForSeconds(waitToFade);
        fadeCanvas.SetActive(true);
        yield return new WaitForSeconds(waitToButton);
        menuButton.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
