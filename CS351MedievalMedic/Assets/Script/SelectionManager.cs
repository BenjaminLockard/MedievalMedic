using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    
    public NPCWalk npc;

    public TMP_Dropdown operationsSelector;
    public TMP_Dropdown remediesSelector;
    public TMP_Dropdown otherSelector;

    public GameObject confirmButton;
    public GameObject selectorPanel;
    public GameObject trackerPanel;
    public TMP_Text trackerText;

    public DialogueManager dialogueManager;
    public GameManager gameManager;
    public CountdownTimerScript countdownTimerScript;

    public AudioSource selectAudio;
    public AudioClip selectSound;

    private int treated;
    private int correct;
    private int incorrect;

    private bool isPlayerInput;

    public void enableTracker()
    {
        trackerPanel.SetActive(true);
    }

    public void operationChanged()
    {
        if (isPlayerInput)
        {
            selectAudio.PlayOneShot(selectSound, 0.25f);
            isPlayerInput = false;
            remediesSelector.value = 0;
            otherSelector.value = 0;

            if (operationsSelector.value == 0)
            {
                confirmButton.SetActive(false);
            }
            else
            {
                confirmButton.SetActive(true);
            }

            isPlayerInput = true;
        }
    }

    public void remedyChanged()
    {
        if (isPlayerInput)
        {
            selectAudio.PlayOneShot(selectSound, 0.25f);
            isPlayerInput = false;
            operationsSelector.value = 0;
            otherSelector.value = 0;

            if(remediesSelector.value == 0)
            {
                confirmButton.SetActive(false);
            } else
            {
                confirmButton.SetActive(true);
            }

            isPlayerInput = true;
        }
    }

    public void otherChanged()
    {
        if (isPlayerInput)
        {
            selectAudio.PlayOneShot(selectSound, 0.25f);
            isPlayerInput = false; 
            operationsSelector.value = 0;
            remediesSelector.value = 0;

            if(otherSelector.value == 0)
            {
                confirmButton.SetActive(false);
            } else
            {
                confirmButton.SetActive(true);
            }

                isPlayerInput = true;
        }
    }

    public void makeSelection()
    {
        selectorPanel.SetActive(true);
        confirmButton.SetActive(false);
        operationsSelector.value = 0;
        remediesSelector.value = 0;
        otherSelector.value = 0;

        //npc approaches 
        //npc.Approach();
    }
   
    public void triggerDayEnd()
    {
        selectorPanel.SetActive(false);
        trackerPanel.SetActive(false);
        countdownTimerScript.hideTimer();
        gameManager.dayEnd(treated, correct, incorrect);
        treated = 0;
        correct = 0;
        incorrect = 0;

        //Make npc go away
        StartCoroutine(EndOfDayExit());

    }

    private IEnumerator EndOfDayExit()
    {
        // Make NPC walk away
        npc.Leave();

        // Wait until NPC finishes walking off screen
        yield return new WaitForSeconds(1f); // adjust based on your NPC speed and distance

        // Once gone, reset position for next day quietly
        npc.ResetPosition();

    }

    // THE BIG ONE - ANSWER CHECK - START -----------------------------------------------------------------------------------------------------------
    public void selectionConfirmed()
    {
        selectAudio.PlayOneShot(selectSound, 0.75f);
        // PRAYER SELECTED, HAIL MARY
        if (operationsSelector.value == 0
            && remediesSelector.value == 0
            && otherSelector.value == 2)
        {
            int rando = Random.Range(1, 5);
            if (rando == 4)
            {
                dialogueManager.provideFeedback(true);
                correct++;
                //dialogueManager.correctFeedback();
            } else
            {
                dialogueManager.provideFeedback(false);
                incorrect++;
                //dialogueManager.incorrectFeedback();
            }
        } // ONLY OPERATION SELECTED
        else if (remediesSelector.value == 0
                    && otherSelector.value == 0)
        {
            if (operationsSelector.options[operationsSelector.value].text == dialogueManager.getCondition()) {
                dialogueManager.provideFeedback(true);
                correct++;
                //dialogueManager.correctFeedback();
            } else
            {
                dialogueManager.provideFeedback(false);
                incorrect++;
                //dialogueManager.incorrectFeedback();
            }


        } // ONLY REMEDY SELECTED
        else if (operationsSelector.value == 0
                    && otherSelector.value == 0)
        {
            if (remediesSelector.options[remediesSelector.value].text == dialogueManager.getCondition())
            {
                dialogueManager.provideFeedback(true);
                correct++;
                //dialogueManager.correctFeedback();
            }
            else
            {
                dialogueManager.provideFeedback(false);
                incorrect++;
                //dialogueManager.incorrectFeedback();
            }

        } // ONLY OTHER SELECTED
        else if (operationsSelector.value == 0
                    && remediesSelector.value == 0)
        {
            if (otherSelector.options[otherSelector.value].text == dialogueManager.getCondition())
            {
                dialogueManager.provideFeedback(true);
                correct++;
                //dialogueManager.correctFeedback();
            }
            else
            {
                dialogueManager.provideFeedback(false);
                incorrect++;
                //dialogueManager.incorrectFeedback();
            }

        }

        treated++;
        selectorPanel.SetActive(false);

        //npc leaves and returns
        npc.LeaveAndReturn(1f);

        if (treated >= gameManager.getInjured())
            triggerDayEnd();
    }
    // THE BIG ONE - ANSWER CHECK - END -------------------------------------------------------------------------------------------------------------


    // Start is called before the first frame update
    void Start()
    {
        isPlayerInput = true;
        //makeSelection();
    }

    // Update is called once per frame
    void Update()
    {
        trackerText.text = "Injured:   " + (gameManager.getInjured() - treated) + "\nTreated:  " + treated + "\nDead:     " + (gameManager.getDead() + incorrect);
    }
}
