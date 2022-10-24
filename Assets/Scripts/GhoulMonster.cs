using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulMonster : MonoBehaviour
{
    [Header("Audio")]
    private AudioSource monsterScream;
    [SerializeField] GameObject monster;

    private void Awake()
    {
        monsterScream=GetComponent<AudioSource>();
    }

    public void PlayScream()
    {
        monsterScream.Play();
    }
}
