# GamePong

Pong 2D game consists of 9 levels that designed in array as follow:

public static int[] ballSpeed = { 3, 3, 3, 4, 4, 4, 5, 5, 5};//set ball speed
public static int[] paddleSpeed = { 8, 8, 8, 7, 7, 7, 6, 6, 6};//set paddle speed
public static int[] obstacleNumbers = { 10, 10, 10, 12, 12, 14, 15, 16, 17}; //number of obstacle spawn
public static int[] specialObstacle = {0, 1, 2, 3, 4, 5, 6, 7, 9}; //spawn special obstacle in x time
public static int[] PowerUpsTime = {25, 25, 25, 25, 20, 20, 20, 15, 10}; //gives power up spawn for x time

This game includes : 
● Unity Physics engine usage
● Basic scene setup
● Basic level design with at least over 10 bricks laid out

Game Overview :
● The objective of any level of the game is to remove all bricks from the view without
losing all lives, by controlling the constantly bouncing ball.
● From level start, the ball is in perpetual motion, constantly bouncing off the walls.
● Player lives can be lost if the user fails to make the ball bounce on the controller platform, so as not to allow the ball to pass through to the abyss. 
● A player will have a maximum of three lives to finish the level, if the lives are exhausted, the user can then choose to replay the level.
● The ball will bounce off all borders(left, right and top) except the bottom one.
● The ball will bounce off the platform and the bricks in space, any brick off which a ball has bounced, shall be destroyed.
● There are two bricks variant, one time hit to be destroyed and three times hits to be destroyed
● There is power up, shield, that can be used to make the ball immune from hitting the Abyss
● The game is designed with 9 levels where each level can be setup easily from ball speed, paddle speed, obstacle numbers, special obstacle, power up time.

Preview of the game can be seen at this folder or this link:
https://github.com/abdiswork/GamePong/blob/main/pongtrailervideo.mp4