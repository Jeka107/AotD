using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiningRoomJS : MonoBehaviour
{
    [SerializeField] private AudioSource glassBreakSound = null;
    [HideInInspector] private bool happened=false;
    private void OnTriggerEnter(Collider other)
    {
        if (!happened)
        {
            glassBreakSound.Play();
            happened = true;
        } 
    }
}
