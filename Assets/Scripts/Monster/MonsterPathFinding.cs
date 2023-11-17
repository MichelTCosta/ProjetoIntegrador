using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class MonsterPathFinding : MonoBehaviour
{
    Charactermanager manager;
    NavMeshAgent agent; //usando para o monstro encontrar o caminho
                        
    List<GameObject> players; // refencias paras os players
    PhotonView view; //Componente do photon que sincroniza os clientes dos jogadores



    // Start is called before the first frame update
    void Awake()
    {
      view = GetComponent<PhotonView>();
      agent = GetComponent<NavMeshAgent>();
        manager = GameObject.FindObjectOfType<Charactermanager>();
    }

    // Update is called once per frame
    void Update()
    {

        players = manager.playerList;
        view.RPC("FollowPlayer", RpcTarget.All); //Executa a função para todos os jogadores


    }

    [PunRPC]
    void FollowPlayer() //Função que faz o monstro checar se a posição do jogador mudou e avançar para a nova posição
    {
        if(players[0].gameObject.transform.hasChanged)
        {
            players[0].gameObject.transform.hasChanged = false;

            agent.SetDestination(players[0].gameObject.transform.position);



        }
    }


}
