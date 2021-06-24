using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public int CountdownFrom = 3;
    private Text _textbox;
    private GamestateManager _gamestateManager;

    private void Awake()
    {
        _gamestateManager = FindObjectOfType<GamestateManager>();
        _textbox = transform.Find("Text").GetComponent<Text>();
    }

    private void Update()
    {
        float time = CountdownFrom - Time.timeSinceLevelLoad;
        var timeStr = time.ToString("0");
        _textbox.text = timeStr == "0" ? "Start!": timeStr;

        if (time <= 0f)
        {
            TimeUp();
        }
    }

    private void TimeUp()
    {
        gameObject.SetActive(false);
        _gamestateManager.State = GameState.Autoplay;
    }
}
