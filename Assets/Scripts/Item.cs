using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string itemName;

   [SerializeField]
    private int quantity;

    [SerializeField]
    private Sprite sprite;

    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    private void OnCollisonEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            inventoryManager.AddItem(itemName, quantity, sprite);
            Destroy(gameObject);
        }
    }
}
