using UnityEngine;

public class DoorShutJS : MonoBehaviour
{
    [SerializeField] private AudioSource doorShut = null;
    [SerializeField] private GameObject doorToClose;
    [SerializeField] private GameObject mockCollider;
    [SerializeField] private GameObject monster;

    // door shut jump scare on clock puzzle room
    private void OnTriggerEnter(Collider other)
    {
        doorShut.Play();
        doorToClose.GetComponent<MeshCollider>().enabled = false;
        mockCollider.SetActive(true);
        doorToClose.GetComponent<Animator>().SetBool("openDoor", false);
        doorToClose.GetComponent<Animator>().SetBool("closeDoor", true);
        doorToClose.GetComponent<JSDoorScript>().jsReady = true;
        doorToClose.GetComponent<LockedDoor>().doorOpened = false;
        monster.SetActive(true);
        Destroy(gameObject);
    }
}
