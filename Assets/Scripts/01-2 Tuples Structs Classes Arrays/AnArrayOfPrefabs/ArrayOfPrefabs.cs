using System;
using System.Collections;
using UnityEngine;


public class ArrayOfPrefabs : MonoBehaviour
{
    private int cubesCount = 11;
    private float cubeSize = 0.1f;
    private GameObject[] cubes;
    private Material[] materials = new Material[2];
    private bool isBlue = true;
    private float startPos;
    private float stepSize;
    private float[] amplitudes;
    public GameObject cubePrefab;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cubes = new GameObject[cubesCount];
        amplitudes = new float[cubesCount];
        stepSize = cubeSize * 2f;
        startPos = -5f * stepSize;

        materials[0] = Resources.Load<Material>("Materials/blueMat");
        materials[1] = Resources.Load<Material>("Materials/redMat");

        for (int i = 0; i < cubesCount; i++)
        {
            // cubes[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            // cubes[i].transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
            cubes[i] = Instantiate(cubePrefab);
            cubes[i].transform.position = new Vector3(startPos + (i * stepSize), 0.0f, 0.0f);
            cubes[i].GetComponent<Renderer>().material = materials[0];
            amplitudes[i] = UnityEngine.Random.Range(0.5f, 1.5f);
        }
        
        

        StartCoroutine(ToggleColor());

    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator ToggleColor()
    {
        yield return new WaitForSeconds(1f);
        
        for (int i = 0; i < cubesCount; i++)
        {
            if (isBlue)
            {
                cubes[i].GetComponent<Renderer>().material = materials[1];
            }
            else
            {
                cubes[i].GetComponent<Renderer>().material = materials[0];
            }
        }
        isBlue = !isBlue;


        StartCoroutine(ToggleColor());
    }
}
