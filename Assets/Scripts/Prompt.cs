using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

    public class Prompt : MonoBehaviour
    {
        [SerializeField] private float playerProximity;
        [SerializeField] private Vector3 playerPosition;
        [SerializeField] private Vector3 objectPosition;

        private PlayerInput _playerInput;
        private StarterAssets.StarterAssetsInputs _input;
        [SerializeField] private GameObject player;

}
