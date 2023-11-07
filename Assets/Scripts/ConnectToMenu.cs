using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class ConnectToMenu : MonoBehaviourPunCallbacks
{
    private void Start()//Pega as configuraçoes do photon e conecta ao servidor
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()//Carrega a cena do menu
    {
        SceneManager.LoadScene("Menu");
    }


}
