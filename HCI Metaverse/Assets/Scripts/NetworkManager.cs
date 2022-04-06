using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private string programVersion = "1.0.0";

    /* Player Information */
    public InputField playerName;
    public GameObject playerInfoPanel;

    /* Connection Information */
    public Text connectionInfoText;
    public GameObject connectionInfoPanel;

    public void Connect()
    {
        playerInfoPanel.SetActive(false);

        connectionInfoPanel.SetActive(true);

        PhotonNetwork.GameVersion = programVersion;

        PhotonNetwork.ConnectUsingSettings();

        connectionInfoText.text = "Connecting ...";
    }

    public override void OnConnectedToMaster()
    {
        connectionInfoText.text = "Online :)";

        PhotonNetwork.NickName = playerName.text;
 
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        connectionInfoText.text = "Offline :(";
    }

    public override void OnJoinedLobby()
    {
        if (PhotonNetwork.IsConnected)
        {
            connectionInfoText.text = "Loading ...";
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 100 });
    }

    public override void OnJoinedRoom()
    {
        connectionInfoText.text = "Join to the room ...";

        PhotonNetwork.LoadLevel("MainScene");
    }
}
