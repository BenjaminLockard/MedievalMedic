using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for UI Text
using TMPro; // Uncomment if using TextMeshPro and replace 'Text' with 'TextMeshProUGUI'

public class CountdownTimerScript : MonoBehaviour
{
    public SelectionManager selectionManager;
    public DialogueManager dialogueManager;

    private float currentTime;
    //public Text timerText; // Assign your UI Text element in the Inspector
    public TextMeshProUGUI timerText; // Use this if using TextMeshPro
    public GameObject timerPanel;

    public void resetTimer()
    {
        currentTime = 180;
        timerPanel.SetActive(true);
    }

    public void hideTimer() {
        timerPanel.SetActive(false);
    }

    void Start()
    {
        currentTime = 999;
    }

    void Update()
    {
        if (currentTime > 0 && currentTime < 999)
        {
            currentTime -= Time.deltaTime; // Subtract time elapsed since last frame
            DisplayTime(currentTime);
        }
        else if (currentTime != 999)
        {
            timerPanel.SetActive(false);

            currentTime = 999; // Stop the timer
            DisplayTime(currentTime);

            // Trigger an event when the timer reaches zero (e.g., game over, level end)
            selectionManager.triggerDayEnd();
            dialogueManager.abortDialogue();
            // You can add more actions here, like loading a new scene or showing a game over screen.
            
            //enabled = false; // Disable the script to stop updating the timer
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
