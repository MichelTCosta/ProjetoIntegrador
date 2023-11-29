using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
public class SceneCalls : MonoBehaviourPunCallbacks
{
    PhotonView view;
    Charactermanager manager;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponentInParent<PhotonView>();
        manager = GameObject.FindAnyObjectByType<Charactermanager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void OnLeftRoom() //Botão para o jogador voltar para o lobby
    {
        base.OnLeftRoom();
        PhotonNetwork.Disconnect();
        Destroy(manager.gameObject);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Loading");
        
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        base.OnMasterClientSwitched(newMasterClient);
        PhotonNetwork.Disconnect();
        Destroy(manager.gameObject);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Loading");
    }


}
