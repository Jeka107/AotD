using UnityEngine;

public class FreeAlexisScript : MonoBehaviour
{
    [SerializeField] public GameObject rope;
    [SerializeField] private GameObject doorShutJS;
    [SerializeField] private GameObject sack;
    [SerializeField] private GameObject crySound;
    [SerializeField] public GameObject alexisTimeline;
    [SerializeField] public bool alexisIsDown;

    [HideInInspector] public bool gotKnife;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (transform.position.y <= 0f)
        {
            alexisIsDown = true;
        }
    }
    public void GravityOn()
    {
        rb.useGravity = true;
        sack.SetActive(false);
        crySound.SetActive(false);
        doorShutJS.GetComponent<Animator>().enabled = false; //after cutting the rope we can open the door.
    }
}
