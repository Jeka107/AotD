using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
	[RequireComponent(typeof(PlayerInput))]
#endif
	public class FirstPersonController : MonoBehaviour
	{
		public delegate void OnFirstInteraction(string state);
		public static event OnFirstInteraction onFirstInteraction;

		public enum MovementState
		{
			standing,
			walking,
			sprinting,
			crouching,
		}


		[Header("Player")]
		private float targetSpeed;
		[Tooltip("Move speed of the character in m/s")]
		public float MoveSpeed = 4.0f;
		[Tooltip("Sprint speed of the character in m/s")]
		public float SprintSpeed = 6.0f;
		[Tooltip("Rotation speed of the character")]
		public float RotationSpeed = 1.0f;
		[Tooltip("Acceleration and deceleration")]
		public float SpeedChangeRate = 10.0f;

		[Space(10)]
		[Header("Crouching")]
		[SerializeField] private float crouchSpeedMovement;
		[SerializeField] private float crouchSpeed;
		[SerializeField] private float crouchYScale;
		private float startYScale;
		private bool crouched;

		[Space(10)]
		[Tooltip("The height the player can jump")]
		public float JumpHeight = 1.2f;
		[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
		public float Gravity = -15.0f;

		[Space(10)]
		[Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
		public float JumpTimeout = 0.1f;
		[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
		public float FallTimeout = 0.15f;

		[Header("Player Grounded")]
		[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
		public bool Grounded = true;
		[Tooltip("Useful for rough ground")]
		public float GroundedOffset = -0.14f;
		[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
		public float GroundedRadius = 0.5f;
		[Tooltip("What layers the character uses as ground")]
		public LayerMask GroundLayers;

		[Header("Cinemachine")]
		[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
		public GameObject CinemachineCameraTarget;
		[Tooltip("How far in degrees can you move the camera up")]
		public float TopClamp = 90.0f;
		[Tooltip("How far in degrees can you move the camera down")]
		public float BottomClamp = -90.0f;

		[Header("RayCast")]
		[SerializeField] private float stopDistanceCollider;

		[Header("Sounds")]
		[SerializeField] private float baseStepSpeed = 0.5f;
		[SerializeField] private float sprintStepMultipler = 0.6f;
		[SerializeField] private AudioSource footStepsSound = default;
		[SerializeField] private AudioClip[] stepsSounds;

		[Space(10)]
		[SerializeField] public MovementState state;

		[Header("Noise")]
		[SerializeField] private float walkRadios;
		[SerializeField] private float sprintRadios;
		[SerializeField] private float crouchRadios;
		[SerializeField] private Noise noise;
		private float _noiseRadius;

		[Space]
		[SerializeField] private float slowmotion;
		//private bool firstInteraction;
		private string firstInteractionButton;

		// cinemachine
		private float _cinemachineTargetPitch;

		// player
		private float _speed;
		private float _rotationVelocity;
		private float _verticalVelocity;
		private float _terminalVelocity = 53.0f;

		// timeout deltatime
		private float _jumpTimeoutDelta;
		private float _fallTimeoutDelta;

		
		private float footstepTimer = 0;
		private float getCurrentOffset;
		private Vector3 inputDirection;

		


#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		private PlayerInput _playerInput;
#endif
		private CharacterController _controller;
		private StarterAssetsInputs _input;
		private GameObject _mainCamera;

		private const float _threshold = 0.01f;

		private bool IsCurrentDeviceMouse
		{
			get
			{
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
				return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
			}
		}

		private void Awake()
		{
			Cursor.visible = false;
			// get a reference to our main camera
			if (_mainCamera == null)
			{
				_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
			}
		}

		private void Start()
		{
			_controller = GetComponent<CharacterController>();
			_input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
			_playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

			// reset our timeouts on start
			_jumpTimeoutDelta = JumpTimeout;
			_fallTimeoutDelta = FallTimeout;

			startYScale =transform.localScale.y;

			Noise.onFirstInteraction += SlowMotion;
			Eyes.onFirstInteraction += SlowMotion;
		}
        private void OnDestroy()
        {
			Noise.onFirstInteraction -= SlowMotion;
			Eyes.onFirstInteraction -= SlowMotion;
		}

		private void Update()
		{
			JumpAndGravity();
			Crouch();
			GroundedCheck();
			StateHandler();
			Move();
			FootSteps();
		}
		private void StateHandler()
		{
			// Mode - Sprinting
			if (_input.sprint)
			{
				state = MovementState.sprinting;
				targetSpeed = SprintSpeed;
				getCurrentOffset = baseStepSpeed* sprintStepMultipler;
				_noiseRadius = sprintRadios;
				noise.SetNoise(true, _noiseRadius);

				if(firstInteractionButton=="Sprint"&& Time.timeScale!=1.0f)//Tutorial
                {
					Time.timeScale = 1.0f;
					onFirstInteraction?.Invoke("Sprint");
				}
			}
			else if(_input.crouch)
            {
				state = MovementState.crouching;
				targetSpeed = crouchSpeed;
				_noiseRadius = crouchRadios;
				noise.SetNoise(true, _noiseRadius);

				if (firstInteractionButton == "Crouch" && Time.timeScale != 1.0f)//Tutorial
				{
					Time.timeScale = 1.0f;
					onFirstInteraction?.Invoke("Crouch");
				}
			}
			//Mode-Standing
			else if (inputDirection == Vector3.zero)
			{
				state = MovementState.standing;
				targetSpeed = 0f;
				noise.SetNoise(false);
			}
			// Mode - Walking
			else if(_controller.isGrounded && inputDirection != Vector3.zero)
			{
				state = MovementState.walking;
				targetSpeed = MoveSpeed;
				getCurrentOffset = baseStepSpeed;
				_noiseRadius = walkRadios;
				noise.SetNoise(true, _noiseRadius);
			}
		}

		private void LateUpdate()
		{
			CameraRotation();
		}

		private void GroundedCheck()
		{
			// set sphere position, with offset
			Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
			Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
		}

		private void CameraRotation()
		{
			// if there is an input
			if (_input.look.sqrMagnitude >= _threshold)
			{
				//Don't multiply mouse input by Time.deltaTime
				float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

				_cinemachineTargetPitch += _input.look.y * RotationSpeed * deltaTimeMultiplier;
				_rotationVelocity = _input.look.x * RotationSpeed * deltaTimeMultiplier;

				// clamp our pitch rotation
				_cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

				// Update Cinemachine camera target pitch
				CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

				// rotate the player left and right
				transform.Rotate(Vector3.up * _rotationVelocity);
			}
		}
		private void Crouch()
		{
			if (_input.crouch)
			{
				transform.localScale = new Vector3(transform.localScale.x, crouchYScale,transform.localScale.z);
			}
			if (!_input.crouch)
			{
				transform.localScale = new Vector3(transform.localScale.x, startYScale,transform.localScale.z);
				crouched = false;
			}
		}
		private void Move()
		{
			// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

			// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is no input, set the target speed to 0
			if (_input.move == Vector2.zero) targetSpeed = 0.0f;

			// a reference to the players current horizontal velocity
			float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

			float speedOffset = 0.1f;
			float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

			// accelerate or decelerate to target speed
			if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				// creates curved result rather than a linear one giving a more organic speed change
				// note T in Lerp is clamped, so we don't need to clamp our speed
				_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

				// round speed to 3 decimal places
				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
			{
				_speed = targetSpeed;
			}

			// normalise input direction
			inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving

			if (_input.move != Vector2.zero)
			{
				Ray ray = Camera.main.ScreenPointToRay(_input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit, stopDistanceCollider))
				{
					Debug.DrawLine(ray.origin, hit.point);
					if (_input.move.y == 1)
					{
						inputDirection = transform.right * _input.move.x;
					}
					else
                    {
						inputDirection = transform.right * _input.move.x + transform.forward * _input.move.y;
					}
				}
				else
				{
					// move
					inputDirection = transform.right * _input.move.x + transform.forward * _input.move.y;
				}
			}
			//on crouch
			if (_input.crouch && !crouched)
			{
				_controller.Move(Vector3.down * crouchSpeed * Time.deltaTime);
				crouched = true;
			}

			// move the player
			_controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
		}
		private void FootSteps()
        {
			if (!_controller.isGrounded) return;     //if jumping then no steps sound
			if (_input.move == Vector2.zero) return; //if standing then no steps sound
			if (_input.crouch) return;               //if crouching then no steps sound

			footstepTimer -= Time.deltaTime;

			if (footstepTimer<=0)
            {
				footStepsSound.PlayOneShot(stepsSounds[Random.Range(0,stepsSounds.Length-1)]);
				footstepTimer = getCurrentOffset;
			}
		}

		private void JumpAndGravity()
		{
			if (Grounded)
			{
				// reset the fall timeout timer
				_fallTimeoutDelta = FallTimeout;

				// stop our velocity dropping infinitely when grounded
				if (_verticalVelocity < 0.0f)
				{
					_verticalVelocity = -2f;
				}

				// Jump
				if (_input.jump && _jumpTimeoutDelta <= 0.0f)
				{
					// the square root of H * -2 * G = how much velocity needed to reach desired height
					_verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
				}

				// jump timeout
				if (_jumpTimeoutDelta >= 0.0f)
				{
					_jumpTimeoutDelta -= Time.deltaTime;
				}
			}
			else
			{
				// reset the jump timeout timer
				_jumpTimeoutDelta = JumpTimeout;

				// fall timeout
				if (_fallTimeoutDelta >= 0.0f)
				{
					_fallTimeoutDelta -= Time.deltaTime;
				}

				// if we are not grounded, do not jump
				_input.jump = false;
			}

			// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
			if (_verticalVelocity < _terminalVelocity)
			{
				_verticalVelocity += Gravity * Time.deltaTime;
			}
		}
		private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
		{
			if (lfAngle < -360f) lfAngle += 360f;
			if (lfAngle > 360f) lfAngle -= 360f;
			return Mathf.Clamp(lfAngle, lfMin, lfMax);
		}

		private void OnDrawGizmosSelected()
		{
			Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
			Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

			if (Grounded) Gizmos.color = transparentGreen;
			else Gizmos.color = transparentRed;

			// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
			Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
		}

		private void SlowMotion(string state)
        {
			if(Time.timeScale==1.0f)
            {
				firstInteractionButton = state;
				Time.timeScale = slowmotion;
            }
        }
	}
}