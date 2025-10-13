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
        npc.Approach();
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

        // Deactivate NPC and reset position for next time
        npc.ResetPosition();

    }
    
    // THE BIG ONE - ANSWER CHECK - START -----------------------------------------------------------------------------------------------------------
    public void selectionConfirmed()
    {
        // PRAYER SELECTED, HAIL MARY
        if (operationsSelector.value == 0
            && remediesSelector.value == 0
            && otherSelector.value == 2)
        {
            int rando = Random.Range(1, 4);
            if (rando == 4)
            {
                correct++;
                //dialogueManager.correctFeedback();
            } else
            {
                incorrect++;
                //dialogueManager.incorrectFeedback();
            }
        }
        else if (operationsSelector.value == 0
            && remediesSelector.value == 0
            && otherSelector.value == 0)
        { // ALL DROPDOWNS BLANK (auto fail)
            incorrect++;
            // dialogueManager.emptyFeedback();
        
        } // ONLY OPERATION SELECTED
        else if (remediesSelector.value == 0
                    && otherSelector.value == 0)
        {
            if (operationsSelector.options[operationsSelector.value].text == dialogueManager.getCondition()) {
                correct++;
                //dialogueManager.correctFeedback();
            } else
            {
                incorrect++;
                //dialogueManager.incorrectFeedback();
            }


        } // ONLY REMEDY SELECTED
        else if (operationsSelector.value == 0
                    && otherSelector.value == 0)
        {
            if (remediesSelector.options[remediesSelector.value].text == dialogueManager.getCondition())
            {
                correct++;
                //dialogueManager.correctFeedback();
            }
            else
            {
                incorrect++;
                //dialogueManager.incorrectFeedback();
            }

        } // ONLY OTHER SELECTED
        else if (operationsSelector.value == 0
                    && remediesSelector.value == 0)
        {
            if (otherSelector.options[otherSelector.value].text == dialogueManager.getCondition())
            {
                correct++;
                //dialogueManager.correctFeedback();
            }
            else
            {
                incorrect++;
                //dialogueManager.incorrectFeedback();
            }

        } // MULTIPLE SELECTIONS (auto fail)
        else
        {
            incorrect++;
            // dialogueManager.excessFeedback();
        }

        treated++;
        selectorPanel.SetActive(false);
        dialogueManager.dialoguePanel.SetActive(false);

        //npc leaves and returns
        npc.LeaveAndReturn(2f);

        if (treated >= gameManager.getInjured())
        {
            triggerDayEnd();
        } else
        {
            // Reactivate and bring NPC back for next option
            npc.gameObject.SetActive(true);
            npc.Approach();

            dialogueManager.promptUser();
        }
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
