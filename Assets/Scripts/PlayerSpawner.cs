using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSpawner : MonoBehaviour
{
    //Refencias
    public GameObject prefabHomem;
    public GameObject prefabMulher;



    Charactermanager manager;
    
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<Charactermanager>();



        //Spawna os jogadores dependendo do genero escolhido
        if(manager.boneco == "Homem")
        {
            PhotonNetwork.Instantiate(prefabHomem.name, this.transform.position, Quaternion.identity);
            
        }
        if(manager.boneco == "Mulher")
        {
           PhotonNetwork.Instantiate(prefabMulher.name, this.transform.position, Quaternion.identity);
           
        }
    }

}
