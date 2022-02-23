using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    public Vector2 lastClickedPos;
    public Vector2 syncedPosition;
    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
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
            // TODO: Try all buffered so when a new player joins they get the changes.
            Vector2 destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            view.RPC("SetMovePos", RpcTarget.AllBuffered, destination);
        }
    }

    [PunRPC]
    void SetMovePos(Vector2 destination)
    {
        lastClickedPos = destination;
    }

    // If player collides with something we can make moving = false, probably
    // would also have to make it so lastClickedPos = transform.position
}
