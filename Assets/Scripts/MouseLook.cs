using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class MouseLook : MonoBehaviour
{
  private PlayerControls controls;

    KeyBinding keyBinding;
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
            keyBinding = GetComponentInParent<KeyBinding>(); // Pega referencia de outro script

            controls = new PlayerControls();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        

        

    }

    void Update()
    {
        if(view.IsMine && keyBinding.isSettingsActive == false)
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
