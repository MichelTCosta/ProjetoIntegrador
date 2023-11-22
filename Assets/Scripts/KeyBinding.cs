using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class KeyBinding : MonoBehaviour
{

    PhotonView view;
    [SerializeField]
    GameObject optionsCanvas; // refencia para o canvas de opçoes
    [SerializeField]
    GameObject canvasHUD; //referencia para o canvas de HUD

    [HideInInspector]public bool isSettingsActive; // Variavel para saber se a HUD de opçoes está ativa ou não
    
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

                isSettingsActive = !isSettingsActive; //Da um toggle no bool

                if(isSettingsActive) //Checa se o boleano esta ativo e ativa o menu de opçoes e desativa o HUD
                {

                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    canvasHUD.SetActive(false);
                    optionsCanvas.SetActive(true);
                }


                if (isSettingsActive == false)// Checa se o boleano não estiver ativo desativa o menu de opções e ativa o HUD
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
