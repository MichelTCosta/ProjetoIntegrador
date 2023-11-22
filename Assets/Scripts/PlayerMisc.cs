using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMisc : MonoBehaviour
{
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
    Rigidbody rb;
    Charactermanager manager;
    // Start is called before the first frame update
    void Awake()
    {
        view = GetComponentInParent<PhotonView>();
        if (view.IsMine)
        {
            manager = GameObject.FindObjectOfType<Charactermanager>();
            rb = GetComponent<Rigidbody>();

        }
        
    }

    public void JumpScare()
    {
        if (view.IsMine)//Checa se o cliente que foi chamado é o do jogador
        {
            normalCam.SetActive(false); //desabilita camera normal
            jumpScareCam.SetActive(true); //ativa a camera de jumpscare
            rb.isKinematic = true; //trava o jogador no lugar
            isDead = true; 


        }

    }
    public void ResetCam()//função que reset a camera normal, é ativada dentro da animação do jumpscare
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
