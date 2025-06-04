using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedListOfCubes3 : MonoBehaviour
{
    private int cubesCount = 10;
    private float cubeSize = 0.1f;
    private float stepSize = 0.5f;
    
    private Material blueMat;
    private Material redMat;
    private Material yellowMat;
    private LinkedList<GameObject> cubes = new LinkedList<GameObject>();
    
    
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

                    cubes.AddLast(cube);
                }
            }
        }
    }

// The blinking routine: Every 10th cube, between red and yellow, once per second
    private IEnumerator BlinkingRoutine() {
        Debug.Log("Blinking routine started: affecting every 10th cube.");
        
        while (true)
        {
            int index = 0;
            foreach (GameObject cube in cubes)
            {
                index++;
                if (index % 10 == 0 && cube != null)
                {
                    cube.GetComponent<Renderer>().material = redMat;
                }
            }
            yield return new WaitForSeconds(1f);
            
            index = 0;
            foreach (GameObject cube in cubes)
            {
                index++;
                if (index % 10 == 0 && cube != null)
                {
                    cube.GetComponent<Renderer>().material = yellowMat;
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
