using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LevelManager : MonoBehaviourPunCallbacks
{
    // We may have to make this a singleton in order to
    // give next position to player

    public static Vector2 spawnPosition = new Vector2(0, 0);
    string levelName;

    public void LoadLevel(string level, Vector2 location)
    {
        FindObjectOfType<GameCanvas>().loadCanvas.SetActive(true);

        spawnPosition = location;
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
