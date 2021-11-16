using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    static public GameObject POI; //Static point of interest

    [Header("Set in Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    [Header("Set Dynamically")]
    public float camZ; // The desired Z pos of the camera 

    void Awake()
    {
        camZ = this.transform.position.z;
    }

    void FixedUpdate()
    {
        Vector3 destination;

        //If there is no POI, return to P: [0,0,0]
        if (POI == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            //Get the position of the POI
            destination = POI.transform.position;

            //Of POI is a projectile, check to see if its at rest
            if (POI.tag == "Projectile")
            {
                // if it is sleeping (that is, not moving)
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    //Return to default view
                    POI = null;

                    // in the next update
                    return;
                }
            }
        }
        //if theres only one line following an if, it doesnt need braces
        // if (POI == null) return;*/ //return if there is no poi

        //Get the position of the poi
        //Vector3 destination = POI.transform.position;

        //Limit the X & Y to minimum values
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);

        //Interlope from the current camera position toward the destination
        destination = Vector3.Lerp(transform.position, destination, easing);

        //Force destination.z to be camZ to keep the camera far enough away
        destination.z = camZ;

        //set the camera to the destination
        transform.position = destination;

        //Set the orthographicSize of the camera to keep ground view 
        Camera.main.orthographicSize = destination.y = 10;

        //Limit the X & Y to minimum values
        destination.x = Mathf.Max(minXY.x, destination.x);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
