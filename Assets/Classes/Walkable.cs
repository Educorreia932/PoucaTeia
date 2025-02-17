﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();

            player.isOnSurface++;

            other.GetComponent<Rigidbody2D>().gravityScale = 0;
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else if (other.tag == "WebButt")
        {
            PlayerController player = other.gameObject.transform.parent.GetComponent<PlayerController>();

            if (player.makingWeb)
            {
                player.makingWeb = false;
                Vector3 Position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, 1);
                player.EndWeb(Position);
            }
        }
        else if (other.tag == "Bug" && this.gameObject.layer == 8)
        {
            Bug bug = other.GetComponent<Bug>();

            bug.isOnSurface++;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();

            player.isOnSurface--;
        }
        else if (other.tag == "WebButt")
        {
            PlayerController player = other.gameObject.transform.parent.GetComponent<PlayerController>();

            if (player.startingWeb)
            {
                player.startingWeb = false;
                player.makingWeb = true;

                RaycastHit2D hit = Physics2D.Raycast(other.gameObject.transform.position, -other.gameObject.transform.parent.transform.up, 1000, player.layerMask);

                // If it hits something...
                if (hit.collider != null)
                {
                    Vector3 Position = new Vector3(hit.point.x, hit.point.y, 1);
                    player.StartWeb(Position);
                }
            }
        }
        else if (other.tag == "Bug")
        {
            Bug bug = other.GetComponent<Bug>();

            bug.isOnSurface--;
        }
    }
}
