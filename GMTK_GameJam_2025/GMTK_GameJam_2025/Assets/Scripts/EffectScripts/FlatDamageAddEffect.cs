using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatDamageAddEffect : MonoBehaviour, IEffect
{
    public string effectName;
    public string effectDesc;

    public string[] GetNameAndDesc()
    {
        return new string[] { effectName, effectDesc };
    }
    public void ApplyEffect()
    {
        GameManager.gameManager.damage += 2;
        Debug.Log($"FlatDamageAddEffect applied when was met!");
    }
}
