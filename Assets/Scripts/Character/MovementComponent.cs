using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

namespace Character
{
    public class MovementComponent : MonoBehaviour
    {
        [SerializeField] private float WalkSpeed;
        [SerializeField] private float RunSpeed;
        [SerializeField] private float JumpForce;

        private GameInputActions InputActions;

        //Comp
        private Animator PlayerAnimator;
        private PlayerController PlayerController;
        private Rigidbody PlayerRigidbody;
        private NavMeshAgent PlayerNavMeshAgent;

        private Vector2 InputVector = Vector2.zero;
        private Vector3 MoveDirection = Vector3.zero;

        // Animator hashes
        private readonly int MovementXHash = Animator.StringToHash("MovementX");
        private readonly int MovementZHash = Animator.StringToHash("MovementZ");
        private readonly int IsRunningHash = Animator.StringToHash("IsRunning");
        private readonly int IsJumpingHash = Animator.StringToHash("IsJumping");

        [SerializeField] private float MoveDirectionBuffer = 2.0f;
        private Vector3 NextPositionCheck;
        private NavMeshHit hit;

        private void Awake()
        {
            PlayerController = GetComponent<PlayerController>();
            PlayerAnimator = GetComponent<Animator>();
            PlayerRigidbody = GetComponent<Rigidbody>();
            PlayerNavMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (PlayerController.isJumping) return;
            //if (!(InputVector.magnitude > 0)) MoveDirection = Vector3.zero;

            MoveDirection = transform.forward * InputVector.y + transform.right * InputVector.x;

            float currentSpeed = PlayerController.isRunning ? RunSpeed : WalkSpeed;

            Vector3 movementDirection = MoveDirection * (currentSpeed * Time.deltaTime);

            NextPositionCheck = transform.position + MoveDirection * MoveDirectionBuffer;

            if (NavMesh.SamplePosition(NextPositionCheck, out hit, 1.0f, NavMesh.AllAreas))
            {
                transform.position += movementDirection;
            }

            //PlayerNavMeshAgent.Move(movementDirection);
        } 

        public void OnMovement(InputValue value)
        {
            InputVector = value.Get<Vector2>();

            PlayerAnimator.SetFloat(MovementXHash, InputVector.x);
            PlayerAnimator.SetFloat(MovementZHash, InputVector.y);
        }

        public void OnRun(InputValue button)
        {
            PlayerController.isRunning = button.isPressed;
            PlayerAnimator.SetBool(IsRunningHash, button.isPressed);
        }

        public void OnJump(InputValue button)
        {
            if (PlayerController.isJumping) return;

            PlayerController.isJumping = true;
            PlayerAnimator.SetBool(IsJumpingHash, true);

            PlayerNavMeshAgent.enabled = false;

            Invoke(nameof(Jump), 0.1f);
        }

        public void Jump()
        {
            PlayerRigidbody.AddForce((transform.up + MoveDirection) * JumpForce, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Ground") && !PlayerController.isJumping) return;

            PlayerNavMeshAgent.enabled = true;

            PlayerController.isJumping = false;
            PlayerAnimator.SetBool(IsJumpingHash, false);
        }
    }
}