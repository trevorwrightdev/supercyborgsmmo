using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public float playerSpeed;
    
    // Update is called once per frame
    void Update()
    {
        MovePlayers();
    }

    void MovePlayers()
    {
        foreach (PlayerMovement player in FindObjectsOfType<PlayerMovement>())
        {
            if ((Vector2)player.transform.position != player.lastClickedPos)
            {
                float step = playerSpeed * Time.deltaTime;
                player.transform.position = Vector2.MoveTowards(player.transform.position, player.lastClickedPos, step);
            }
        }
    }
}
