using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public SelectionManager selectionManager;
    public GameManager gameManager;


    private string output;
    public float typeSpeed;
    public TMP_Text dialogueText;
    public GameObject dialoguePanel;

    IEnumerator Type()
    {
        dialogueText.text = "";
        foreach (char letter in output)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
        selectionManager.makeSelection();
    }

    // Will sync up correct treatment w/ corresponding trio of stories
    private string currentCorrect;

    public string getCondition()
    {
        return currentCorrect;
    }


    private string[] treatmentOptions = new string[] {"Stitches", "Extraction", "Bone Setting", "Amputation", "Cauterization", 
                                                       "Medicinal Herbs", "Ointment", "Antiseptic", "Leeches", "Maggots",
                                                       "Rest", "Bandage", "Incense", };
    // ALL STORIES - START ------------------------------------------------------------------------------------------------------------

    // Let Ben handle this mess...
    // Replace each element soldiers' story dialogue, see data sheet for some ideas
    private string[,] allStories = { 
        {"Stitches Serious", "Stitches Silly", "Stitches Vague"},
        {"Extraction Serious", "Extraction Silly", "Extraction Vague"},
        {"Bone Setting Serious", "Bone Setting Silly", "Bone Setting Vague"},
        {"Amputation Serious", "Amputation Silly", "Amputation Vague"},
        {"Cauterization Serious", "Cauterization Silly", "Cauterization Vague"},
        {"Medicinal Herbs Serious", "Medicinal Herbs Silly", "Medicinal Herbs Vague"},
        {"Ointment Serious", "Ointment Silly", "Ointment Vague"},
        {"Antiseptic Serious", "Antiseptic Silly", "Antiseptic Vague"},
        {"Leeches Serious", "Leeches Silly", "Leeches Vague"},
        {"Maggots Serious", "Maggots Silly", "Maggots Vague"},
        {"Rest Serious", "Rest Silly", "Rest Vague"},
        //No row for Prayer, random chance of success w/ any condition
        {"Bandage Serious", "Bandage Silly", "Bandage Vague"},
        {"Incense Serious", "Incense Silly", "Incense Vague"},
        //No row for Bloodletting, always fails
    };

    // ALL STORIES - END --------------------------------------------------------------------------------------------------------------
    
    public void promptUser()
    {
        int randomIndex = Random.Range(0, 12);
        currentCorrect = treatmentOptions[randomIndex];
        output = allStories[randomIndex, Random.Range(0, 2)];
        //AFTER IMPLEMTATION - CALL DAY END & ENGAGE LOOP
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
