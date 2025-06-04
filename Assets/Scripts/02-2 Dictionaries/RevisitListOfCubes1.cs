using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevisitListOfCubes1 : MonoBehaviour
{
    private int arrDimension = 40;
    private float cubeSize = 0.1f;
    private float stepSize = 0.5f;
    private bool useRed = true;
    
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
    }

    // Update is called once per frame
    void Update()
    {
        ChangeRandomCubesColor();
    }
    
    
    private void CreateCubes() {

        for (int k = 0; k < arrDimension; k++)
        {
            for (int i = 0; i < arrDimension; i++)
            {
                for (int j = 0; j < arrDimension; j++)
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

    private void ChangeRandomCubesColor() {
        for (int k = 0; k < 100; k++)
        {
            int randomIndex = Random.Range(0, cubes.Count - 1);
            GameObject cube = cubes[randomIndex];

            if (useRed)
            {
                cube.GetComponent<Renderer>().material = redMat;
            }
            else
            {
                cube.GetComponent<Renderer>().material = yellowMat;
            }
        }
        useRed = !useRed;
    }
}
