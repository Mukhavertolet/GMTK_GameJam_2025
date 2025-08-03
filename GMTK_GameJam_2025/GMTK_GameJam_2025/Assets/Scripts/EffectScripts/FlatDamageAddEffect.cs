using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EffectManager;

public class FlatDamageAddEffect : MonoBehaviour, IEffect
{
    public string effectName;
    public string effectDesc;

    public string condition;

    public int value;
    public int loop;

    public string[] GetNameAndDesc()
    {
        return new string[] { effectName, effectDesc };
    }
    public void ApplyEffect()
    {
        GameManager.playerInstance.damage += value * loop;
        Debug.Log($"FlatDamageAddEffect applied when was met!");
    }
    public string GetCondition()
    {
        return condition;
    }
    public void SetItemLevel(int level)
    {
        loop = level;
    }
}
