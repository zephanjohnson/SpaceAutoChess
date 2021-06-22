using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgressionManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GamestateManager _gamestateManager;
    
    //Public for debugging purposes right now, should technically be private
    [SerializeField]
    public int Level = 1;
    
    private void Start()
    {
        DontDestroyOnLoad(this);
        Level = 1;
    }

    public void LoadPlanningPhase()
    {
        SceneManager.LoadScene("Planning");
        _gamestateManager.LoadPreviousGameState();
        Debug.Log("Current level: " + Level);
        //Enter the planning phase, this should call LoadPreviousGameState and transition us to the layout scene
    }

    public void CompleteLevel()
    {
        Level += 1;
        _gamestateManager.SaveCurrentGameState();
        LoadPlanningPhase();
    }

    public void LoseLevel()
    {
        LoadPlanningPhase();
    }

    public void LoadAutoplayPhase()
    {
        SceneManager.LoadScene("Autoplay");
        //Load the autoplayPhase that corresponds to the current level. need to figure out some way to do this that doesn't suck. if we're doing procedural stuff
        //then we need only one level, 
    }
}
