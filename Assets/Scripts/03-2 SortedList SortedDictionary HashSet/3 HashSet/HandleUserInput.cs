using UnityEngine;
using TMPro;

public class HandleUserInput : MonoBehaviour
{
    private SearchNames searchNames;
    private TextMeshPro inputField;
    private string currentInput = "";
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputField = gameObject.GetComponent<TextMeshPro>();
        searchNames = FindFirstObjectByType<SearchNames>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (char c in Input.inputString)
        {
            if (char.IsLetter(c))
            {
                currentInput += c;
                inputField.text = currentInput;
            }
            else if (c == '\b' && currentInput.Length > 0)
            {
                currentInput = currentInput.Substring(0, currentInput.Length - 1);
                inputField.text = currentInput;
            }
            else if (c == '\n' || c == '\r') // Enter gedrückt
            {
                searchNames.CheckAndAddName(currentInput);
                currentInput = "";
                inputField.text = currentInput;
            }
        }
    }
}
