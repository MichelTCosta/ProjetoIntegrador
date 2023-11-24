using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerMisc : MonoBehaviour
{


    [Header("Referencias para as cameras")]
    public GameObject jumpScareCam;
    public GameObject normalCam;


    [Header("Referencias para o HUD")]
    [SerializeField]
    Image dot;
    [SerializeField]
    GameObject canvasOptions;
    [SerializeField]
    GameObject canvasHUD;


    [Header("Confira��es do monstro")]
    [SerializeField]
    Mesh monster1;
    [SerializeField]
    Mesh monster2;
    [SerializeField]
    RuntimeAnimatorController monster1Controller;
    [SerializeField]
    RuntimeAnimatorController monster2Controller;
    [SerializeField]
    GameObject monsterRef;

    [Header("Monstro escolhido")]
    public bool monstro1Escolhido;
    public bool monstro2Escolhido;

    //Configura��es de ressucita��o(Nao terminado)
    [Header("Configura��es do res(n�o terminado)")]
    [SerializeField]
    float timeToRes;
    float timeToResCounter;
    public bool isRessing;
    public bool isDead;

    //Referencias gerais
    RuntimeAnimatorController animatorController;
    PhotonView view;
    Charactermanager manager;
    PlayerMovement playerMovement;
    
    // Start is called before the first frame update
    void Awake()
    {
        view = GetComponentInParent<PhotonView>();
        if (view.IsMine)
        {
            manager = GameObject.FindObjectOfType<Charactermanager>(); //Encontra a referencia do manager
            playerMovement = GetComponent<PlayerMovement>(); // Encontra a referencia de movimenta��o do jogador
            timeToResCounter = timeToRes; // Guarda o tempo de ressucitar
            dot.enabled = false; // desabilita o ponto central da tela
            monsterRef = GameObject.Find("Monstro").gameObject; // encontra a referencia do monstro na cena
            animatorController = monsterRef.GetComponent<RuntimeAnimatorController>(); //transforma o controlador do monstro em um variavel
            if (monstro1Escolhido) //checa o monstro escolhido
            {
             monsterRef.GetComponent<MeshFilter>().mesh = monster1; //troca o modelo do monstro
             animatorController = monster1Controller; //troca o animador do monstro

            }

            if (monstro2Escolhido) //checa o monstro escolhido
            {
                monsterRef.GetComponent<MeshFilter>().mesh = monster2; //troca o modelo do monstro
                animatorController = monster2Controller; //troca o animador do monstro
            }
        }
        

    }

    private void Update()
    {
        DetectKey();
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
        //if (view.IsMine)
        //{
        //if (other.CompareTag("Player") && other.gameObject != this.gameObject);
        //        {
        //    Debug.Log("Checagem de colisor");
        //            if (Input.GetKeyDown(KeyCode.E) && isRessing == false)
        //            {
        //        Debug.Log("Checagem de input");
        //                if(timeToResCounter > 0)
        //                {
        //                    timeToResCounter -= Time.deltaTime;
        //                    isRessing = true;
        //                }
        //
        //
        //                if(timeToResCounter <= 0)
        //                {
        //                    isRessing = false;
        //                    other.GetComponent<PlayerMisc>().isDead = false;
        //                    other.GetComponent<PlayerMovement>().canMove = true;
        //                    timeToResCounter = timeToRes;
        //                }
        //                    
        //            }
        //        if (Input.GetKeyUp(KeyCode.E))
        //        {
        //            timeToResCounter = timeToRes;
        //            isRessing = false;
        //
        //        }
        //        }
        //
        //}

        
    }
    //private void OnTriggerExit(Collider other) 
    //{
    //
    //        if(other.CompareTag("Player") && other.GetComponent<PlayerMisc>().isDead)
    //        {
    //            timeToResCounter = timeToRes;
    //            isRessing = false;
    //        } 
    //
    //    
    //}


    void DetectKey() //Detecta a chave quando olha para ela e libera pegar
    {
        if (view.IsMine)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Cria um raycast na posi��o do mouse
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Key") == true) // Checa se o raycast acertou uma chave(pode ser trocado para item se necessario)
                {
                    dot.enabled = true; //Ativa o ponto central da tela
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        view.RPC("KeyGotten", RpcTarget.All); //Chave uma fun��o para todos os jogadores
                        PhotonNetwork.Destroy(hit.transform.gameObject); // Destroi o objeto pego
                    }

                }

                if (hit.transform.CompareTag("Key") == false) //Checa se o raycast encontrou a chave
                {
                    dot.enabled = false; // Desabilita o ponto central
                }
            }
        }
    }

    [PunRPC]
    void KeyGotten() //Fun��o que atualiza os dados do monstro
    {
        monsterRef.GetComponent<MonsterBasics>().numberOfKeys++; // Aumenta o numero de chaves pegas
        monsterRef.GetComponent<MonsterBasics>().getKey = true; // Ativa o trigger que faz o monstro ficar irritado
    }



}
