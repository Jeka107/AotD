using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    [SerializeField] private MonsterAI monsterAI;
    [SerializeField] private bool enterFirstFloor;
    [SerializeField] private bool enterSecondFloor;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            if (enterFirstFloor)
            {
                monsterAI.secondFloor = false;
                monsterAI.firstFloor = true;
                monsterAI.walkPointSet = false;
            }
            else if (enterSecondFloor)
            {
                monsterAI.firstFloor = false;
                monsterAI.secondFloor = true;
                monsterAI.walkPointSet = false;
            }
        }
    }
}
