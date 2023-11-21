using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMisc : MonoBehaviour
{
    public GameObject jumpScareCam;
    public GameObject normalCam;
    public bool isDead;
    PhotonView view;
    Rigidbody rb;
    // Start is called before the first frame update
    void Awake()
    {
        view = GetComponentInParent<PhotonView>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void JumpScare()
    {
        if (view.IsMine)//Checa se o cliente que foi chamado é o do jogador
        {
            normalCam.SetActive(false);
            jumpScareCam.SetActive(true);
            rb.isKinematic = true;
            isDead = true;

        }

    }
    public void ResetCam()
    {
        if (view.IsMine)
        {

            jumpScareCam.SetActive(false);
            normalCam.SetActive(true);
        }

    }
}
