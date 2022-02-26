using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    public Vector2 lastClickedPos;
    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();

        // This is so we don't move to 0, 0 every time we load a new scene
        if (view.IsMine)
        {
            lastClickedPos = LevelManager.spawnPosition;
        }
        else
        {
            Debug.Log("Found another player");
        }
    }

    private void Update()
    {
        // Check for mouse every frame
        if (view.IsMine)
        {
            Move();
        }
    }

    void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            view.RPC("SetMovePos", RpcTarget.AllBuffered, destination);
        }
    }

    [PunRPC]
    void SetMovePos(Vector2 destination)
    {
        lastClickedPos = destination;
    }
}
