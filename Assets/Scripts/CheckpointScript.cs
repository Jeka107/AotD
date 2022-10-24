using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    [SerializeField] public bool firstFriend;
    [SerializeField] public bool secondFriend;
    [SerializeField] public bool thirdFriend;
    [SerializeField] public bool fourthFriend;

    [SerializeField] public Vector3 checkpoint1;
    [SerializeField] public Vector3 checkpoint2;
    [SerializeField] public Vector3 checkpoint3;
    [SerializeField] public Vector3 lastCheckpoint;

    private void Update()
    {
        if (firstFriend)
        {
            lastCheckpoint = checkpoint1;
            if (secondFriend)
            {
                lastCheckpoint = checkpoint2;
                if (thirdFriend)
                {
                    lastCheckpoint = checkpoint3;
                }
            }
        }  
    }

    /*  [SerializeField] public GameObject player;*/
}
