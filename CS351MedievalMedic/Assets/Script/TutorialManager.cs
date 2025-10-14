using System.Collections;
using UnityEngine;
using TMPro;  // Added this for TMP_Text

public class TutorialManager : MonoBehaviour
{
    [Header("Tutorial UI")]

    public GameObject[] tutorialBoxes; // Assign UI in Inspector
    public float typingSpeed = 0.05f;  // Match to GameManager
    public bool hasRunTutorial = false;



void Start()
{
    StartTutorial();
}

    public void SetTutorialComplete(bool value)
    {
        hasRunTutorial = value;
    }

    private Coroutine currentTypingCoroutine;

    // tutorialstarts

    public void StartTutorial()
    {
        if (hasRunTutorial) return;
        StartCoroutine(RunTutorialSequence());
    }

    private IEnumerator RunTutorialSequence()
    {
        // STEP 1: Introduction
        yield return ShowTutorialBox(0, "Welcome, medic. Your army is currently at battle, and your mission is to treat injured soldiers. Press space to continue ");
        yield return WaitForContinue();
        HideTutorialBox(0);

        // STEP 2: Stories
        yield return ShowTutorialBox(1, "Each soldier will tell their story and explain what their injury is. Press space to continue");
        yield return WaitForContinue();
        HideTutorialBox(1);

        // STEP 3: Choosing treatments
        yield return ShowTutorialBox(2, "Use your knowledge to select the best treatment from the list presented to you. You will click the treatment you want to choose from the selector buttons. Press space to continue");
        yield return WaitForContinue();
        HideTutorialBox(2);

        // STEP 4: Tracker
        yield return ShowTutorialBox(3, "Keep an eye on the tracker above. It shows how many soldiers you've saved—or lost. You will also recieve an end of day counter showing your progress. Press space to continue");
        yield return WaitForContinue();
        HideTutorialBox(3);

        // STEP 5: Timer
        yield return ShowTutorialBox(4, "Time is of the essence. There is a timer on the top of the screen counting down from 3 minutes. Press space to finish tutorial");
        yield return WaitForContinue();
        HideTutorialBox(4);

        EndTutorial();
    }

    private IEnumerator WaitForContinue()
    {
        // Waits for player to press Space to proceed (you can change this if needed)
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
    }

    //  TYPING EFFECT 
    private IEnumerator ShowTutorialBox(int index, string message)
    {
        if (index < 0 || index >= tutorialBoxes.Length) yield break;

        GameObject box = tutorialBoxes[index];
        TMP_Text textComponent = box.GetComponentInChildren<TMP_Text>();

        if (textComponent == null)
        {
            Debug.LogError($"No TMP_Text component found in tutorial box at index {index}");
            yield break;
        }

        box.SetActive(true);
        textComponent.text = "";

        foreach (char letter in message.ToCharArray())
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    //  HIDING
    public void HideTutorialBox(int index)
    {
        if (index < 0 || index >= tutorialBoxes.Length) return;
        tutorialBoxes[index].SetActive(false);
    }

    // FINISH TUTORIAL 
    public void EndTutorial()
    {
        hasRunTutorial = true;
        foreach (GameObject box in tutorialBoxes)
        {
            box.SetActive(false);
        }
    }

    // EXTERNAL ACCESS
    public bool HasTutorialRun()
    {
        return hasRunTutorial;
    }

    public void SetTutorialRun(bool value)
    {
        hasRunTutorial = value;
    }
}