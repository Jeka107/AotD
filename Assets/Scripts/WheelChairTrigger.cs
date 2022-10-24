using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelChairTrigger : MonoBehaviour
{
    [SerializeField] private GameObject wheelchair;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        wheelchair.SetActive(true);
    }
}
