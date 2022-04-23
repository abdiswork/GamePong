using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBehaviour : MonoBehaviour
{
    //brick can hold being hit by ball in "doggedness" time
    public int doggedness = 1; //default set up by 1.

    public void BrickCheck()
    {
       
        // decrease endurance if being hit
        doggedness--;

        //check if should be destroyed
        if (doggedness == 0)
            Destroy(gameObject);
        
    }


}
