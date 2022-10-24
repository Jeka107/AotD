using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [SerializeField] public bool lockedDoor;
    [SerializeField] public bool doorOpened=false;

    [Header("Sounds")]
    [SerializeField] private AudioSource doorOpenSound;
    [SerializeField] private AudioSource lockOpenSound;
    [SerializeField] private AudioSource lockedDoorSound;

    public void PlayDoorOpenSound()
    {
        doorOpenSound.Play();
        doorOpened = true;
    }
    public void PlayLockOpenSound()
    {
        lockOpenSound.Play();
    }
    public void PlayLockedDoorSound()
    {
        lockedDoorSound.Play();
    }
}
