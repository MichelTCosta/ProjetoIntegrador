using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class KeyBinding : MonoBehaviour
{

    PhotonView view;
    [SerializeField]
    GameObject optionsCanvas;
    [SerializeField]
    GameObject canvasHUD;

    [HideInInspector]public bool isSettingsActive;
    
    // Start is called before the first frame update
    void Awake()
    {

        view = GetComponent<PhotonView>();
        optionsCanvas.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {

                isSettingsActive = !isSettingsActive;

                if(isSettingsActive)
                {

                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    canvasHUD.SetActive(false);
                    optionsCanvas.SetActive(true);
                }


                if (isSettingsActive == false)
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    canvasHUD.SetActive(true);
                    optionsCanvas.SetActive(false);
                }



            }
        }
    }
}
