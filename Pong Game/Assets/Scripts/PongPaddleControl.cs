using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongPaddleControl : MonoBehaviour
{
    
    public float speed = 10.0f;
    private Rigidbody2D rb2d;

    
    // Start is called before the first frame update
    void Start()
    {
        //get the Rigidbody component
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       //Checking touch interaction everytime
       TouchMove();
    }

    //check touch
    void TouchMove()
    {
        //do not allow to move if game is not started yet
        GameManager GM = GameObject.FindObjectOfType<GameManager>();
        if (!GM.gameStarted)
            return;

        //if user touches the screen
        if (Input.GetMouseButtonDown(0))
        {
            //get position of mouse
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //get velocity
            var vel = rb2d.velocity;

            //if the position is more than middle point of layer
            if (mousePos.x > 1)
            {
                //move the paddle by using velocity to the left
                vel.x = speed;
            }
            else if (mousePos.x < 1) //else, less than
            {
                //move the paddle by using velocity to the left
                vel.x = -speed;
            }
            else
            {
                //stop the paddle if touch in the middle
                vel.x = 0;
            }

            //update velocity of object
            rb2d.velocity = vel;

        }

        //if user does not touch the screen
        if (Input.GetMouseButtonUp(0))
        {

            //get position of mouse
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //get velocity
            var vel = rb2d.velocity;

            //stop the paddle if the user is not touching the screen.
            vel.x = 0;
            
            rb2d.velocity = vel;

        }

    }

}
