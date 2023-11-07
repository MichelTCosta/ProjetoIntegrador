using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NickDisplay : MonoBehaviour
{
    //Referencias
    public TMP_Text nickText;
    Charactermanager manager;
    PhotonView view;
    void Awake()
    {
        manager = GameObject.FindObjectOfType<Charactermanager>();
        view = GetComponent<PhotonView>();
        nickText.text = manager.nick;
        if(view.IsMine)
        {
            nickText.text = PhotonNetwork.NickName;
        }
        else
        {
            nickText.text = view.Owner.NickName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
