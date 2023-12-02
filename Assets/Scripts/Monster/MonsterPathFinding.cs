using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class MonsterPathFinding : MonoBehaviour
{
    Charactermanager manager;
    NavMeshAgent agent; // Usando para o monstro encontrar o caminho

    [SerializeField]
    List<GameObject> players; // Referencias paras os players
    PhotonView view; // Componente do photon que sincroniza os clientes dos jogadores
    public int target;

    [Header("Timer de troca de alvo")]
    [SerializeField]
    float switchTargetTime = 180f;
    float switchTarget;

    [Header("Distancia para pegar o player")]

    int playersInServer;
    Vector3 lookPos;
    Animator animator;
    int numberOfKeys; // Numero de chaves pegas pelos players
    // Start is called before the first frame update
    void Awake()
    {
        view = GetComponent<PhotonView>();
        agent = GetComponent<NavMeshAgent>();
        manager = GameObject.FindObjectOfType<Charactermanager>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        numberOfKeys = GetComponent<MonsterBasics>().numberOfKeys;// Numero de chaves que os jogadores tem
        players = manager.playerList; // Lista de jogadores em gameobjects
         //  playersInServer = PhotonNetwork.PlayerList.Length;
        view.RPC("FollowPlayer", RpcTarget.AllViaServer); // Executa a função para todos os jogadores

       ChoosePlayer();
        lookPos.x = players[target].transform.position.x;
        transform.LookAt(lookPos);

    }

    [PunRPC]
    void FollowPlayer() //Função que faz o monstro checar se a posição do jogador mudou e avançar para a nova posição
    {

        if (players[target].GetComponent<PlayerMisc>().isDead == false)// Checa se o jogador está morto se não estiver continua perseguindo ele
        {

            agent.SetDestination(players[target].gameObject.transform.position); // Coloca um destino de pathfinding para o monstro
            
        }
        if (players[target].GetComponent<PlayerMisc>().isDead == true ) // Checa se o player que o monstro está perseguindo morreu e se estiver morto e o monstro estiver no lvl4 irá atras de outro direto
        {
            playersInServer = PhotonNetwork.PlayerList.Length;
            target = Random.Range(0, playersInServer);

        }


        

    }


    void ChoosePlayer()// Escolhe um jogador para perseguir a cada algum tempo
    {
        if (switchTarget <= 0)
        {
            switchTarget = switchTargetTime; 
            int playersInServer = PhotonNetwork.PlayerList.Length; // Pega o numero de player conectados na sala
            target = Random.Range(0, playersInServer); // Escolhe aleatoriamente um novo jogador
            

        }

        if (switchTarget > 0)
        {
            switchTarget -= Time.deltaTime;
        }

    }



  

   

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && other.GetComponent<PlayerMisc>().isDead == false)
        {
            other.GetComponent<PlayerMisc>().JumpScare();
            other.GetComponent<PlayerMisc>().isDead = true;
            other.GetComponent<PlayerMovement>().canMove = false;
            if( numberOfKeys < 4)
            {
                animator.SetTrigger("EnterCalmMode");
            }
        }
    }




}
