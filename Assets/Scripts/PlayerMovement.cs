using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    Vector2 lastClickedPos;
    Animator anim;
    SpriteRenderer rend;

    bool moving = true;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rend = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        // Check for mouse every frame
        Move();
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
            rend.flipX = true;
        }
        else if (lastClickedPos.x > transform.position.x)
        {
            rend.flipX = false;
        }
    }
}
