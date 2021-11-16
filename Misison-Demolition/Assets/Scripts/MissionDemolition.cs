using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{

    static private MissionDemolition S; // A private Singleton

    [Header("Set in Inspector:")]
    public Text uitLevel; // The UIText_Level Text
    public Text uiShots; //THe UIText_Shots Text
    public Text uiButton; // The Text on UIButton_View
    public Vector3 castlePos; //THe place to put castles
    public GameObject[] castles; //An array of the castles

    [Header("Set Dynamically")]
    public int level; //The current level
    public int levelMax; //The number of levels
    public int shotsTaken;
    public GameObject castle; //The current castle
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot"; //FollowCam mode

    // Start is called before the first frame update
    void Start()
    {
        S = this; //Define the Slingshot

        level = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
    {
        //Get rid of the old castle if one exists
        if (castle != null)
        {
            Destroy(castle);
        }

        //Destroy old projectile of they exist
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }

        //Instatiate the new castle
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;

        //Reset the camera
        ProjectileLine.S.Clear();

        //Reset the goal
        Goal.goalMet = false;

        //UpdateGUI();

        mode = GameMode.playing;
    }

    //void UpdateGUI()
    //{
    //    //Show the data in the GUITexts
    //    uitLevel.text = "Level:" + (level+1) + "of"+levelMax;
    //    uiShots.text = "Shots Taken:"+shotsTaken;
    //}

    // Update is called once per frame
    void Update()
    {
        //UpdateGUI();

        ////Check for level end
        //if((mode == GameMode.playing) && Goal.goalMet)
        //{
        //    //Change mode to stop checking for level end
        //    mode = GameMode.levelEnd;

        //    //Start the next level in2 seconds
        //    Invoke("NextLevel", 2f); 
        //}
    }

    void NextLevel()
    {
        level++;
        if(level == levelMax) 
        {
            level = 0;
        }
        StartLevel();
    }

    //Static method that allows code anywhere to increment shotsTaken
    public static void ShotFired()
    {
        S.shotsTaken++;
    }
}

