using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatDamageAddEffect : IEffect
{
    public void ApplyEffect()
    {
        GameManager.gameManager.damage += 2;
        Debug.Log($"FlatDamageAddEffect applied when was met!");
    }
}
