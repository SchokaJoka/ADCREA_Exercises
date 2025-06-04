using TMPro;
using UnityEngine;

public class PlayFieldGame2 : MonoBehaviour
{
    public GameObject textPrefab;
    
    private char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y' };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                int alphabetIndex = i + j;
                
                // 1. Create a new GameObject for each cell in the grid (the cube)
                GameObject cell = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cell.name = "Cube_" + i + "_" + j;
                
                // Set the position of the cell in the grid (world position)
                cell.transform.position = new Vector3(i, 0, j);

                // 2. Instantiate the TextMeshPro prefab and make it a child of the cube.
                GameObject cellTextObj = Instantiate(textPrefab, cell.transform);

                // 3. Adjust the text object's LOCAL position to sit on top of the cube
                cellTextObj.transform.localPosition = new Vector3(0, 0.6f, 0); 

                // 4. Get the TextMeshPro component from the INSTANTIATED TEXT OBJECT
                TextMeshPro textMeshProComponent = cellTextObj.GetComponent<TextMeshPro>();
                // textMeshProComponent.fontSize = 0.5f; // Set the font size for better visibility
                
                // 5. Check if the component exists and set its text
                if (textMeshProComponent != null)
                {
                    textMeshProComponent.text = alphabet[alphabetIndex].ToString();
                }
                else
                {
                    Debug.LogError("TextMeshPro component not found on textPrefab! Make sure your prefab has a Text (TMP) component attached.");
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
