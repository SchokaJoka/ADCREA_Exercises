using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Make sure you have TextMeshPro imported

public class AlphabetGridGenerator : MonoBehaviour
{
    [Header("Grid Settings")]
    public GameObject cellPrefab;
    public int gridSize = 5;
    public float squareSize = 1.0f;

    [Header("Cell Appearance (Optional)")]
    public bool applyColorsToPrefab = true;
    public Color color1 = Color.white;
    public Color color2 = Color.black;

    [Header("Lettering in Prefab")]
    public float textObjectScale = 4.0f;
    public Color textColor = Color.red;
    
    public Dictionary<char, Vector3> charLocation = new Dictionary<char, Vector3>();
    private Queue<char> charQueue = new Queue<char>();
    private Material yellowMat;
    private GameObject player;
    
    void Start()
    {
        if (cellPrefab == null)
        {
            Debug.LogError("Cell Prefab not assigned! Please assign a prefab in the Inspector.");
            this.enabled = false; // Disable the script if no prefab is set
            return;
        }
        
        yellowMat = Resources.Load<Material>("Materials/yellowMat");
        GenerateGrid();

        GeneratePlayer();
        
        StartCoroutine(ProcessQueue());

        
        foreach(var kvp in charLocation)
        {
            Debug.Log($"Character: {kvp.Key}, Position: {kvp.Value}");
        }
    }

    void Update()
    {
        if (Input.inputString.Length > 0)
        {
            foreach (char c in Input.inputString)
            {
                // Check if the character is a lowercase letter
                if (char.IsLetter(c) && char.IsLower(c))
                {
                    Debug.Log($"Lowercase letter pressed: '{c}'");
                    
                    charQueue.Enqueue(char.ToUpper(c));
                }
            }
        }
    }

    private IEnumerator ProcessQueue()
    {
        while (true)
        {
            if (charQueue.Count > 0)
            {
                yield return new WaitForSeconds(1.0f);
                char currentChar = charQueue.Dequeue();
                Debug.Log($"Processing character: {currentChar}");

                if (charLocation.TryGetValue(currentChar, out Vector3 location))
                {
                    Debug.Log($"Found location for '{currentChar}': {location}");
                    MovePlayer(location);

                }
                else
                {
                    Debug.Log($"Lowercase letter '{currentChar}' pressed, but no location mapped for it.");
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    void MovePlayer(Vector3 location)
    {
        player.transform.position = new Vector3(location.x, player.transform.position.y, location.z);
    }
    
    void GeneratePlayer()
    {
        // Create a player GameObject at the center of the grid
        player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        player.name = "Player";
        charLocation.TryGetValue('M', out Vector3 startPosition);
        player.transform.position = new Vector3(startPosition.x, 1.0f, startPosition.z);
        player.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        player.GetComponent<Renderer>().material = yellowMat;
    }
    
    void GenerateGrid()
    {
        char currentLetter = 'A';
        float gridHalfExtent = (gridSize * squareSize) / 2f; // Half the total width/depth of the grid
        float cellCenterOffset = squareSize / 2f;            // Offset to center each cell

        for (int z_row = 0; z_row < gridSize; z_row++)
        {
            for (int x_col = 0; x_col < gridSize; x_col++)
            {
                float cellXPos = (x_col * squareSize) - gridHalfExtent + cellCenterOffset;
                float cellZPos = gridHalfExtent - (z_row * squareSize) - cellCenterOffset;

                Vector3 cellPosition = new Vector3(cellXPos, 0, cellZPos);

                // Instantiate Prefab as a child of this GameObject
                GameObject cellInstance = Instantiate(cellPrefab, this.transform);
                cellInstance.transform.localPosition = cellPosition;
                cellInstance.transform.localScale = new Vector3(squareSize, squareSize, squareSize);
                
                // Add the character's position to the dictionary
                charLocation[currentLetter] = cellInstance.transform.position;
                
                // --- Optional: Apply alternating colors to the prefab's renderer ---
                if (applyColorsToPrefab)
                {
                    Renderer rend = cellInstance.GetComponentInChildren<Renderer>();
                    if (rend != null)
                    {
                        if ((x_col + z_row) % 2 == 0)
                        {
                            rend.material.color = color1;
                        }
                        else
                        {
                            rend.material.color = color2;
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"No Renderer found in prefab instance '{cellInstance.name}' to apply color to cell ({x_col},{z_row}).");
                    }
                }

                // --- Find TextMeshPro component in prefab instance and set the letter ---
                TextMeshPro tmpro = cellInstance.GetComponentInChildren<TextMeshPro>(true); // true includes inactive GameObjects

                if (tmpro != null)
                {
                    tmpro.text = currentLetter.ToString();
                    tmpro.transform.localScale = Vector3.one * textObjectScale;
                    tmpro.color = textColor;
                }
                else
                {
                    Debug.LogWarning($"No TextMeshPro component found in prefab instance '{cellInstance.name}' for cell ({x_col},{z_row}). Ensure your prefab has one.");
                }

                // Increment letter, stopping at 'y' for a 5x5 grid
                if (currentLetter < 'Y')
                {
                    currentLetter++;
                }
            }
        }
    }
}