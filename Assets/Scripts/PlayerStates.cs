using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerStates : MonoBehaviourPunCallbacks
{
    [System.Serializable]
    public struct Player
    {
        public GameObject playerObject;
        public Vector2 curPos;
        public Vector2 lastPos;
    }

    public List<Player> players;

    private void Start()
    {
        AddPlayers();
    }

    void Update()
    {
        SyncAnimations();
    }

    void AddPlayers()
    {
        foreach (PlayerMovement player in FindObjectsOfType<PlayerMovement>())
        {
            if (!player.GetComponent<PhotonView>().IsMine)
            {
                Player newP = new Player();
                newP.playerObject = player.gameObject;

                players.Add(newP);
            }
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        // Update list of players
        foreach (PlayerMovement playerMovement in FindObjectsOfType<PlayerMovement>())
        {
            bool isNewPlayer = true;

            foreach (Player player in players)
            {
                if (playerMovement.gameObject == player.playerObject)
                {
                    isNewPlayer = false;
                }
            }

            if (isNewPlayer)
            {
                Player newP = new Player();
                newP.playerObject = playerMovement.gameObject;

                players.Add(newP);
            }
        }
    }

    void SyncAnimations()
    {
        foreach (Player player in players)
        {
            if (!player.playerObject.GetComponent<PhotonView>().IsMine)
            {
                
            }
        }
    }

}
