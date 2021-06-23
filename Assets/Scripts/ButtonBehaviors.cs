using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviors : MonoBehaviour
{
    // Start is called before the first frame update

    private LevelProgressionManager _levelProgressionManager;
    void Start()
    {
        _levelProgressionManager = (LevelProgressionManager) GameObject.FindObjectOfType(typeof(LevelProgressionManager));
    }

    // Update is called once per frame
    public void CallCompleteLevel()
    {
        _levelProgressionManager.CompleteLevel();
    }
    
    public void CallLoadAutoplayPhase()
    {
        _levelProgressionManager.LoadAutoplayPhase();
    }

    public void CallLoseLevel()
    {
        _levelProgressionManager.LoseLevel();
    }
}
