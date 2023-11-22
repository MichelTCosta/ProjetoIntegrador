using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField]
    PhotonView view;
    [SerializeField]
    Camera cam;
    [SerializeField]
    Slider slider;



    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {

            cam.GetComponent<MouseLook>().mouseSensitivity = slider.value; //coloca o valor da sensibilidade da camera igual o valor do slider no menu de opçoes


        }
            
    }
}
