using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float crouchHeight = 0.5f; // Altura do jogador quando agachado
    bool readyToJump;

    [HideInInspector] public float walkSpeed;

    [Header("Sprint")]
    public float sprintSpeedMultiplier = 1.5f;
    public KeyCode sprintKey = KeyCode.LeftShift;
    private bool isSprinting;

    [Header("Crouch")]
    public KeyCode crouchKey = KeyCode.LeftControl;
    private bool isCrouching;
    private Vector3 originalCenter;
    private float originalHeight;
    private CapsuleCollider playerCollider;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;

    [SerializeField] GameObject cam;
    Rigidbody rb;
    PhotonView view;
    private void Awake()
    {
        view = GetComponent<PhotonView>();

        if (view.IsMine)
        {
            cam.SetActive(true);
        }
            rb = GetComponent<Rigidbody>();
            playerCollider = GetComponent<CapsuleCollider>();
            rb.freezeRotation = true;
            originalCenter = rb.centerOfMass;

            readyToJump = true;
            
            originalHeight = playerHeight;
        
        

        
    }

    private void Update()
    {

            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
            MyInput();
            SpeedControl();
            if (grounded)
            {
                rb.drag = groundDrag;
            }

            else
            {
                rb.drag = 0;
            }

        
    }

    private void FixedUpdate()
    {

            MovePlayer();
        

        

    }

    private void MyInput()
    {
        if(view.IsMine)
        {
 horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        isSprinting = Input.GetKey(sprintKey);

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // Agachamento
        if (Input.GetKeyDown(crouchKey))
        {
            StartCrouch();
        }
        else if (Input.GetKeyUp(crouchKey))
        {
            StopCrouch();
        }
        }
       
    }

    private void MovePlayer()
    {

            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
            float currentMoveSpeed = isSprinting ? moveSpeed * sprintSpeedMultiplier : moveSpeed;
            if (isCrouching)
                currentMoveSpeed *= 0.5f; // Reduz a velocidade ao agachar
            if (grounded)
                rb.AddForce(moveDirection.normalized * currentMoveSpeed * 10f, ForceMode.Force);
            else if (!grounded)
                rb.AddForce(moveDirection.normalized * currentMoveSpeed * 10f * airMultiplier, ForceMode.Force);
        
       
    }

    private void SpeedControl()
    {
        if(view.IsMine)
        {
         Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
        }
       
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    
    private void StartCrouch()
    {
        if (!isCrouching)
        {
            isCrouching = true;
            playerHeight = crouchHeight;
            playerCollider.height = crouchHeight;

            playerCollider.center = new Vector3(playerCollider.center.x, crouchHeight / 2f, playerCollider.center.z);

            rb.centerOfMass = new Vector3(originalCenter.x, originalCenter.y - 0.25f, originalCenter.z);
        }
    }

    private void StopCrouch()
    {
        if (isCrouching)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.up, out hit, originalHeight - crouchHeight, whatIsGround))
            {
                return;
            }
            
            isCrouching = false;
            playerHeight = originalHeight;
            playerCollider.height = originalHeight; 

            playerCollider.center = originalCenter;

            rb.centerOfMass = originalCenter;
        }
    }
}