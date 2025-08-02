using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private Color defaultColor;



    public string itemName = "Item";

    public int itemLevel = 1;

    public List<IEffect> effects = new List<IEffect>();
    public int effectCounter = 0;

    public List<string[]> effectNames = new List<string[]>();

    public List<string> conditions = new List<string>();


    private void Start()
    {
        defaultColor = spriteRenderer.color;



        foreach (var effect in GetComponents<IEffect>())
        {
            effects.Add(effect);
            effectNames.Add(effect.GetNameAndDesc());
            conditions.Add(effect.GetCondition());
        }

        effectCounter = effects.Count;

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>() != null)
        {
            InventoryManager.inventory.selectedItem = this.gameObject;
            spriteRenderer.color = Color.white;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null && InventoryManager.inventory.selectedItem == this.gameObject)
        {
            InventoryManager.inventory.selectedItem = null;
            spriteRenderer.color = defaultColor;
        }
    }



}
