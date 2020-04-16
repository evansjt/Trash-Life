using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> inventoryItems = new List<InventoryItem>();

    int currentInventoryIndex = 0;

    private void Update()
    {
        HandleScrollWheel();
    }

    void HandleScrollWheel()
    {
        float val = Input.GetAxis("Mouse ScrollWheel");

        if(val < 0f)
        {
            currentInventoryIndex++;
            if(currentInventoryIndex >= inventoryItems.Count)
            {
                currentInventoryIndex = 0;
            }
            print(currentInventoryIndex);
        } else if (val > 0f)
        {
            currentInventoryIndex--;
            if(currentInventoryIndex < 0)
            {
                currentInventoryIndex = inventoryItems.Count - 1;
            }
            print(currentInventoryIndex);
        }
    }
}
