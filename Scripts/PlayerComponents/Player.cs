using System.Collections;
using Items.Crafting;
using UIComponents;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerComponents
{
    public class Player : MonoBehaviour
    {

        private CharacterController _characterController;
        private Vector3 _move;
        private Animator _animator;
        private Rigidbody _rigidbody;
        private bool _sprint = false;
        public float characterSpeed = 3f;
        [SerializeField] private float sprintMultiplier = 2f;
        [SerializeField] private GameObject body;
        [SerializeField] private Interact interactObject;
        private Collider _collider;
        private static readonly int Sprinting = Animator.StringToHash("Sprinting");
        private static readonly int Walking = Animator.StringToHash("Walking");
        private float _time;
        private bool _falling = false;
        public static bool movementLock = false;
        public InventoryController inventoryController;
        private bool _skipFrame = false;


        public void OnMove(InputAction.CallbackContext context)
        {
            if (movementLock)
            { 
                _move = Vector3.zero;
                return;
            }
            Vector2 movementInput = context.ReadValue<Vector2>();
            _move = new Vector3(-movementInput[1], 0, movementInput[0]);
            _animator = body.GetComponent<Animator>();
        }

        // Start is called before the first frame update
        void Start()
        {
            _characterController = gameObject.AddComponent<CharacterController>();
            _characterController.center = new Vector3(0, 1, 0);
            _animator = GetComponentInChildren<Animator>();
            _collider = GetComponent<Collider>();
            _rigidbody = body.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if (movementLock)
            {
                _animator.SetBool("Walking", false);
                _animator.SetBool("Sprinting", false);
            }
            if (_skipFrame) return;
            if (!interactObject || !interactObject.GetInAnimation())
            {
                Move();
            }
        }

        private void LateUpdate()
        {
            if (_skipFrame)
            {
                _skipFrame = false;
                return;
            }
            if (movementLock) return;
            if (!Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 0.18f))
            {
                if (!_falling)
                {
                    _falling = true;
                    _time = Time.time;
                }
                transform.Translate(Vector3.down * (10 * (Time.time - _time) * Time.deltaTime));
            }
            else
            {
                _falling = false;
            }
        }

        private void Move()
        {
            if (movementLock) return;
            float mag = _move.magnitude;
            if (mag > 1)
            {
                _move.Normalize();
            }

            Vector3 move = _move;
        
            if (mag > 0)
            {
                if (_sprint)
                {
                    move = _move * sprintMultiplier;
                    _animator.SetBool(Sprinting, true);
                    _animator.SetBool(Walking, false);
                }
                else
                {
                    _animator.SetBool(Walking, true);
                    _animator.SetBool(Sprinting, false);
                }

                body.transform.localPosition = new Vector3(0, 0, 0);
                body.transform.localRotation = new Quaternion(0, 0, 0, 1);
            }
            else
            {
                _animator.SetBool(Walking, false);
                _animator.SetBool(Sprinting, false);
            }

            _characterController.Move(move * (Time.deltaTime * characterSpeed));
            if (_move != Vector3.zero && _move.magnitude > 0.1)
            {
                Vector3 controlledMove = Vector3.RotateTowards(_characterController.transform.forward,
                    _move, 5 * Time.deltaTime,
                    0.5f * Time.deltaTime);
                _characterController.transform.forward = controlledMove;
            }
        }


        public void OnLook(InputAction.CallbackContext context)
        {
        
        }

        public void OnFire(InputAction.CallbackContext context)
        {

        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _sprint = true;
            }

            else if (context.canceled)
            {
                _sprint = false;
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (!context.canceled) return;
            var crafting = FindObjectOfType<CraftingMenu>();
            if (crafting && crafting.gameObject.activeSelf)
            {
                crafting.ToggleMenu();
                return;
            }
            var pauseHandler = FindObjectOfType<PauseHandler>();
            if (pauseHandler) pauseHandler.OnPause();
        }

        public void OnCraft(InputAction.CallbackContext context)
        {
            if (!context.canceled) return;
            var hud = FindObjectOfType<HUD>();
            if (hud) hud.ToggleCraftingMenu();
        }

        public void SetLocation(Vector3 location)
        {
            StartCoroutine(MoveToLocation(location));
        }
        
        private IEnumerator MoveToLocation (Vector3 location)
        {
            yield return new WaitForEndOfFrame();
            _skipFrame = true;
            gameObject.transform.position = location;
        }
        

    }
}
