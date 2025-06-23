using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Bullet behavior script
public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 10f;
    public float lifetime = 10f;
    
    private Rigidbody rb;
    private float timer;
    private CannonController cannon;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void OnEnable()
    {
        timer = 0f;
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            ReturnToPool();
        }
    }
    
    public void Initialize(Vector3 direction, CannonController cannonRef)
    {
        cannon = cannonRef;
        rb.linearVelocity = direction * speed;
        gameObject.SetActive(true);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        // Return bullet to pool when it hits something
        ReturnToPool();
    }
    
    void ReturnToPool()
    {
        if (cannon != null)
        {
            cannon.ReturnBullet(this);
        }
        gameObject.SetActive(false);
    }
}