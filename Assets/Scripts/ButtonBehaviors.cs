using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviors : MonoBehaviour
{
    // Start is called before the first frame update

    private GamestateManager _gamestateManager;
    void Start()
    {
        _gamestateManager = (GamestateManager) GameObject.FindObjectOfType(typeof(GamestateManager));
    }

    // Update is called once per frame
    public void CallCompleteLevel()
    {
        _gamestateManager.CompleteLevel();
    }
    
    public void CallLoadAutoplayPhase()
    {
        _gamestateManager.LoadAutoplayPhase();
    }

    public void CallLoseLevel()
    {
        _gamestateManager.LoseLevel();
    }
}
