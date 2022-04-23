using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public float speed = 30;

    void Start()
    {
        // Initial Velocity
        GetComponent<Rigidbody2D>().velocity =new Vector2(-1,-1f) * speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    float hitFactor(Vector2 ballPos, Vector2 racketPos,
                float racketHeight)
    {
        // ascii art:
        // ||  1 <- at the top of the racket
        // ||
        // ||  0 <- at the middle of the racket
        // ||
        // || -1 <- at the bottom of the racket
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Note: 'col' holds the collision information. If the
        // Ball collided with a racket, then:
        //   col.gameObject is the racket
        //   col.transform.position is the racket's position
        //   col.collider is the racket's collider

        // Hit the Paddle
        if (col.gameObject.tag == "Player")
        {
            // Calculate hit Factor
            float x = hitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.x);

            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(x, 1).normalized;

            // Set Velocity with dir * speed
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }

        // Hit the 
        if (col.gameObject.tag == "Bricks")
        {
            // Calculate hit Factor
            float x = hitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.x);

            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(x, -1).normalized;

            // Set Velocity with dir * speed
            GetComponent<Rigidbody2D>().velocity = dir * speed;

            //check if the brick should be destroyed
            col.gameObject.GetComponent<BrickBehaviour>().BrickCheck();

        }
    }

}
