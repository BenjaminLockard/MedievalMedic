/*
    Author: Lenny Ozaeta
    Assignment: Team Project (Medieval Medic)
    Description: Manages the title screen
    Initially Created: Sunday, 10/12/25
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed to manage scene

public class TitleScreenManager : MonoBehaviour
{
    // This function is called when the "Start" button is clicked
    public void StartGame()
    {
        SceneManager.LoadScene("MedivalMedicStarter"); // Loads main game scene
    }

    // This function is called when the "Quit" button is clicked
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in Editor
        #else
        Application.Quit(); // Closes build
        #endif
    }
}
