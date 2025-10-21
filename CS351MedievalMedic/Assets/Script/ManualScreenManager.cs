/*
    Author: Lenny Ozaeta
    Assignment: Team Project (Medieval Medic)
    Description: Manages the screen that takes user to a manual that defines game's treatment options
    Initially Created: Monday, 10/20/25
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed to manage scene

public class ManualScreenManager : MonoBehaviour
{
    // This function is called when the "Main Menu" button is clicked
    public void BackToTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen"); // Loads main game scene
    }
}
