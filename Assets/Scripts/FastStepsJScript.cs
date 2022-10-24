using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastStepsJScript : MonoBehaviour
{
    [SerializeField] AudioSource fastSteps = null;
    [HideInInspector] private bool happened=false;
    private void OnTriggerEnter(Collider other)
    {
        if (!happened)
        {
            fastSteps.Play();
            happened = true;
        }  
    }
}
