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
            anim.SetBool("isLeft", true);
        }
        else if (lastClickedPos.x > transform.position.x)
        {
            anim.SetBool("isLeft", false);
        }
    }

    //TODO: Fix sorting layer issue

    // If player collides with something we can make moving = false, probably
    // would also have to make it so lastClickedPos = transform.position
}
