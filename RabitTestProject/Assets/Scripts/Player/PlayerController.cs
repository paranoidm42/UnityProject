using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace rabbit_game.player {
    public class PlayerController : MonoBehaviour {


            [Header("Resources")]
        [SerializeField] private float gravityScale;
        public PlayerInputManager inputManager;
        Transform cameraTransform;
        Rigidbody rb;
        [SerializeField]PlayerCollisionDetect playerCollisionDetect;

        

        [Header("For Movement and Rotation")]
        Vector3 moveDirection;
        [SerializeField] float moveSpeed;
        [SerializeField] float walkSpeed;
        [SerializeField] float sprintSpeed;
        [SerializeField] float rotationSpeed;
        [SerializeField] float fallForce;
        public MovementState state;
        public enum MovementState
        {
            walking,
            sprinting,
            crouching,
            air
        }

        [Header("Crouch")]
        [SerializeField] float crouchSpeed;
        [SerializeField] float crouhcYScale;
        float startYScale;


        [Header("Jump")]
        [SerializeField] float jumpForce;
        [SerializeField] float jumpCooldown;
        [SerializeField] float airMultiplier;
        [SerializeField] float wallJumpForce;
        [SerializeField] private float wallJumpCooldownDuration;
        private bool wallJumpCooldown = false;
        [SerializeField] bool canDoubleJump;


        [Header("Slope")]
        [SerializeField] float slopeForce;
        bool exitingSlope;
        
        bool readyToJump = true;

        
        [SerializeField]bool isGround, isHead, onSlope;

        private void OnEnable() {
            inputManager.jumpHandler += Jump;
        }

        private void OnDisable() {
            inputManager.jumpHandler -= Jump;
        }

        void Start() {
            rb = GetComponent<Rigidbody>();
            cameraTransform = Camera.main.transform;
            rb.freezeRotation = true;

            startYScale = transform.localScale.y;
        }

        private void Update() {
            isGround = playerCollisionDetect.OverlapGroundChech();
            isHead = playerCollisionDetect.OverlapHeadChech();
            onSlope = playerCollisionDetect.SlopeRaycast();
            

            StateHandler();
        }

        private void FixedUpdate() {
            
            AddGravity();
            SpeedLimitController();
            Crouch();
            MovePlayer();
            RotatePlayer();
            DragController();

        }

        void StateHandler()
        {
            if(isGround && inputManager.crouchPress)
            {
                state = MovementState.crouching;
                moveSpeed = crouchSpeed;
            }
            else if(isGround && inputManager.runPress)
            {
                state = MovementState.sprinting;
                moveSpeed = sprintSpeed;
            }
            else if(isGround)
            {
                state = MovementState.walking;
                canDoubleJump = true;
                moveSpeed = walkSpeed;
            }
            else
            {
                state = MovementState.air;
                moveSpeed = walkSpeed;
            }
        }

        void MovePlayer() {

            moveDirection = CalculateMovement();
            print(moveDirection);
            if (onSlope && !exitingSlope)
            {
                
                rb.AddForce(GetSlopeMoveDirection() * moveSpeed * slopeForce, ForceMode.Force);
                if(rb.velocity.y > 0)
                    rb.AddForce(Vector3.down * 80f,ForceMode.Acceleration);
            }
            else
            {
                if(isGround)
                    rb.AddForce(moveDirection * moveSpeed *10, ForceMode.Force);
                else if(!isGround)
                    rb.AddForce(moveDirection * moveSpeed *10 * airMultiplier, ForceMode.Force);
            }

        }
        void RotatePlayer() {
            float targetAngel = cameraTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetAngel, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }  

        void Jump()
        {
            if (isGround) // Yere temas varsa normal zıplama
            {
                exitingSlope = true;
                readyToJump = false;
                JumpForce();
            }

            else if (canDoubleJump) // Double jump
            {
                JumpForce();
                canDoubleJump = false;
            }
            else if (playerCollisionDetect.OnWall() && !wallJumpCooldown) // Duvara temas varsa duvardan zıplama
            {
                JumpForce();
                StartCoroutine(WallJumpCooldown());
                canDoubleJump = false;
            }
            Invoke(nameof(ResetJump), jumpCooldown);

        }

        void Crouch()
        {
            if(inputManager.crouchPress && !isGround)
            {
                rb.AddForce(Vector3.down * fallForce * Time.deltaTime, ForceMode.Impulse);
            }
            else if(inputManager.crouchPress)
            {
                transform.localScale = new Vector3(transform.localScale.x, crouhcYScale, transform.localScale.z);
            }
            else if(!inputManager.crouchPress)
            {

                transform.localScale = new Vector3(transform.localScale.x, startYScale , transform.localScale.z);
                if(isHead)
                    transform.localScale = new Vector3(transform.localScale.x, startYScale , transform.localScale.z);
                else if(!isHead)
                     moveSpeed = crouchSpeed;

            }
        }

        void JumpForce() {

            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up* jumpForce, ForceMode.Impulse);
            
        }
        
        void ResetJump()
        {
            readyToJump = true;
            exitingSlope =false;
        }
        IEnumerator WallJumpCooldown()
        {
            wallJumpCooldown = true;
            yield return new WaitForSeconds(wallJumpCooldownDuration);
            wallJumpCooldown = false;
        }

        private Vector3 CalculateMovement() {
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;
            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();
            return camForward * inputManager.MovementValue.y  + camRight * inputManager.MovementValue.x;
        }

        void DragController() 
        {
            if(onSlope ||isGround && onSlope)
                rb.drag = 10;
            else if(isGround)
                rb.drag = 5;
            else
                rb.drag = 1;
        }


        void SpeedLimitController()
        {
            if( onSlope && !exitingSlope)
            {
                if(rb.velocity.magnitude > moveSpeed)
                    rb.velocity = rb.velocity.normalized * moveSpeed;
            }
            else
            {
                Vector3 flatVel = new Vector3(rb.velocity.x,0,rb.velocity.z);
                if(flatVel.magnitude > moveSpeed)
                {
                    Vector3 limitedVel = flatVel.normalized * moveSpeed;
                    rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z); 
                }
            }
            
        }

        Vector3 GetSlopeMoveDirection() 
        {
            //İşlem, nesnenin hareket vektörünü, eğimli yüzeye dik olan bir düzleme yansıtarak, düzleme paralel bir hareket vektörü elde eder.
            return Vector3.ProjectOnPlane(moveDirection, playerCollisionDetect.slopeHit.normal).normalized;
        }


        private void AddGravity() {
            
            if(!onSlope)
                rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
        }
    }
}
