using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class KeyBinding : MonoBehaviour
{

    PhotonView view;
    [SerializeField]
    GameObject optionsCanvas; // refencia para o canvas de op�oes
    [SerializeField]
    GameObject canvasHUD; //referencia para o canvas de HUD

    [HideInInspector]public bool isSettingsActive; // Variavel para saber se a HUD de op�oes est� ativa ou n�o
    
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

                if(isSettingsActive) //Checa se o boleano esta ativo e ativa o menu de op�oes e desativa o HUD
                {

                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    canvasHUD.SetActive(false);
                    optionsCanvas.SetActive(true);
                }


                if (isSettingsActive == false)// Checa se o boleano n�o estiver ativo desativa o menu de op��es e ativa o HUD
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
