using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManagement : MonoBehaviour
{
    public Button[] levelButtons;
    // Start is called before the first frame update
    void Start()
    {
        //get level information
        int level = PlayerPrefs.GetInt("Level", 1);

        for(int i = 0; i < level; i++)
        {
            levelButtons[i].enabled = true;
        }

    }

    // Update is called once per frame
    public void LoadLevel(int level)
    {
        GameData.gameLevel = level;

    }
}
