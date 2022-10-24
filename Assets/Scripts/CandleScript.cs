using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleScript : MonoBehaviour
{
    [SerializeField] public GameObject flameParticle;
    [SerializeField] public GameObject flameLight;
    [SerializeField] public bool candleOff = false;
    [SerializeField] private AudioSource blowOutCandleSound;

    public void CandleUse()
    {
        flameLight.SetActive(false);
        flameParticle.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        candleOff = true;
        blowOutCandleSound.Play();
    }
}
