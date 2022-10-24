using UnityEngine;

public class FreeTravisScript : MonoBehaviour
{
    [HideInInspector] public bool gotSyringe=false;
    [HideInInspector] public bool travisIsFree = false;
    [SerializeField] public GameObject coughSound;
    [SerializeField] public GameObject travisTimeline;
    [SerializeField] public GameObject finalBlock;
}
