using UnityEngine;


public class LockedSafeScript : MonoBehaviour
{
    [SerializeField] private GameObject toolInside;//knife inside the chest
    

    [Header("ForMe")]
    [SerializeField] public bool isSafeOpen = false;
    
    public void ActiveKnifeCollider()//after open safe to pick up knife
    {
        toolInside.GetComponent<Collider>().enabled = true;
    }
}
