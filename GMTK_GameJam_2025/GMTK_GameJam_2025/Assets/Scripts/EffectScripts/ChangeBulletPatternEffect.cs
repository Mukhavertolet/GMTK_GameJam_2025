using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBulletPatternEffect : MonoBehaviour, IEffect
{
    public string effectName;
    public string effectDesc;

    public string condition;

    public GameObject BulletPattern;

    public string[] GetNameAndDesc()
    {
        return new string[] { effectName, effectDesc };
    }

    public void ApplyEffect()
    {
        GameManager.playerInstance.bulletPattern[0] = BulletPattern;
        Debug.Log($"bullet pattern changed to {BulletPattern}");
    }

    public string GetCondition()
    {
        return condition;
    }
}
