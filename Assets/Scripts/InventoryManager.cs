using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu;
    private bool menuActivated;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetButtonDown("Inventory") && menuActivated)
        {
            inventoryMenu.SetActive(false);
            menuActivated = false;

        }

        else if(Input.GetButtonDown("Inventory") && !menuActivated)
        {
            inventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        Debug.Log("itemMame = " + itemName + "quantity = " + quantity + "itemSprite = " + itemSprite);
    }
}
