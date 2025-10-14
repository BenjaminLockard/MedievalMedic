/*
    Author: Lenny Ozaeta
    Assignment: Team Project (Medieval Medic)
    Description: Manages the lose screen
    Initially Created: Sunday, 10/12/25
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed to manage scene

public class LoseScreenManager : MonoBehaviour
{
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
