using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfCubes2 : MonoBehaviour
{
    private int cubesCount = 10;
    private float cubeSize = 0.1f;
    private float stepSize = 0.5f;
    
    private Material blueMat;
    private Material redMat;
    private Material yellowMat;
    private List<GameObject> cubes = new List<GameObject>();
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blueMat = Resources.Load<Material>("Materials/blueMat");
        redMat = Resources.Load<Material>("Materials/redMat");
        yellowMat = Resources.Load<Material>("Materials/yellowMat");
        
        
        CreateCubes();
        StartCoroutine(BlinkingRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    private void CreateCubes() {

        for (int k = 0; k < 9; k++)
        {
            for (int i = 0; i < cubesCount; i++)
            {
                for (int j = 0; j < cubesCount; j++)
                {

                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
                    cube.transform.position = new Vector3(j * stepSize, i * stepSize, k * stepSize);
                    cube.GetComponent<Renderer>().material = blueMat;

                    cubes.Add(cube);
                }
            }
        }
    }

// The blinking routine: Every 10th cube, between red and yellow, once per second
    private IEnumerator BlinkingRoutine() {
        Debug.Log("Blinking routine started: affecting every 10th cube.");
        
        while (true)
        {
            for (int i = 0; i < cubes.Count; i += 10)
            {
                if (cubes[i] != null)
                {
                    cubes[i].GetComponent<Renderer>().material = redMat;
                }
            }
            Debug.Log("Cubes are now RED.");
            yield return new WaitForSeconds(1f);
            
            for (int i = 0; i < cubes.Count; i += 10)
            {
                if (cubes[i] != null)
                {
                    cubes[i].GetComponent<Renderer>().material = yellowMat;
                }
            }
            Debug.Log("Cubes are now YELLOW.");
            yield return new WaitForSeconds(1f);
        }
    }
}
