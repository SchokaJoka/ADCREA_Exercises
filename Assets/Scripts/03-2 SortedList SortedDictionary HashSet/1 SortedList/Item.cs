using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, ICollectible
{
    public string itemName;
    public int itemQuantity;
    private GameObject itemObject;
    private ItemProperties item;
    private InventorySystem inventoryScript;

    public struct ItemProperties
    {
        public string name;
        public int quantity;
        public Vector3 position;
        public GameObject objRef;
        
        public ItemProperties(string n, int v, Vector3 pos, GameObject obj)
        {
            this.name = n;
            this.quantity = v;
            this.position = pos;
            this.objRef = obj;
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        item = new ItemProperties(
            itemName, 
            itemQuantity, 
            transform.position, 
            this.gameObject
        );
        
        if(!inventoryScript)
        {
            inventoryScript = FindFirstObjectByType<InventorySystem>();
            
            if (!inventoryScript)
            {
                Debug.LogError("InventorySystem not found in the scene.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ItemProperties GetItem()
    {
        if (itemObject != null)
        {
            itemObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
        return item;
    }
}
