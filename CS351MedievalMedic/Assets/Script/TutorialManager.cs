using System.Collections;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [Header("Tutorial UI")]


    public GameObject[] tutorialBoxes; // Assign in inspector
    public float typingSpeed;
    private bool firstRound = true;

    public bool isFirstRound()
    {
        return firstRound;
    }

    public void disableTutorial()
    {
        firstRound = false;
    }

    // Show specific tutorial box by index with typing effect
    public void ShowTutorialBox(int index, string message)
    {
        if (index < 0 || index >= tutorialBoxes.Length)
        {
            Debug.LogWarning("Invalid tutorial box index: " + index);
            return;
        }
        StartCoroutine(TypeTextCoroutine(index, message));
    }


    // Hide specific tutorial box by index
    public void HideTutorialBox(int index)
    {
        if (index < 0 || index >= tutorialBoxes.Length)
        {
            Debug.LogWarning("Invalid tutorial box index: " + index);
            return;
        }
        tutorialBoxes[index].SetActive(false);
    }

    // Coroutine to  typing effect for a specific box
    private IEnumerator TypeTextCoroutine(int index, string message)
    {
        GameObject box = tutorialBoxes[index];
        TMP_Text textComponent = box.GetComponentInChildren<TMP_Text>();
        if (textComponent == null)
        {
            Debug.LogError($"No TMP_Text found in tutorial box at index {index}");
            yield break;
        }

        box.SetActive(true);
        textComponent.text = "";

        foreach (char letter in message)
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // ======= Example dedicated methods for each dialog box =======
    public void ShowIntro()
    {
        ShowTutorialBox(0, "Welcome, medic. Your army is in battle and you must help heal your soldiers.");
    }

    public void HideIntro()
    {
        HideTutorialBox(0);
    }

    public void ShowStories()
    {
        ShowTutorialBox(1, "Each soldier will explain their ailment to you with a story. \nSome stories will be clearer than others.");
    }

    public void HideStories()
    {
        HideTutorialBox(1);
    }

    public void ShowTreatments()
    {
        ShowTutorialBox(2, "Use your knowledge to select a treatment from this list of options. \nIf you choose correctly, you will heal them. \nIf you choose incorrectly, they will die.");
    }

    public void HideTreatments()
    {
        HideTutorialBox(2);
    }

    public void ShowTracker()
    {
        ShowTutorialBox(3, "Keep an eye on the tracker above. \n\nIt shows how many soldiers need attention, how many you've seen, and how many you've lost.");
    }

    public void HideTracker()
    {
        HideTutorialBox(3);
    }

    public void ShowTimer()
    {
        ShowTutorialBox(4, "Time is of the essence. \n\nThis timer shows how much time you have to treat soldiers each day.");
        firstRound = false;
    }

    public void HideTimer()
    {
        HideTutorialBox(4);
    }
}