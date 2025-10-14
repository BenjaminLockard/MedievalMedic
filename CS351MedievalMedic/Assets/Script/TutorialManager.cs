using System.Collections;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [Header("Tutorial UI")]


    public GameObject[] tutorialBoxes; // Assign in inspector
    public float typingSpeed = 0.05f;

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
        ShowTutorialBox(0, "Welcome, medic. Your army is in battle and you must help heal your team of soldiers. They will come into your medic tent one at a time and you must listen to them and choose the appropriate treatment. Press space to continue");
    }

    public void HideIntro()
    {
        HideTutorialBox(0);
    }

    public void ShowStories()
    {
        ShowTutorialBox(1, "Each soldier will explain their injury to you with a story. Press space to continue");
    }

    public void HideStories()
    {
        HideTutorialBox(1);
    }

    public void ShowTreatments()
    {
        ShowTutorialBox(2, "Use your knowledge to select a treatment from the selector of options. If you choose correctly, you will heal them, if you choose incorrectly they will get hurt. Press space to continue");
    }

    public void HideTreatments()
    {
        HideTutorialBox(2);
    }

    public void ShowTracker()
    {
        ShowTutorialBox(3, "Keep an eye on the tracker above. It shows how many you've saved—or lost. Press space to continue");
    }

    public void HideTracker()
    {
        HideTutorialBox(3);
    }

    public void ShowTimer()
    {
        ShowTutorialBox(4, "Time is of the essence, there is a timer that counts down from 3 minutes. Press space to end tutorial");
    }

    public void HideTimer()
    {
        HideTutorialBox(4);
    }
}