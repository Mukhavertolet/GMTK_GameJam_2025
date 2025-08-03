using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager inventory;

    public List<Item> items;

    public GameObject selectedItem;

    public GameObject chosenItem;
    public bool choice;

    private void Start()
    {
        inventory = GetComponent<InventoryManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && selectedItem != null)
        {
            if (chosenItem == null && choice)
            {
                chosenItem = selectedItem;
                GameManager.gameManager.currentRoom.RemoveDroppedItemsExcept(chosenItem);
                choice = false;
                return;
            }

            GameManager.gameManager.currentRoom.leftItem = null;

            items.Add(selectedItem.GetComponent<Item>());
            AddItemEffects(selectedItem.GetComponent<Item>());

            selectedItem.SetActive(false);
            selectedItem = null;
        }
    }


    public void AddItemEffects(Item item)
    {
        foreach (var effect in item.effects)
        {
            EffectManager.effectManager.AddEffect(effect, effect.GetCondition());
        }
    }


}
