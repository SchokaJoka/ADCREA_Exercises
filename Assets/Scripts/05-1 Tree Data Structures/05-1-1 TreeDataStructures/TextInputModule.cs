using UnityEngine;
using TMPro;
using System;

public class TextInputModule : MonoBehaviour
{
    public event Action<string> AddNewNodeToTree;

    private Vector3 displayPosition = new Vector3(0, 0, -2);
    private Vector3 displaySize = new Vector3(4, 0.4f, 0.3f);

    private GameObject displayBox;
    private GameObject displayCanvas;
    private GameObject textObject;
    private TextMeshPro textMesh;

    private string currentInput = "";   //  Keyboard input


    private void Start()
    {
        CreateTextDisplayBox();
    }


    private void Update()
    {
        HandleKeyboardInput();
    }


    private void CreateTextDisplayBox()
    {
        // Create the display box
        displayBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
        displayBox.transform.SetParent(transform);
        displayBox.transform.localPosition = displayPosition;
        displayBox.transform.localScale = displaySize;
        displayBox.transform.localRotation = Quaternion.Euler(-45, 0, 0); // Rotated 45° upward along X-axis

        displayCanvas = new GameObject("DisplayCanvas");
        displayCanvas.transform.SetParent(displayBox.transform);
        displayCanvas.transform.localPosition = new Vector3(0, 0.55f, 0);

        textObject = new GameObject("TextMeshPro_Display");
        textObject.transform.SetParent(displayCanvas.transform);
        textObject.transform.localPosition = new Vector3(0, 0, 0);
        textObject.transform.localRotation = Quaternion.Euler(45, 0, 0); // Counteract box rotation

        // Add TextMeshPro component
        textMesh = textObject.AddComponent<TextMeshPro>();
        textMesh.text = "Type here for node name";
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.fontSize = 3;
        textMesh.autoSizeTextContainer = true;
        textMesh.rectTransform.sizeDelta = new Vector2(3f, 0.4f); // Matching box width and height
    }


    private void HandleKeyboardInput()
    {
        foreach (char c in Input.inputString)
        {
            if (char.IsLetter(c) || char.IsNumber(c) || c == ' ')
            {
                currentInput += c;
                textMesh.text = currentInput;
            }
            else if (c == '\b' && currentInput.Length > 0) // Backspace
            {
                currentInput = currentInput.Substring(0, currentInput.Length - 1);
                textMesh.text = currentInput;
            }
            else if (c == '\n' || c == '\r') // Enter key
            {
                if (!string.IsNullOrEmpty(currentInput))
                {
                    Debug.Log("Adding word: " + currentInput);
                    AddNewNodeToTree?.Invoke(currentInput);
                    currentInput = "";
                    textMesh.text = "";
                }
            }
        }
    }

}
