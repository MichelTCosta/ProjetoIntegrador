using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomInputField;


    private void Awake()
    {

        PhotonNetwork.AutomaticallySyncScene = true; //Sincroniza a cena no photon
    }
    public void CreateRoom() //Cria a sala
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(roomInputField.text, roomOptions);

    }

    public void JoinRoom() //Conecta a uma sala criada
    {
        PhotonNetwork.JoinRoom(roomInputField.text);

    }
    public override void OnJoinedRoom()//Checa se entrou na sala e troca de cena
    {

        PhotonNetwork.LoadLevel("WaitingRoom");

    }
}
