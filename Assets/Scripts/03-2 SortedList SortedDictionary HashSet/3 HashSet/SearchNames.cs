using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SearchNames : MonoBehaviour
{
    
    public HashSet<string> names;
    private TextMeshPro outputText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        outputText = GetComponentInChildren<TextMeshPro>();
        outputText.text = "TEST";
        
        names = new HashSet<string> {
            "Lukas", "Mia", "Leon", "Emma", "Paul", "Hannah", "Ben", "Sofia", "Finn", "Anna",
            "Elias", "Marie", "Noah", "Lina", "Luis", "Lea", "Felix", "Emilia", "Max", "Lara",
            "Jonas", "Mila", "Henry", "Ella", "Moritz", "Clara", "Julian", "Luisa", "David", "Nina",
            "Tim", "Laura", "Tom", "Ida", "Jan", "Amelie", "Jannik", "Charlotte", "Simon", "Frieda",
            "Philipp", "Sophie", "Matteo", "Maja", "Samuel", "Greta", "Oskar", "Johanna", "Anton", "Isabella"
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckAndAddName(string inputName)
    {
        if (names.Contains(inputName))
        {
            outputText.text = "Sorry – this is already in memory";
        }
        else
        {
            names.Add(inputName);
            outputText.text = $"Success – '{inputName}' added to memory";
        }
    }
}
