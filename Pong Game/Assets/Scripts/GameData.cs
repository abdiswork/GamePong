using System.Collections;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static int[] ballSpeed = { 3, 3, 3, 4, 4, 4, 5, 5, 5};
    public static int[] paddleSpeed = { 8, 8, 8, 7, 7, 7, 6, 6, 6};
    public static int[] obstacleNumbers = { 10, 10, 10, 12, 12, 14, 15, 16, 17};
    public static int[] specialObstacle = {0, 1, 2, 3, 4, 5, 6, 7, 9};

    public static int gameLevel=1;
}
