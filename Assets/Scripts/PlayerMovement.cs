using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{


    [Header("Movement Settings")]

    [SerializeField]
    float crouchSpeed;

    [SerializeField]
    float walkSpeed = 6f;

    [SerializeField]
    float runSpeed = 12f;

    [SerializeField]
    float jumpPower = 7f;

    [SerializeField]
    float gravity = 10f;

    public bool canMove = true;

    bool isRunning;



    [Header("Crouch Settings")]

    [SerializeField]
    float crouchHeight;

    [SerializeField]
    float croucYHeight;

    [SerializeField]
    LayerMask whatIsGround;

    [SerializeField]
    float roofDetectionDistance;

    float baseHeight = 1.93f;
    float baseYHeight = 0.93f;
    bool isCrouch;



    [Header("Camera")]
    [SerializeField]
    GameObject activateCamera;

    public Camera playerCamera;

    [SerializeField]
    float lookSpeed = 2f;

    [SerializeField]
    float lookXLimit = 45f;

    [SerializeField]
    GameObject crouchCameraPos;
    [SerializeField]
    GameObject normalCameraPos;

    float rotationX = 0;
    Vector3 moveDirection = Vector3.zero;




    //Refencias
    PhotonView view;
    CharacterController characterController;
    void Awake()
    {
        view = GetComponent<PhotonView>();

        if (view.IsMine)
        {

            characterController = GetComponent<CharacterController>();
            activateCamera.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }

    void Update()
    {
        if (view.IsMine)
        {
            Crouch();


           #region Handles Movment
           Vector3 forward = transform.TransformDirection(Vector3.forward);
           Vector3 right = transform.TransformDirection(Vector3.right);

           // Press Left Shift to run
           if(isCrouch == false)
           {
               isRunning = Input.GetKey(KeyCode.LeftShift);

           }
               float curSpeedX = canMove ? (isRunning ? runSpeed: isCrouch ? crouchSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
               float curSpeedY = canMove ? (isRunning ? runSpeed: isCrouch ? crouchSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
               float movementDirectionY = moveDirection.y;
               moveDirection = (forward * curSpeedX) + (right * curSpeedY);

           #endregion

           #region Handles Jumping
           if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
           {
               moveDirection.y = jumpPower;
           }
           else
           {
               moveDirection.y = movementDirectionY;
           }

           if (!characterController.isGrounded)
           {
               moveDirection.y -= gravity * Time.deltaTime;
           }

           #endregion

           #region Handles Rotation
           characterController.Move(moveDirection * Time.deltaTime);

           if (canMove)
           {
               rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
               rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
               playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
               transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
           }


           #endregion

           }
       





    }




    void Crouch() 
    {


            bool hit = Physics.Raycast(transform.position, Vector3.up, roofDetectionDistance ,whatIsGround);
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            playerCamera.transform.position = crouchCameraPos.transform.position;
            isCrouch = true;
            characterController.height = crouchHeight;
            characterController.center = new Vector3(0, croucYHeight, 0);
        }



        if (Input.GetKeyUp(KeyCode.LeftControl) && hit == false)
        {
            playerCamera.transform.position = normalCameraPos.transform.position;
            isCrouch = false;
            characterController.height = baseHeight;
            characterController.center = new Vector3(0, baseYHeight, 0);
        }

    }
}