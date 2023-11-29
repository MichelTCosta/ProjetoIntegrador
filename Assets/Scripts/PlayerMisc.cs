using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerMisc : MonoBehaviour
{
    [SerializeField]
    GameObject keyPrefab;

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


    [Header("Confirações do monstro")]
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
    [SerializeField]
    string monsterchoose;

    //Configurações de ressucitação(Nao terminado)
    [Header("Configurações do res(não terminado)")]
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
    RaycastHit hit;
    // Start is called before the first frame update
    void Awake()
    {
        view = GetComponentInParent<PhotonView>();
        if (view.IsMine)
        {
            manager = GameObject.FindObjectOfType<Charactermanager>(); //Encontra a referencia do manager
            playerMovement = GetComponent<PlayerMovement>(); // Encontra a referencia de movimentação do jogador
            timeToResCounter = timeToRes; // Guarda o tempo de ressucitar
            dot.enabled = false; // desabilita o ponto central da tela
            monsterRef = GameObject.Find("Monstro").gameObject; // encontra a referencia do monstro na cena
            animatorController = monsterRef.GetComponent<RuntimeAnimatorController>(); //transforma o controlador do monstro em um variavel
            monsterchoose = manager.monsterName;
            keyPrefab = GameObject.Find("Key1");
            if (monsterchoose == "Monstro 1") //checa o monstro escolhido
            {
             monsterRef.GetComponent<MeshFilter>().mesh = monster1; //troca o modelo do monstro
             animatorController = monster1Controller; //troca o animador do monstro

            }

            if (monsterchoose == "Monstro 2") //checa o monstro escolhido
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
        if (view.IsMine)//Checa se o cliente que foi chamado é o do jogador
        {
            normalCam.SetActive(false); //desabilita camera normal
            jumpScareCam.SetActive(true); //ativa a camera de jumpscare
            playerMovement.canMove = false; //Trava a movimentação do jogador
            isDead = true; //Refencia para poder reviver o jogador

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

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Cria um raycast na posição do mouse
           

            if (Physics.Raycast(ray, out hit))
            {
             

               if (hit.transform.tag != "Key") //Checa se o raycast encontrou a chave
               {
                    dot.enabled = false; // Desabilita o ponto central
               }



                if (hit.collider.tag == "Key" && Physics.Raycast(ray, out hit, 2f)) // Checa se o raycast acertou uma chave(pode ser trocado para item se necessario)
                {
                    dot.enabled = true; //Ativa o ponto central da tela
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("Works");
                        view.RPC("KeyGotten", RpcTarget.All);
                        
                    }

                }


            }
        
    }

    [PunRPC]
    void KeyGotten() // Função que atualiza os dados do monstro
    {
        monsterRef.GetComponent<MonsterBasics>().numberOfKeys++; // Aumenta o numero de chaves pegas
        monsterRef.GetComponent<MonsterBasics>().getKey = true; // Ativa o trigger que faz o monstro ficar irritado
        PhotonNetwork.Destroy(keyPrefab);
    }





}
