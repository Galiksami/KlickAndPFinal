using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{

    public GameObject[] inventory; // The player's inventory array
    public int inventorySize = 5;  // Maximum size of the inventory

    private void Start()
    {
        // Initialize the inventory with the specified size
        inventory = new GameObject[inventorySize];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Define the layer mask for the pickup objects
            int layerMask = LayerMask.GetMask("UI");

            // Cast the ray with the layer mask
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                // Check the distance and add to inventory
                if (Vector3.Distance(transform.position, hit.transform.position) <= 2f)
                {
                    AddToInventory(hit.collider.gameObject);
                }
            }
        }
    }

    private void AddToInventory(GameObject item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null) // We found an empty slot in the inventory
            {
                inventory[i] = item; // Add the item to the inventory
                item.SetActive(false); // Hide the item from the game world (or handle as needed)
                break; // We've added the item, no need to check further slots
            }
        }
    }
}
