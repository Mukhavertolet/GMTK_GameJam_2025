using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatHealthAddEffect : MonoBehaviour, IEffect
{
    public string effectName;
    public string effectDesc;

    public string[] GetNameAndDesc()
    {
        return new string[] { effectName, effectDesc };
    }
    public void ApplyEffect()
    {
        Debug.Log($"FlatHEALTHAddEffect applied when was met!");
    }
}
