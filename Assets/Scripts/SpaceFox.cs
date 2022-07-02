using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceFox : MonoBehaviour
{
    GameObject player;
    Vector2 playerPosition;
    float moveSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            playerPosition = player.transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, moveSpeed);
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if (collision.gameObject.tag == "Bullet")
        // {
        //     Destroy(collision.gameObject);
        //     Destroy(gameObject);
        // }
    }
}
