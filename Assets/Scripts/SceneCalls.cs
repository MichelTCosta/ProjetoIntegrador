using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
public class SceneCalls : MonoBehaviourPunCallbacks
{
    PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponentInParent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void OnLeftRoom() //Botão para o jogador voltar para o lobby
    {
        base.OnLeftRoom();
        PhotonNetwork.Disconnect();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Loading");
        
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        base.OnMasterClientSwitched(newMasterClient);
        PhotonNetwork.Disconnect();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Loading");
    }


}
