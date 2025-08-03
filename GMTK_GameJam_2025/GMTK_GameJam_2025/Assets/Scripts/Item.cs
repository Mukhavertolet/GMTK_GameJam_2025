using OutlineFx;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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



    public TMP_Text itemNameText;
    public TMP_Text itemDescText;
    public TMP_Text itemLevelText;

    public GameObject canvas;

    public OutlineFx.OutlineFx outline;


    private void Start()
    {
        defaultColor = spriteRenderer.color;

        outline = GetComponentInChildren<OutlineFx.OutlineFx>();
        outline.enabled = false;
        itemNameText.text = itemName;
        itemDescText.text = "";
        foreach (var effect in GetComponents<IEffect>())
        {
            itemDescText.text += effect.GetNameAndDesc()[1] + "\n";
        }
        canvas.SetActive(false);


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>() != null)
        {
            InventoryManager.inventory.selectedItem = this.gameObject;
            //spriteRenderer.color = Color.white;

            outline.enabled = true;
            canvas.SetActive(true);
            itemLevelText.text = "Level: " + itemLevel.ToString();

            effects.Clear();
            foreach (var effect in GetComponents<IEffect>())
            {
                effect.SetItemLevel(itemLevel);
                effects.Add(effect);
                effectNames.Add(effect.GetNameAndDesc());
                conditions.Add(effect.GetCondition());
            }

            effectCounter = effects.Count;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null && InventoryManager.inventory.selectedItem == this.gameObject)
        {
            InventoryManager.inventory.selectedItem = null;
            //spriteRenderer.color = defaultColor;

            outline.enabled = false;
            canvas.SetActive(false);
        }
    }



}
