using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //bricks
    public GameObject bricksGroup;
    public GameObject bricksPrefab;

    //Paddle Gameobject
    public GameObject paddle;

    //ball Gameobject
    public GameObject ball;

    public Sprite spriteWallConcrete;

    //waiting time before start the game
    public float waitForStartTime = 2f;

    //initial bricks
    private int brickcount = 10;

    //initial lives
    public int lives = 3;
    
    //health Sprite
    public Sprite fullHealthSprite;
    public Sprite emptyHealthSprite;

    //healthBar for health UI
    public Image[] healthBar;

    //Text for Level Info
    public Text levelInfo;

    //Pop up for Game Complete
    public GameObject gameCompletePopUp;

    //Pop up for Game Over
    public GameObject gameOverPopUp;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //setup of the game level
        GameSetup();
        //start the game
        StartCoroutine(StartGame());
    }

    void GameSetup()
    {
        //spawnBricks
        List<GameObject> spawnedObjects = SpawnBricks(GameData.obstacleNumbers[GameData.gameLevel - 1]);
        
        
        //check if there is special obstacle;
        if (GameData.specialObstacle[GameData.gameLevel - 1] != 0)
            SpecialObstacle(spawnedObjects); //spawn special obstacle/bricks

        //setup paddle speed
        paddle.GetComponent<PongPaddleControl>().speed = GameData.paddleSpeed[GameData.gameLevel - 1];

        //setup ball speed
        ball.GetComponent<BallControl>().speed = GameData.ballSpeed[GameData.gameLevel - 1];

        //setup Game Level Text
        levelInfo.text = "Level " + GameData.gameLevel;

        
    }


    List<GameObject> SpawnBricks(int bricksNumber)
    {
        //inital position x = -2.38 and y = 1 
        //add x by +1.2 for the next position; and decreasing x by 0.65 for the next position
        float xPosition = -2.38f;
        float yPosition = 1f;

        //generate list for spawned game object
        List<GameObject> spawnedObjects = new List<GameObject>();

        //spawn the bricks
        for (int i = 1; i <= bricksNumber; i++)
        {
            //spawn bricks and add them to list 
            spawnedObjects.Add(Instantiate(bricksPrefab, new Vector3(xPosition, yPosition, 0), Quaternion.identity, bricksGroup.transform) as GameObject);


            xPosition += 1.2f;

            //calculate the brick is already 5
            if (i % 5 == 0)
            {
                //move to start position of x
                xPosition = -2.38f;
                //move to the next y position
                yPosition -= 0.65f;


            }
        }

        return spawnedObjects;
    }

    //spawn specialObstacle
    void SpecialObstacle(List<GameObject> spawnedObjects)
    {
        for (int i = 0; i < GameData.specialObstacle[GameData.gameLevel - 1]; i++)
        {
            //generate random number
            int itemIndex = Random.Range(0, GameData.specialObstacle[GameData.gameLevel - 1] - 1);
            //pick random object and change its image
            spawnedObjects[itemIndex].GetComponent<SpriteRenderer>().sprite = spriteWallConcrete;
            //change its endurance
            spawnedObjects[itemIndex].GetComponent<BrickBehaviour>().doggedness = Random.Range(2, 5);

            //remove replaced from list
            spawnedObjects.RemoveAt(itemIndex);
        }
    }

    public void CheckGame()
    {
        GameObject [] bricks = GameObject.FindGameObjectsWithTag("Bricks");


        if(bricks.Length-1 == 0)
        {
            Time.timeScale = 0;
            gameCompletePopUp.SetActive(true);

            if (GameData.gameLevel < 9)
            {
                GameData.gameLevel++;
                PlayerPrefs.SetInt("Level", GameData.gameLevel);
            }

        }
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
