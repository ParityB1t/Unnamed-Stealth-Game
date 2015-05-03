﻿using UnityEngine;

public class Loot : MonoBehaviour
{

    private readonly string _interTag = "Lootable";
    private bool _canLoot;
    private int _lootableId;
    private GameObject _destroyable;
    private const KeyCode _loot = KeyCode.E;
    private GameObject _inventory;

    void Awake()
    {
        _inventory = GameObject.FindGameObjectWithTag("Inventory");
    }

    /**
     * Player will loot item in E is pressed
     */
    void Update()
    {

        if (_canLoot && Input.GetKeyDown(_loot))
        {
            LootItem();
        }
    }

    /*
     * Adds the item into inventory and destroy it in the game world
     */
    private void LootItem()
    {
        _inventory.GetComponent<InventoryLogic>().AddItem(_lootableId);
        Destroy(_destroyable);
        _canLoot = false;
    }

    /*
     * If collides with an lootable item, allow looting
     */
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == _interTag)
        {
            _canLoot = true;            
            _lootableId = col.gameObject.GetComponent<Identifer>().GetIdentity().ItemId;           
            _destroyable = col.gameObject;
        }
    }

    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == _interTag)
        {
            _canLoot = false;
        }
    }
}
