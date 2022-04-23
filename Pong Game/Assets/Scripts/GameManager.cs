using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //waiting time before start the game
    public float waitForStartTime = 2f;

    //initial lives
    public int lives = 3;
    
    //health Sprite
    public Sprite fullHealthSprite;
    public Sprite emptyHealthSprite;

    //healthBar for health UI
    public Image[] healthBar;

    //Pop up for Game Over
    public GameObject gameOverPopUp;
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseHealth()
    {        
        //update the lives UI
        healthBar[lives-1].sprite = emptyHealthSprite;

        //decrease player lives
        lives--;

        //check if lives equal to 0, or lives are exhausted
        if (lives==0)
        {
            Time.timeScale = 0;
            gameOverPopUp.SetActive(true);
        }
    }
    

    //reset game
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartCoroutine(StartGame());
        Time.timeScale = 1;
    }


    IEnumerator StartGame()
    {
       
        //gives 2 seconds break. Break for a moment before start the game to make the user ready
        yield return new WaitForSeconds(waitForStartTime);

        GameObject.FindObjectOfType<BallControl>().StartBall();

        
    }

}
