using UnityEngine;

public class Player : MonoBehaviour
{
    private InventorySystem inventoryScript;
    
    public float moveSpeed = 0.5f;
    private Vector3 movement;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryScript = FindFirstObjectByType<InventorySystem>();
        if (!inventoryScript)
        {
            Debug.LogError("InventorySystem not found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get WASD input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        movement = new Vector3(moveX, 0, moveZ);
        
        transform.position += movement * (moveSpeed * Time.fixedDeltaTime);
    }
    
    public void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision detected with: " + other.gameObject.name);
        ICollectible collectible = other.gameObject.GetComponent<ICollectible>();
        if (collectible != null)
        {
            inventoryScript.HandleCollectible(collectible);
        }
        else
        {
            Debug.LogWarning("The collided object does not implement ICollectible.");
        }
    }
}
