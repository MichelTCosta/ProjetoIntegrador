
using UnityEngine;
using Photon.Pun;
public class FunctionsCalls : MonoBehaviour
{
    [SerializeField]
    PlayerMisc playerMisc;
    [SerializeField]
    PhotonView view;




    public void CamReset()
    {
        if (view.IsMine)
        {

        playerMisc.ResetCam();
        }

    }
}
