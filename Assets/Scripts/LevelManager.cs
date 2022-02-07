using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LevelManager : MonoBehaviourPunCallbacks
{
    // We may have to make this a singleton in order to
    // give next position to player

    string levelName;

    public void LoadLevel(string level)
    {
        levelName = level;
        PhotonNetwork.LeaveRoom();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRoom(levelName);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(levelName);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(levelName);
    }
}
