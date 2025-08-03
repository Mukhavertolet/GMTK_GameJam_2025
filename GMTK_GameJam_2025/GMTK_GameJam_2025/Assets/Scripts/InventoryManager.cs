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

    public string chooseSound;
    public string pickSound;


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


                GameManager.gameManager.audioManager.Play(chooseSound, UnityEngine.Random.Range(0.9f, 1.1f));

                //chosenItem.GetComponent<Item>().outline.Color = Color.green;

                choice = false;
                return;
            }

            GameManager.gameManager.currentRoom.leftItem = null;

            items.Add(selectedItem.GetComponent<Item>());
            AddItemEffects(selectedItem.GetComponent<Item>());

            GameManager.gameManager.audioManager.Play(pickSound, UnityEngine.Random.Range(0.9f, 1.1f));

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
