using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public blinkPulse blinkEffect;
    //ball speed
    public float speed = 30;

    //ball immune state
    private bool isImmune = false;

    AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    
    public void StartBall()
    {
        // Initial Velocity
        GetComponent<Rigidbody2D>().velocity = new Vector2(-1, -1f) * speed;
    }

    float bounceCalulate(Vector2 ballPosition, Vector2 paddlePosition, float paddleHeight)
    {
        //x=1 if in the ball position is on the right of paddle
        //x=-1 if in the ball position is on the left of paddle
        //x=0 if in the ball position is on the middle of paddle
        return (ballPosition.x - paddlePosition.x) / paddleHeight;
    }

    IEnumerator ImmuneStop()
    {
        blinkEffect.enabled = true;
        yield return new WaitForSeconds(10f);
        isImmune = false;
        blinkEffect.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        // Hit the Paddle
        if (col.gameObject.tag == "Player")
        {
            // Calculate bouncing position
            float x = bounceCalulate(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.x);

            // Calculate direction, the Y position is always going up
            Vector2 dir = new Vector2(x, 1).normalized;

            // Set Velocity with dir * speed
            GetComponent<Rigidbody2D>().velocity = dir * speed;

            audioManager.HitEffect();
        }

        // Hit the 
        if (col.gameObject.tag == "Bricks")
        {
            // Calculate bouncing position
            float x = bounceCalulate(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.x);

            // Calculate direction, the Y position is always going down
            Vector2 dir = new Vector2(x, -1).normalized;

            // Set Velocity with dir * speed
            GetComponent<Rigidbody2D>().velocity = dir * speed;

            //check if the brick should be destroyed
            col.gameObject.GetComponent<BrickBehaviour>().BrickCheck();

            //check wheter the game is finished.
            GameManager.FindObjectOfType<GameManager>().CheckGame();

            audioManager.HitEffect();
        }

        //hit the abyss
        if (col.gameObject.tag == "Abyss")
        {
            //ignore if it is immune
            if (isImmune)
                return;
            //decrease health
            GameManager.FindObjectOfType<GameManager>().DecreaseHealth();

            audioManager.abyssEffect();
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "PowerUp")
        {
            //set the shield to inactive
            col.gameObject.SetActive(false);

            //set ball immune for 10 seconds
            isImmune = true;
            StartCoroutine(ImmuneStop());

            audioManager.powerUpEffect();
        }
    }

}
