using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{

    private int day;
    private int injured;
    private int dead;

    private string output;
    public float typeSpeed;
    public TMP_Text dayEndText;
    public GameObject dayEndPanel;

    public DialogueManager dialogueManager;
    public SelectionManager selectionManager;
    public CountdownTimerScript timer;


    public GameObject nextButton;
    // 0 during day end changes, 1 during day end summary, 2 for day start endstart
    public int nextButtonMode;

    IEnumerator Type()
    {
        nextButton.SetActive(false);

        dayEndText.text = "";
        foreach (char letter in output)
        {
            dayEndText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
        nextButton.SetActive(true);
    }

    // getters if numbers needed elsewhere
    public int getInjured()
    {
        return injured;
    }
    public int getDead()
    {
        return dead;
    }

    // initialize / reset values for day #1
    public void startFresh()
    {
        day = 1;
        nextButtonMode = 1;
        dayEndPanel.SetActive(true);
        dayStart();
    }

    public void dayEnd(int treated, int correct, int incorrect)
    {
        nextButtonMode = 0;

        output = "Day #" + day + " Summary\n---------------------------------------------\nTreated: " + treated + "\nInjured: " + injured + " - " + (correct + incorrect) + " (all choices)\nDead: " + dead + " + " + incorrect + " (incorrect choices)";

        // update data
      
        injured -= correct + incorrect;
        dead += incorrect;

        dayEndPanel.SetActive(true);
        StartCoroutine(Type());
    }

    public void dayStart()
    {
        if (nextButtonMode == 0)
        {
            output = "Day #" + day + " Results\n---------------------------------------------\n\nInjured: " + injured + "\nDead: " + dead;
            day += 1;

            nextButtonMode = 1;
            StartCoroutine(Type());

        } else if (nextButtonMode == 1) {
            int newlyInjured = (8 + 2 * day) + Random.Range(day * 1, day * 3);
            int newlyDead = Random.Range(day * 1, day * 3);
            output = "Day #" + day + " Start\n---------------------------------------------\n\nInjured: " + injured + " + " + newlyInjured + " (new injuries)\nDead: " + dead + " + " + newlyDead + " (new casualties)";
            injured += newlyInjured;
            dead += newlyDead;

            nextButtonMode = 2;
            StartCoroutine(Type());

        } else {
            nextButton.SetActive(false);
            dayEndPanel.SetActive(false);

            timer.resetTimer();
            selectionManager.enableTracker();
            dialogueManager.promptUser();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        startFresh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
