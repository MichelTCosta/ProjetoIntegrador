using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Charactermanager : MonoBehaviour
{
    //Referencias
    public TMP_Dropdown character;
    public TMP_InputField nome;

    
    //Variaveis
    public string boneco;
    public string nick;

    public int playerInServer;
    public bool isOnGame;
    
    

    public List<GameObject> playerList;//Lista de gameobjects dos jogadores

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
     
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.CurrentRoom != null)
        {
            playerInServer = PhotonNetwork.CurrentRoom.PlayerCount; //Pega o numero de jogadores na sala
        }

        if(isOnGame == false) //checa se os jogadores ja estão em jogo e para de pegar informaçoes que nao sao mais necessarias
        {
            PhotonNetwork.NickName = nome.text;
            boneco = character.options[character.value].text;
        }


    }
}
