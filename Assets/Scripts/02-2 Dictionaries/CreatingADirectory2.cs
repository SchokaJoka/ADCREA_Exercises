using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreatingADirectory2 : MonoBehaviour
{
    private Dictionary<string, string> CapitalCities = new Dictionary<string, string>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CapitalCities.Add("United States", "Washington D.C.");
        CapitalCities.Add("Canada", "Ottawa");
        CapitalCities.Add("Mexico", "Mexico City");
        CapitalCities.Add("Brazil", "Brasília");
        CapitalCities.Add("Argentina", "Buenos Aires");
        CapitalCities.Add("United Kingdom", "London");
        CapitalCities.Add("France", "Paris");
        CapitalCities.Add("Germany", "Berlin");
        CapitalCities.Add("Italy", "Rome");
        CapitalCities.Add("Spain", "Madrid");
        CapitalCities.Add("China", "Beijing");
        CapitalCities.Add("India", "New Delhi");
        CapitalCities.Add("Japan", "Tokyo");
        CapitalCities.Add("South Korea", "Seoul");
        CapitalCities.Add("Australia", "Canberra");
        CapitalCities.Add("Egypt", "Cairo");
        CapitalCities.Add("Nigeria", "Abuja");
        CapitalCities.Add("South Africa", "Pretoria");
        CapitalCities.Add("Russia", "Moscow");
        CapitalCities.Add("Switzerland", "Bern");


        /* ==== Example of accessing a value ==== */
        
        string currentKey = "United States";
        // Debug.Log("keyindexing = Capital of United States: " + CapitalCities[currentKey]);
        // Debug.Log("getvalueordefault = Capital of United States: " + CapitalCities.GetValueOrDefault(currentKey));

        if (CapitalCities.TryGetValue(currentKey, out string value))
        {
            // Debug.Log("trygetvalue = Capital of " + currentKey + ": " + value);
        }
        
        /* ==== Examples of Removing items ==== */
    
        // Remove one key-value pair using the key
        string keyToRemove = "United States";
        CapitalCities.Remove(keyToRemove);
        Debug.Log("Removed " + keyToRemove);
        
        // Remove one key-value pair using the value
        string valueToRemove = "Ottawa";
        string keyfromValueToRemove = "";
        
        foreach (var kvp in CapitalCities)
        {
            if (kvp.Value == valueToRemove)
            {
                keyfromValueToRemove = kvp.Key;
                Debug.Log("Found key for value " + valueToRemove + ": " + keyfromValueToRemove);
                break;
            }
        }

        if (keyfromValueToRemove == null)
        {
            Debug.Log("Value not found in the dictionary.");
        }
        else
        {
            CapitalCities.Remove(keyfromValueToRemove);
            Debug.Log("Removed key-value pair with value " + valueToRemove + " and key " + keyfromValueToRemove);
        }
        
        // Check if the key-value pairs were successfully removed
        bool FoundNonExistingPair = false;
        foreach (var kvp in CapitalCities)
        {
            if (kvp.Key == keyToRemove || kvp.Value == valueToRemove)
            {
                FoundNonExistingPair = true;
                break;
            }
        }
        if (FoundNonExistingPair)
        {
            Debug.LogWarning("Still found non existing key-value pair");
        }
        else
        {
            Debug.Log("Successfully removed key-value pairs");
        }
        
        // Print all remaining key-value pairs
        foreach(var kvp in CapitalCities)
        { 
            Debug.Log("foreach = Country: " + kvp.Key + ", Capital: " + kvp.Value);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
