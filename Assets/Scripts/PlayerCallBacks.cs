using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerCallBacks : MonoBehaviourPunCallbacks
{
    Charactermanager manager;

    // Start is called before the first frame update
    void Awake()
    {
        manager = GameObject.FindObjectOfType<Charactermanager>();
        manager.playerList.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
