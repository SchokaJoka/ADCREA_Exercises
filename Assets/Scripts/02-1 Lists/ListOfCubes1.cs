using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfCubes1 : MonoBehaviour
{
    
    private int cubesCount = 11;
    private float cubeSize = 0.1f;
    private float startPos;
    private float stepSize;
    
    private List<GameObject> cubes = new List<GameObject>();

    private Material blueMat;
    private Material redMat;
    private Material yellowMat;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stepSize = 0.5f;
        startPos = -5f * stepSize;

        blueMat = Resources.Load<Material>("Materials/blueMat");
        redMat = Resources.Load<Material>("Materials/redMat");
        yellowMat = Resources.Load<Material>("Materials/yellowMat");
        
        StartCoroutine(CreationHandling());


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator CreationHandling()
    {
        CreateCubes();
        yield return new WaitForSeconds(5f);
        CreateCubes2();
        yield return new WaitForSeconds(5f);
        ChangeColor();
        yield break;
    }

    private void CreateCubes()
    {
        for (int i = 0; i < cubesCount; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
            cube.transform.position = new Vector3(startPos + (i * stepSize), 0.0f, 0.0f);
            cube.GetComponent<Renderer>().material = blueMat;

            cubes.Add(cube);
        }
    }

    private void CreateCubes2()
    {
        startPos = 1f * stepSize;
        
        for (int i = 0; i < cubesCount / 2; i++)
        {

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
            cube.transform.position = new Vector3(0.0f, 0.0f, startPos + (i * stepSize));
            cube.GetComponent<Renderer>().material = redMat;

            cubes.Add(cube);
            
            Debug.Log("Cube " + i + ": " + cubes[i].transform.position);
        }
        
        startPos = -1f * stepSize;
        
        for (int i = 0; i < cubesCount / 2; i++)
        {

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
            cube.transform.position = new Vector3(0.0f, 0.0f, startPos + (i * -stepSize));
            cube.GetComponent<Renderer>().material = redMat;

            cubes.Add(cube);
            
            Debug.Log("Cube " + i + ": " + cubes[i].transform.position);
        }
    }
    
    private void ChangeColor()
    {
        for (int i = 0; i < cubes.Count; i += 5)
        {
            cubes[i].GetComponent<Renderer>().material = yellowMat;
        }
    }
}
