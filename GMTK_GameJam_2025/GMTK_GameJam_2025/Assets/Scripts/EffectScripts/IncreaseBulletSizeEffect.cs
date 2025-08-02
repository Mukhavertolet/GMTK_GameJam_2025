using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseBulletSizeEffect : MonoBehaviour, IEffect
{
    public string effectName;
    public string effectDesc;

    public string[] GetNameAndDesc()
    {
        return new string[] { effectName, effectDesc };
    }

    public void ApplyEffect()
    {
        GameManager.gameManager.bulletSize += 5;
        Debug.Log("BULLET SIZE INCREASED");
    }


}
