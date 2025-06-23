using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public SortedList<string, Item.ItemProperties> inventory = new SortedList<string, Item.ItemProperties>();
    public GameObject inventoryTextObj;
    private TextMeshPro inventoryText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryText = inventoryTextObj.GetComponentInChildren<TextMeshPro>();
        if (inventoryText == null)
        {
            Debug.LogError("InventoryText not found in the scene.");
        }

        inventoryText.text = "Inventory:\n Empty";
        
        
        // Create a new cube with item script
        GameObject Cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Cube1.name = "Item1";
        Cube1.transform.position = new Vector3(4, 0, 4);
        Item item1 = Cube1.AddComponent<Item>();
        item1.itemName = "Health Potion";
        item1.itemQuantity = 10;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void HandleCollectible(ICollectible collectible)
    {
        Item.ItemProperties item = collectible.GetItem();
        if (!inventory.ContainsKey(item.name))
        {
            inventory.Add(item.name, item);
        }
        else
        {
            // Erhöhe die Anzahl und aktualisiere ggf. die Position und das GameObject
            Item.ItemProperties existing = inventory[item.name];
            existing.quantity += item.quantity;
            existing.position = item.position;
            existing.objRef = item.objRef;
            inventory[item.name] = existing;
        }

        Debug.Log($"Collected Item: {item.name}, Quantity: {inventory[item.name].quantity}");
        
        UpdateInventoryText();
    }

    public void UpdateInventoryText()
    {
        inventoryText.text = "Inventory:\n";
        
        if (inventory.Count == 0)
        {
            inventoryText.text += "Empty";
        }
        else
        {
            foreach (KeyValuePair<string, Item.ItemProperties> kvp in inventory)
            {
                inventoryText.text += $"{kvp.Key} | Quantity: {kvp.Value.quantity} | Last Pos: {kvp.Value.position}\n";
            }
        }
    }
}
