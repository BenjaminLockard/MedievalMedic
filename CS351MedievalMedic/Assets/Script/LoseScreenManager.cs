/*
    Author: Lenny Ozaeta
    Assignment: Team Project (Medieval Medic)
    Description: Manages the lose screen
    Initially Created: Sunday, 10/12/25
        Modified: Tuesday, 10/14/25
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed to manage scene
using TMPro; // Needed to use TMP_Text

public class LoseScreenManager : MonoBehaviour
{
    private int index;
    // Following are all set in Inspector
    public TMP_Text loseTextbox;
    public string[] sentences;
    public float typingSpeed;

    // Start dialog coroutine
    private void OnEnable()
    {
        StartCoroutine(Type());
    }

    // Coroutine (types one letter at a time)
    IEnumerator Type()
    {
        loseTextbox.text = ""; // Start textbox as empty
        foreach (char letter in sentences[index]) // Loop through each letter in message
        {
            loseTextbox.text += letter;
            yield return new WaitForSeconds(typingSpeed); // Wait before continuing executing coroutine
        }
    }

    // This function is called when the "Main Menu" button is clicked
    public void Lose_RestartGame()
    {
        SceneManager.LoadScene("TitleScreen"); // Loads title scene
    }

    // This function is called when the "Quit" button is clicked
    public void Lose_QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in Editor
        #else
        Application.Quit(); // Closes build
        #endif
    }
}
