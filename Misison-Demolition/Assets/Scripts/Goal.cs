using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    static public bool goalMet = false;

    void OnTriggerEnter(Collider other)
    {
        //When the trigger is git by something
        //Check to see if its a projectile
        if(other.gameObject.tag == "projectile")
        {
            //If so, set goaltMat to true
            Goal.goalMet = true;

            //Also set the Alpha of the color to higher opacity 
            Material mat = GetComponent<Renderer>().material;
            Color c = mat.color;
            c.a = 1;
            mat.color = c;
        }
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
