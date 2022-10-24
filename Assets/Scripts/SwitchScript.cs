using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    [SerializeField] private AudioSource switchSound = null;
    [SerializeField] private List<GameObject> lights;
    [SerializeField] private GameObject switchHand;
    [SerializeField] public bool lightOff;
    public void SwitchUse()
    {
        if (lightOff == false)
        {
            switchSound.Play();
            switchHand.GetComponent<Animator>().SetBool("down", true);
            foreach (GameObject item in lights)
            {
                item.SetActive(false);
            }
            lightOff = true;
        }
        else
        {
            switchHand.GetComponent<Animator>().SetBool("down", false);
            foreach (GameObject item in lights)
            {
                item.SetActive(true);

            }
            lightOff = false;
        }

    }
}
