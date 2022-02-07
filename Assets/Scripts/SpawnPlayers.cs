using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject player;

    Vector2 spawnPosition = new Vector2(0, 0);

    private void Start()
    {
        PhotonNetwork.Instantiate(player.name, spawnPosition, Quaternion.identity);
    }

}
