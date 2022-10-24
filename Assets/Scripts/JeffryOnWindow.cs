using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeffryOnWindow : MonoBehaviour
{
    [SerializeField] private GameObject lampOn;
    [SerializeField] private GameObject lampOff;
    [SerializeField] private GameObject lampOffRoom;
    [SerializeField] private GameObject ghoul;
    [SerializeField] private bool happened;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (happened!)
        {
            JeffAniamtion();
            happened = true;
        }
    }

    IEnumerator JeffAniamtion()
    {
        lampOn.SetActive(true);
        ghoul.SetActive(true);
        lampOff.SetActive(false);
        lampOffRoom.SetActive(false);
        yield return new WaitForSeconds(3);
        ghoul.SetActive(false);
    }
}
