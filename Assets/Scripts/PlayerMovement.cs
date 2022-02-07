using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    public float speed;
    Vector2 lastClickedPos;
    Animator anim;
    SpriteRenderer rend;
    PhotonView view;

    bool moving = false;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rend = GetComponentInChildren<SpriteRenderer>();
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
            lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moving = true;
        }

        if (moving && (Vector2)transform.position != lastClickedPos)
        {
            // We are moving as long as we haven't reached our destination.

            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, lastClickedPos, step);
        }
        else
        {
            // We are not moving because we have reached our destination.
            moving = false;
        }

        // Set animations
        anim.SetBool("isRunning", moving);

        // Set inversion
        if (lastClickedPos.x < transform.position.x)
        {
            FlipX();
        }
        else if (lastClickedPos.x > transform.position.x)
        {
            UnflipX();
        }
    }

    void FlipX()
    {
        if (view.IsMine)
        {
            view.RPC("FlipSprite", RpcTarget.AllBuffered);
        }
    }

    void UnflipX()
    {
        if (view.IsMine)
        {
            view.RPC("UnflipSprite", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void FlipSprite()
    {
        if (rend != null) rend.flipX = true;
    }

    [PunRPC]
    void UnflipSprite()
    {
        if (rend != null) rend.flipX = false;
    }

    //TODO: Make orientation stay accurate if player just joins.
    //TODO: Fix sorting layer issue

    // If player collides with something we can make moving = false
}
