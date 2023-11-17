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
        view.RPC("FollowPlayer", RpcTarget.All); //Executa a fun��o para todos os jogadores


    }

    [PunRPC]
    void FollowPlayer() //Fun��o que faz o monstro checar se a posi��o do jogador mudou e avan�ar para a nova posi��o
    {
        if(players[0].gameObject.transform.hasChanged)
        {
            players[0].gameObject.transform.hasChanged = false;

            agent.SetDestination(players[0].gameObject.transform.position);



        }
    }


}
