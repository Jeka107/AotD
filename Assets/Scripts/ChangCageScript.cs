using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangCageScript : MonoBehaviour
{
    [SerializeField] SwitchScript switchScript;
    [SerializeField] PlayerActions player;
    [SerializeField] private GameObject[] candleList;
    [SerializeField] private Animator changAnimator;
    [SerializeField] private GameObject mvpBlock;
    [SerializeField] public GameObject changTimeline;
    [SerializeField] public GameObject changCrySound;
    /* [SerializeField] private bool candlesAreOff;*/
    [SerializeField] private AudioSource changCellOpenedSound;

    private bool candeles;
   
    void Update()
    {
        if (switchScript.lightOff==true)
        {
            candlesAreOff();

            if (candeles)
            {
                if (!player.flashLightStatus)
                {
                    gameObject.SetActive(false);
                    changAnimator?.SetBool("isFree", true);
                    changCellOpenedSound.Play();
                    mvpBlock.SetActive(false);
                    changCrySound.SetActive(false);
                    changTimeline.SetActive(true);
                    player.GetComponent<CheckpointScript>().secondFriend = true;
                }
            }
        }
    }
    private void candlesAreOff()
    {
        foreach (GameObject candle in candleList)
        {
            if (candle.GetComponent<CandleScript>().candleOff == false)
            {
                return;
            }
        }
        candeles = true;
    }
}
