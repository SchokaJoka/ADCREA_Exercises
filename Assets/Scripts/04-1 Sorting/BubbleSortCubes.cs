using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BubbleSortCubes : MonoBehaviour
{
    public int zOffset = 10;
    // private string sortingAlgorithm = "Bubble Sort";

    private const int numberOfCubes = 100; // Number of cubes
    private const float cubeSize = 0.1f; // Cube size (10cm)
    private const float spacing = 0.1f; // Spacing between cubes
    public float iretationDelay = 0.001f; // Delay between swaps

    // public GameObject tmParent; // Text to display label
    public GameObject[] cubeArray; // Array with references to cubes
    private bool isSorting = false; // Flag sorting in progress

    private int numComparisons = 0;
    private int numSwaps = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateRandomCubes();
        // AddLabel(sortingAlgorithm);
        Debug.Log("Press Space to start Bubble Sort!");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isSorting)
        {
            StartCoroutine(BubbleSort());
            Debug.Log("Bubble Sort started!");
        }
    }

    private void CreateRandomCubes()
    {
        cubeArray = new GameObject[numberOfCubes];
        
        for (int i = 0; i < numberOfCubes; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.name = "Cube_" + i;                                                    // Name change
            cube.transform.localScale = Vector3.one * cubeSize;                         // Scale cube
            float randomY = Random.Range(0f, 10f);                                      // Random height

            cube.transform.position = new Vector3(i * spacing, randomY / 2, zOffset);
            cube.transform.localScale = new Vector3(cubeSize, randomY, cubeSize);
            float hue = (float) i / numberOfCubes;
            Color rainbowColor = Color.HSVToRGB(hue, 1f, 1f);
            cubeArray[i] = cube;                                                        // Store reference
        }
    }

    private void SwapCubes(int arrayIndex1, int arrayIndex2)
    {
        // Swap cubes in array
        /*
         GameObject tempCube = cubeArray[arrayIndex1];
         cubeArray[arrayIndex1] = cubeArray[arrayIndex2];
         cubeArray[arrayIndex2] = tempCube;
        */
        (cubeArray[arrayIndex1], cubeArray[arrayIndex2]) = (cubeArray[arrayIndex2], cubeArray[arrayIndex1]);
        
        // Swap x-coordinates of first cube to match array
        cubeArray[arrayIndex1].transform.localPosition = new Vector3(
            arrayIndex1 * spacing,
            cubeArray[arrayIndex1].transform.localPosition.y,
            cubeArray[arrayIndex1].transform.localPosition.z
            );
        
        // Swap x-coordinates of second cube to match array
        cubeArray[arrayIndex2].transform.localPosition = new Vector3(
            arrayIndex2 * spacing,
            cubeArray[arrayIndex2].transform.localPosition.y,
            cubeArray[arrayIndex2].transform.localPosition.z
            );
        
        // Number of Swaps + 1
        numSwaps++;
    }

    private IEnumerator BubbleSort()
    {
        isSorting = true;
        
        // Outer loop for each cube position
        for (int i = 0; i < cubeArray.Length - 1; i++)
        {
            // Inner loop for comparing adjacent cubeArray
            for (int j = 0; j < cubeArray.Length - i - 1; j++)
            {
                if (cubeArray[j].transform.position.y > cubeArray[j + 1].transform.position.y)
                {
                    SwapCubes(j, j + 1);
                    yield return new WaitForSeconds(iretationDelay);    // Delay for visualization
                }
                numComparisons++;
            } // End of inner loop
        } // End of outer loop
        
        isSorting = false;
        // UpdateLabel(sortingAlgorithm);
    }
}
