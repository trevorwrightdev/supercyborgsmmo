using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        PhotonNetwork.Instantiate(player.name, LevelManager.spawnPosition, Quaternion.identity);
    }

}
