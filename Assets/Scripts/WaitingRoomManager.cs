using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitingRoomManager : MonoBehaviour
{
    //Referencias
    Charactermanager manager;
    PhotonView view;

    [SerializeField]
    TMP_Text connectedPlayersText;

    [SerializeField]
    GameObject startButton;
    
    //Variaveis
    int connectedPlayers;

   
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<Charactermanager>();
        view = GetComponent<PhotonView>();  

    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.CurrentRoom != null) //checa se a sala existe
        {
            connectedPlayers = PhotonNetwork.CurrentRoom.PlayerCount; // pega a contagem de jogadores na sala
        }

        connectedPlayersText.text = connectedPlayers.ToString(); //atualiza a contagem de players


        if(view.IsMine)//Faz com que so a pessoa que entrou primeiro na sala possa iniciar a partida
        {
            startButton.SetActive(true);
        }
    }

    [PunRPC]
    void LoadGame() //Carrega a cena do jogo
    {
        SceneManager.LoadScene("Game");
        manager.isOnGame = true;
    }


    public void StartGame() //void para usar no botão dentro do jogo
    {
        view.RPC("LoadGame", RpcTarget.All); //Força a execução da função para todos os jogadores

    }
}
