using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartFox : MonoBehaviour
{
	GameObject player;
	Vector2 playerPosition;
    Vector2 currentVelocity;
	float moveSpeed = 0.01f;
    float dartSpeed = 3f;
    float speedUpDistance = 3;
    float slowDownDistance = 5;
    bool isDarting;
    bool movingToward = true;

	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerPosition = player.transform.position;
        if (Vector2.Distance(transform.position,playerPosition) < 7)
        {
            Destroy(gameObject);
        }
	}

	// Update is called once per frame
	void Update()
	{
		if (player != null)
		{
            if (!isDarting)
            {
                playerPosition = player.transform.position;
                transform.position = Vector2.MoveTowards(transform.position, playerPosition, moveSpeed);
                if (Vector2.Distance(transform.position,playerPosition) < speedUpDistance)
                {
                    isDarting = true;
                }
            }
            else
            {
                if (Vector2.Distance(transform.position, playerPosition) > 0.5f && movingToward)
                {
                    float angle = Mathf.Atan2(playerPosition.y - transform.position.y, playerPosition.x - transform.position.x) * Mathf.Rad2Deg;
                    currentVelocity = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right * dartSpeed;
                    GetComponent<Rigidbody2D>().velocity = currentVelocity;
                }
                else if (Vector2.Distance(transform.position, playerPosition) > slowDownDistance)
                {
                    isDarting = false;
                    movingToward = true;
                }
                else
                {
                    if (movingToward)
                    {
                        StartCoroutine(SlowDown());
                    }
                    movingToward = false;
                }
            }
		}
		else
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}
	}

    IEnumerator SlowDown()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        for (float i = rb.velocity.x > rb.velocity.y ? rb.velocity.x : rb.velocity.y; i > 0.5f; i /= 2)
        {
            yield return new WaitForSeconds(0.5f);
            rb.velocity /= 2;
        }
        yield return null;
        rb.velocity = Vector2.zero;
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