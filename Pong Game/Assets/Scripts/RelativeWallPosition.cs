using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativeWallPosition : MonoBehaviour
{
    public bool isLeftWall = true;

    // Start is called before the first frame update
    void Start()
    {
        //check position object of wall if the resolution is changed in other device
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height/2));

        //adjust position for left and right wall.
        if (isLeftWall)
            transform.position = new Vector2(-screenBounds.x, screenBounds.y);
        else
            transform.position = new Vector2(screenBounds.x, screenBounds.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
