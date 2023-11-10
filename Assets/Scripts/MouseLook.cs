using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class MouseLook : MonoBehaviour
{
  private PlayerControls controls;

    public float mouseSensitivity = 100f;
    private Vector2 mouseLook;
    private float  xRotation = 0f;
    private Transform playerBody;
    PhotonView view;
    void Awake()
    {

            view = GetComponent<PhotonView>();
        if (view.IsMine)
        {
            playerBody= transform.parent;

        }
            controls = new PlayerControls();
            Cursor.lockState = CursorLockMode.Locked;

        

        

    }

    void Update()
    {
        if(view.IsMine)
        {
            Look();
        }

    }

    private void Look()
    {
      mouseLook =  controls.Player.Look.ReadValue<Vector2>();

      float mouseX = mouseLook.x * mouseSensitivity * Time.deltaTime;
      float mouseY = mouseLook.y * mouseSensitivity * Time.deltaTime;

      xRotation -= mouseY;
      xRotation = Mathf.Clamp(xRotation, -90f, 90);

      transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
      playerBody.Rotate(Vector3.up * mouseX);
    }

    private void OnEnable()
    {
      controls.Enable();
    }

    private void OnDisable()
    {
      controls.Disable();
    }
}
