using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class MonsterPathFinding : MonoBehaviour
{
    Charactermanager manager;
    NavMeshAgent agent; //usando para o monstro encontrar o caminho

    [SerializeField]
    List<GameObject> players; // referencias paras os players
    PhotonView view; //Componente do photon que sincroniza os clientes dos jogadores
    public int target;

    [Header("Timer de troca de alvo")]
    [SerializeField]
    float switchTargetTime = 180f;
    float switchTarget;

    [Header("Distancia para pegar o player")]

    int playersInServer;

    Animator animator;
    int numberOfKeys; //Numero de chaves pegas pelos players
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
        numberOfKeys = GetComponent<MonsterBasics>().numberOfKeys;//numero de chaves que os jogadores tem
        players = manager.playerList; //lista de jogadores em gameobjects
           playersInServer = PhotonNetwork.PlayerList.Length;
        view.RPC("FollowPlayer", RpcTarget.All); //Executa a fun��o para todos os jogadores

       ChoosePlayer();
        transform.LookAt(players[target].transform.position);

    }

    [PunRPC]
    void FollowPlayer() //Fun��o que faz o monstro checar se a posi��o do jogador mudou e avan�ar para a nova posi��o
    {

        if (players[target].GetComponent<PlayerMisc>().isDead == false)//checa se o jogador est� morto se n�o estiver continua perseguindo ele
        {

            agent.SetDestination(players[target].gameObject.transform.position); //coloca um destino de pathfinding para o monstro
            
        }
        if (players[target].GetComponent<PlayerMisc>().isDead == true ) //checa se o player que o monstro est� perseguindo morreu e se estiver morto e o monstro estiver no lvl4 ir� atras de outro direto
        {
            agent.ResetPath();
            target = Random.Range(0, playersInServer);
            return;
        }


   

    }


    void ChoosePlayer()//escolhe um jogador para perseguir a cada algum tempo
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
