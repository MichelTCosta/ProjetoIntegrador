using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class MonsterPathFinding : MonoBehaviour
{
    Charactermanager manager;
    NavMeshAgent agent; //usando para o monstro encontrar o caminho

    List<GameObject> players; // referencias paras os players
    PhotonView view; //Componente do photon que sincroniza os clientes dos jogadores
    int target;

    [Header("Timer de troca de alvo")]
    [SerializeField]
    float switchTargetTime = 15f;
    float switchTarget;

    [Header("Distancia para pegar o player")]
    [SerializeField]
    float distanceToCatch;

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
        view.RPC("FollowPlayer", RpcTarget.All); //Executa a função para todos os jogadores
        ChoosePlayer();
        JumpScare();
    }

    [PunRPC]
    void FollowPlayer() //Função que faz o monstro checar se a posição do jogador mudou e avançar para a nova posição
    {
        if (players[target].GetComponent<PlayerMisc>().isDead == false)//checa se o jogador está morto se não estiver continua perseguindo ele
        {

            agent.SetDestination(players[target].gameObject.transform.position); //coloca um destino de pathfinding para o monstro
        }
        if (players[target].GetComponent<PlayerMisc>().isDead == true && numberOfKeys >= 4) //checa se o player que o monstro está perseguindo morreu e se estiver morto e o monstro estiver no lvl4 irá atras de outro direto
        {
            int playersInServer = PhotonNetwork.PlayerList.Length;
            target = Random.Range(0, playersInServer);
            return;
        }





    }


    void ChoosePlayer()//escolhe um jogador para perseguir a cada algum tempo
    {
        if (switchTarget <= 0)
        {
            switchTarget = switchTargetTime; 
            int playersInServer = PhotonNetwork.PlayerList.Length; //pega o numero de player conectados na sala
            target = Random.Range(0, playersInServer); // escolhe aleatoriamente um novo jogador

        }
        if (switchTarget > 0)
        {
            switchTarget -= Time.deltaTime;
        }

    }

    void JumpScare()//da um jumpscare dependendo da distacia do jogador
    {
        float distance = Vector3.Distance(transform.position, players[target].transform.position); //checa a distancia entre o monstro e os jogadores

        if (distance <= distanceToCatch && players[target].GetComponent<PlayerMisc>().isDead == false)//checa se a distacia for perto o suficiente e o jogador nao estiver morto e depois da um jumpscare
        {
            
            players[target].GetComponent<PlayerMisc>().JumpScare();//chama a funcao de jumpscare de dentro do script do player
            if(numberOfKeys < 4)//checa se o monstro esta no ultimo nivel e se nao estiver faz ele se acalmar depois de pegar um jogador
            {
                animator.SetTrigger("EnterCalmMode");//troca o modo do monstro
            }






        }
    }
}
