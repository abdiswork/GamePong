using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Shield Power Up
    public GameObject shieldPowerUp;

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

    //Pop up for Tutorial
    public GameObject tutorialPopUp;

    //Pop up for Paused Game
    public GameObject pauseGamePopUp;

    public bool gameStarted = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        

        //show tutorial for the first user
        int firstPlayer = PlayerPrefs.GetInt("firstPlayer",1);
        //check if they are first player
        if (firstPlayer == 1)
        {
            tutorialPopUp.SetActive(true);
            PlayerPrefs.SetInt("firstPlayer", 0);
        }

        GameSetup();
    }

    void Update()
    {
        //start the game if touched
        if (Input.GetMouseButtonDown(0) && !gameStarted)
        {
            tutorialPopUp.SetActive(false);
            StartGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }


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

        //start spawn the power up
        StartCoroutine(SpawnPowerUp());
        
    }


    List<GameObject> SpawnBricks(int bricksNumber)
    {
        //inital position x = -1.97 and y = 1 
        //add x by +1 for the next position; and decreasing x by 0.65 for the next position
        float xPosition = -1.97f;
        float yPosition = 1f;

        //generate list for spawned game object
        List<GameObject> spawnedObjects = new List<GameObject>();

        //spawn the bricks
        for (int i = 1; i <= bricksNumber; i++)
        {
            //spawn bricks and add them to list 
            spawnedObjects.Add(Instantiate(bricksPrefab, new Vector3(xPosition, yPosition, 0), Quaternion.identity, bricksGroup.transform) as GameObject);


            xPosition += 1f;

            //calculate the brick is already 5
            if (i % 5 == 0)
            {
                //move to start position of x
                xPosition = -1.97f;
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
            GameObject.FindObjectOfType<AudioManager>().CompletedEffect();

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
            GameObject.FindObjectOfType<AudioManager>().CompletedEffect();
        }
    }
    

    //reset game
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartGame();
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseGamePopUp.SetActive(true);
    }

    public void ResumeGame()
    {
        pauseGamePopUp.SetActive(false);
        Time.timeScale = 1;
    }

    void StartGame()
    {
        

        Time.timeScale = 1;

        StartCoroutine(MoveBallAfter());
    }

    IEnumerator MoveBallAfter()
    {
        //wait for 2 seconds
        yield return new WaitForSeconds(1f);
        //enable game
        gameStarted = true;
        //start ball movement
        GameObject.FindObjectOfType<BallControl>().StartBall();
    }

    IEnumerator SpawnPowerUp()
    {
        //wait for 10 second before first start of power up
        yield return new WaitForSeconds(10f);

        while (gameStarted)
        {
            //delay the spawn based on game setup
            yield return new WaitForSeconds(GameData.PowerUpsTime[GameData.gameLevel-1]);
            
            //random position
            shieldPowerUp.transform.position = new Vector2(Random.Range(-2.7f, 2.7f), Random.Range(-1.6f, -3.8f));

            //set the object to active
            shieldPowerUp.SetActive(true);
        }

    }

}
