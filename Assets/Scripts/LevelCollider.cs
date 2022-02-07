using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LevelCollider : MonoBehaviour
{
    public string levelToLoad;
    LevelManager lvlManager;

    private void Start()
    {
        lvlManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (collision.GetComponent<PhotonView>().IsMine)
        {
            lvlManager.LoadLevel(levelToLoad);
        }

    }
}
