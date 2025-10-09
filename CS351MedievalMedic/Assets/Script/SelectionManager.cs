using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    public TMP_Dropdown operationsSelector;
    public TMP_Dropdown remediesSelector;
    public TMP_Dropdown otherSelector;

    public GameObject confirmButton;
    public GameObject selectorPanel;

    public DialogueManager dialogueManager;
    public GameManager gameManager;

    private int treated;
    private int correct;
    private int incorrect;

    public void makeSelection()
    {
        selectorPanel.SetActive(true);
        operationsSelector.value = 0;
        remediesSelector.value = 0;
        otherSelector.value = 0;
    }
   
    
    // THE BIG ONE - ANSWER CHECK - START -----------------------------------------------------------------------------------------------------------
    public void selectionConfirmed()
    {
        treated++;
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
    }
    // THE BIG ONE - ANSWER CHECK - END -------------------------------------------------------------------------------------------------------------


    // Start is called before the first frame update
    void Start()
    {
        //makeSelection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
