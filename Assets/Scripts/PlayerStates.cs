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

    public void AddPlayers()
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
            if (!playerMovement.GetComponent<PhotonView>().IsMine)
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
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        
    }

    // TODO: When player leaves room, find the destroyed object and remove them from the list.

    void SyncAnimations()
    {
        
    }

}
