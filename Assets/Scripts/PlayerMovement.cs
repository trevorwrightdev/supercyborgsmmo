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
    }

    private void Update()
    {
        // Check for mouse every frame
        if (view.IsMine)
        {
            Move();
        }
    }

    // TODO: Make it so your coordinates are stored as integers on your character. But on the movement manager, it converts them to vectors.

    void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // TODO: Try all buffered so when a new player joins they get the changes.
            view.RPC("SetMovePos", RpcTarget.All);
        }
    }

    [PunRPC]
    void SetMovePos()
    {
        lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // If player collides with something we can make moving = false, probably
    // would also have to make it so lastClickedPos = transform.position
}
