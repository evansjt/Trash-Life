using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] Transform rightHand;
    [SerializeField] Transform leftHand;

    public List<InventoryItem> inventoryItems = new List<InventoryItem>();

    InventoryItem currentItem;

    int currentInventoryIndex = 0;

    private void Start()
    {
        if(inventoryItems.Count != 0)
        {
            currentItem = inventoryItems[currentInventoryIndex];
            currentItem.SpawnObject(rightHand, leftHand, GetComponent<Animator>());
        }
    }

    private void Update()
    {
        HandleScrollWheel();
    }

    void HandleScrollWheel()
    {
        if (inventoryItems.Count == 0) return;

        float val = Input.GetAxis("Mouse ScrollWheel");

        if(val < 0f)
        {
            currentInventoryIndex++;
            if(currentInventoryIndex >= inventoryItems.Count)
            {
                currentInventoryIndex = 0;
            }
        } else if (val > 0f)
        {
            currentInventoryIndex--;
            if(currentInventoryIndex < 0)
            {
                currentInventoryIndex = inventoryItems.Count - 1;
            }
        }

        if(!Mathf.Approximately(val, 0f))
        {
            currentItem = inventoryItems[currentInventoryIndex];
            currentItem.SpawnObject(rightHand, leftHand, GetComponent<Animator>());
            print(currentItem.GetItemName());
        }
    }

    public InventoryItem GetItem()
    {
        return currentItem;
    }
}
