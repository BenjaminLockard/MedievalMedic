using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public SelectionManager selectionManager;
    public GameManager gameManager;

    private string currentCondition;

    public string getCondition()
    {
        return currentCondition;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        currentCondition = "Amputation";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
