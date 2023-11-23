using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMisc : MonoBehaviour
{
    PlayerMovement playerMovement;

    public GameObject jumpScareCam;
    public GameObject normalCam;


    [SerializeField]
    GameObject canvasOptions;
    [SerializeField]
    GameObject canvasHUD;
    [SerializeField]
    SphereCollider sphereCollider;

    public bool isDead;
    PhotonView view;

    Charactermanager manager;
    // Start is called before the first frame update
    void Awake()
    {
        view = GetComponentInParent<PhotonView>();
        if (view.IsMine)
        {
            manager = GameObject.FindObjectOfType<Charactermanager>();
            playerMovement = GetComponent<PlayerMovement>();
        }
        
    }

    public void JumpScare()
    {
        if (view.IsMine)//Checa se o cliente que foi chamado � o do jogador
        {
            normalCam.SetActive(false); //desabilita camera normal
            jumpScareCam.SetActive(true); //ativa a camera de jumpscare
            playerMovement.canMove = false; //Trava a movimenta��o do jogador
            isDead = true; //Refencia para poder reviver o jogador

        }

    }
    public void ResetCam()//fun��o que reset a camera normal, � ativada dentro da anima��o do jumpscare
    {
        if (view.IsMine)
        {

            jumpScareCam.SetActive(false); //desativa a camera de jumpscare
            normalCam.SetActive(true); // ativa a camera normal

            
        }

    }


    private void OnTriggerStay(Collider other)
    {
        if (view.IsMine)
        {

        }
    }
    void Resucitate()
    {
        
    }



}
