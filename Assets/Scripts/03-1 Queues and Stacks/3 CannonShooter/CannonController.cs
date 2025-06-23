using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Main cannon controller with stack-based object pooling
public class CannonController : MonoBehaviour
{
    [Header("Cannon Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public int poolSize = 20;
    public float fireRate = 0.2f;
    public float rotationSpeed = 90f;
    
    [Header("Debug Info")]
    public int bulletsInPool;
    public int bulletsActive;
    
    private Stack<Bullet> bulletPool;
    private float lastFireTime;
    private bool isFiring;
    
    void Start()
    {
        InitializeBulletPool();
    }
    
    void Update()
    {
        HandleInput();
        UpdateDebugInfo();
    }
    
    void InitializeBulletPool()
    {
        bulletPool = new Stack<Bullet>();
        
        // Create initial bullets and add them to the stack
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bulletObj = Instantiate(bulletPrefab);
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            
            if (bullet == null)
            {
                bullet = bulletObj.AddComponent<Bullet>();
            }
            
            // Ensure bullet has Rigidbody
            if (bulletObj.GetComponent<Rigidbody>() == null)
            {
                Rigidbody rb = bulletObj.AddComponent<Rigidbody>();
                rb.useGravity = true;
            }
            
            // Ensure bullet has Collider
            if (bulletObj.GetComponent<Collider>() == null)
            {
                SphereCollider collider = bulletObj.AddComponent<SphereCollider>();
                collider.radius = 0.1f;
            }
            
            bulletObj.SetActive(false);
            bulletPool.Push(bullet);
        }
        
        Debug.Log($"Bullet pool initialized with {poolSize} bullets");
    }
    
    void HandleInput()
    {
        // Rotate cannon with arrow keys
        float horizontal = Input.GetAxis("Horizontal");
        
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, 0);
        }
        
        // Handle firing
        isFiring = Input.GetKey(KeyCode.Space);
        
        if (isFiring && CanFire())
        {
            Fire();
        }
    }
    
    bool CanFire()
    {
        return bulletPool.Count > 0 && Time.time >= lastFireTime + fireRate;
    }
    
    void Fire()
    {
        if (bulletPool.Count == 0)
        {
            Debug.Log("No bullets available in pool!");
            return;
        }
        
        // Get bullet from stack
        Bullet bullet = bulletPool.Pop();
        
        // Position bullet at fire point
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        
        // Initialize and fire bullet
        Vector3 fireDirection = firePoint.forward;
        bullet.Initialize(fireDirection, this);
        
        lastFireTime = Time.time;
        
        Debug.Log($"Fired bullet! Bullets remaining in pool: {bulletPool.Count}");
    }
    
    public void ReturnBullet(Bullet bullet)
    {
        // Return bullet to the stack
        bulletPool.Push(bullet);
        Debug.Log($"Bullet returned to pool. Bullets available: {bulletPool.Count}");
    }
    
    void UpdateDebugInfo()
    {
        bulletsInPool = bulletPool.Count;
        bulletsActive = poolSize - bulletsInPool;
    }
    
    void OnGUI()
    {
        // Simple UI for debugging
        GUI.Box(new Rect(10, 10, 200, 100), "Cannon Stats");
        GUI.Label(new Rect(20, 30, 180, 20), $"Bullets in Pool: {bulletsInPool}");
        GUI.Label(new Rect(20, 50, 180, 20), $"Active Bullets: {bulletsActive}");
        GUI.Label(new Rect(20, 70, 180, 20), $"Firing: {(isFiring ? "Yes" : "No")}");
        
        GUI.Box(new Rect(10, 120, 200, 60), "Controls");
        GUI.Label(new Rect(20, 140, 180, 20), "Arrow Keys: Rotate");
        GUI.Label(new Rect(20, 160, 180, 20), "Space: Fire");
    }
}