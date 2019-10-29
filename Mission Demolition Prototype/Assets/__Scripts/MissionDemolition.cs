using System;
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

public class MissionDemolition : MonoBehaviour {
    static private MissionDemolition S;

    [SerializeField] Text uitLevel;
    [SerializeField] Text uitShots;
    [SerializeField] Text uitButton;
    [SerializeField] Vector3 castlePos;
    [SerializeField] GameObject[] castles;

    private int level;
    private int levelMax;
    private int shotsTaken;
    private GameObject castle;
    private GameMode mode = GameMode.idle;
    private string showing = "Show Slingshot";

    // Use this for initialization
    void Start () {
        S = this;
        level = 0;
        levelMax = castles.Length;
        StartLevel();
	}

    private void StartLevel()
    {
        if (castle)
        {
            Destroy(castle);
        }

        foreach (GameObject pTemp in GameObject.FindGameObjectsWithTag("Projectile"))
        {
            Destroy(pTemp);
        }

        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;

        SwitchView("Show Both");
        ProjectileLine.S.Clear();

        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;
    }

    private void UpdateGUI()
    {
        uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;
    }

	// Update is called once per frame
	void Update () {
        UpdateGUI();

        if (mode == GameMode.playing && Goal.goalMet)
        {
            mode = GameMode.levelEnd;
            SwitchView("Show Both");
            Invoke("NextLevel", 2f);
        }
	}

    private void NextLevel()
    {
        if (++level >= levelMax)
        {
            level = 0;
        }
        StartLevel();
    }

    public void SwitchView(string eView = "")
    {
        if (eView == "")
        {
            eView = uitButton.text;
        }

        showing = eView;

        switch(showing)
        {
            case "Show Slingshot":
                FollowCam.POI = null;
                uitButton.text = "Show Castle";
                break;
            case "Show Castle":
                FollowCam.POI = S.castle;
                uitButton.text = "Show Both";
                break;
            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                uitButton.text = "Show Slingshot";
                break;
            default:
                throw new Exception("unknown showing value: " + showing);
        }
    }

    static public void ShotFired()
    {
        S.shotsTaken++;
    }
}
