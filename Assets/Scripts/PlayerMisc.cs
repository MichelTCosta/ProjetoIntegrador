using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerMisc : MonoBehaviour
{
    [Header("Referencias para as chaves")]
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

    // Configurações de ressucitação(Não terminado)
    [Header("Configurações do res(Não terminado)")]
    [SerializeField]
    float timeToRes;

    float timeToResCounter;
    public bool canBeRessed;

    public bool isDead;

    // Referencias gerais
    RuntimeAnimatorController animatorController;
    PhotonView view;
    Charactermanager manager;
    PlayerMovement playerMovement;
    RaycastHit hit;
    PhotonView otherPlayerView;
    // Start is called before the first frame update
    void Awake()
    {
        view = GetComponentInParent<PhotonView>();
        if (view.IsMine)
        {
            manager = GameObject.FindObjectOfType<Charactermanager>(); //Encontra a referencia do manager
            playerMovement = GetComponent<PlayerMovement>(); // Encontra a referencia de movimentação do jogador
            timeToResCounter = timeToRes; // Guarda o tempo de ressucitar
            if(Camera.main != null)
            {
                dot.enabled = false; // Desabilita o ponto central da tela
            }
            monsterRef = GameObject.Find("Monstro").gameObject; // Encontra a referencia do monstro na cena
            animatorController = monsterRef.GetComponent<RuntimeAnimatorController>(); // Transforma o controlador do monstro em um variavel
            monsterchoose = manager.monsterName;
            keyPrefab = GameObject.Find("Key1");
            
            if (monsterchoose == "Monstro 1") // Checa o monstro escolhido
            {
                monsterRef.GetComponent<MeshFilter>().mesh = monster1; // Troca o modelo do monstro
                animatorController = monster1Controller; // Troca o animador do monstro
                monsterRef.GetComponent<MeshCollider>().sharedMesh = null;
                monsterRef.GetComponent<MeshCollider>().sharedMesh = monster1;
                monsterRef.GetComponent<MonsterPathFinding>().agent.baseOffset = 1f;
            }

            if (monsterchoose == "Monstro 2") // Checa o monstro escolhido
            {
                monsterRef.GetComponent<MeshFilter>().mesh = monster2; // Troca o modelo do monstro
                animatorController = monster2Controller; // Troca o animador do monstro
                monsterRef.GetComponent<MeshCollider>().sharedMesh = null;
                monsterRef.GetComponent<MeshCollider>().sharedMesh = monster2;
                monsterRef.GetComponent<MonsterPathFinding>().agent.baseOffset = 0.03f;
            }
        }
    }

    private void Update()
    {
        DetectKey();
        ResTimer();
       
       
    }

    public void JumpScare()
    {
        if (view.IsMine)// Checa se o cliente que foi chamado é o do jogador
        {
            normalCam.SetActive(false); // Desabilita camera normal
            jumpScareCam.SetActive(true); // Ativa a camera de jumpscare

            

        }

    }
    public void ResetCam()// Função que reset a camera normal, é ativada dentro da animação do jumpscare
    {


            normalCam.SetActive(true); // Ativa a camera normal
            jumpScareCam.SetActive(false); // Desativa a camera de jumpscare

        

    }


    [PunRPC]
    void Ressucite()
    {
        isDead = false;
        GetComponent<PlayerMovement>().canMove = true;
        canBeRessed = false;
    }



    void DetectKey() //Detecta a chave quando olha para ela e libera pegar
    {
        
        if(Camera.main != null)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Cria um raycast na posição do mouse


            if (Physics.Raycast(ray, out hit))
            {
             

               if (hit.transform.CompareTag("Key") == false || hit.transform.CompareTag("Player") == false)  // Checa se o raycast encontrou a chave
               {
                    dot.enabled = false; // Desabilita o ponto central

               }



                if (hit.collider.CompareTag("Key") && Physics.Raycast(ray, out hit, 2f)) // Checa se o raycast acertou uma chave(pode ser trocado para item se necessario)
                {
                    dot.enabled = true; // Ativa o ponto central da tela
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("Works");
                        view.RPC("KeyGotten", RpcTarget.All);
                        
                    }

                }


                if(hit.collider.CompareTag("Player")&& hit.transform.GetComponent<PhotonView>() != null && Physics.Raycast(ray, out hit, 2f))
                {
                    dot.enabled = true;
                    otherPlayerView = hit.transform.GetComponent<PhotonView>();
                    PlayerMisc playermisc = hit.transform.GetComponent<PlayerMisc>();
                    PlayerMovement playerMovement = hit.transform.GetComponent<PlayerMovement>();
                    if (Input.GetKeyDown(KeyCode.E) && playermisc.canBeRessed)
                    {


                        view.RPC("Ressucite", otherPlayerView.Controller);
                        playermisc.isDead = false;
                        playerMovement.canMove = true;

                    }
                  

                    
                 
                    
                }




            }

        }
        else
        {
            return;
        }
           

        
    }

    [PunRPC]
    void KeyGotten() // Função que atualiza os dados do monstro
    {
        monsterRef.GetComponent<MonsterBasics>().numberOfKeys++; // Aumenta o numero de chaves pegas
        monsterRef.GetComponent<MonsterBasics>().getKey = true; // Ativa o trigger que faz o monstro ficar irritado
        PhotonNetwork.Destroy(keyPrefab);
    }


    public void ResTimer()
    {
       if(isDead)
        {
            if(timeToRes > 0)
            {
                timeToRes -= Time.deltaTime;
            }
            if(timeToRes <= 0)
            {
                canBeRessed = true;
                timeToRes = timeToResCounter;
            }
        }

    }
}
