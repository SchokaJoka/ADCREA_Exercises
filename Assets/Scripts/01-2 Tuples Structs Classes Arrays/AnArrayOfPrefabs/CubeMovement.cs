using System;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    private float offset;
    private float amplitude;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = UnityEngine.Random.Range(0.1f, 2f);
        amplitude = UnityEngine.Random.Range(1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        float position = (float)Math.Sin(Time.time + offset); 
        transform.position = new Vector3(transform.position.x, 0, position * amplitude);
    }
}
