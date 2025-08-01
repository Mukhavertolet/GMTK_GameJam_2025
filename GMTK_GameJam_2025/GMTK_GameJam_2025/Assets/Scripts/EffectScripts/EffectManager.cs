using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager effectManager;


    public delegate void Condition();
    public event Condition conditionSPACE;
    public event Condition conditionZ;


    public List<IEffect> effects = new List<IEffect>();


    private void Awake()
    {
        effectManager = this;
    }

    private void Start()
    {
        conditionSPACE += AddEffect(new FlatDamageAddEffect()).ApplyEffect;
        conditionSPACE += AddEffect(new FlatDamageAddEffect()).ApplyEffect;


        conditionSPACE += AddEffect(new IncreaseBulletSizeEffect()).ApplyEffect;

    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    conditionSPACE();
        //}
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    conditionZ();
        //}

    }
    public IEffect AddEffect(IEffect effect)
    {
        effects.Add(effect);
        return effect;
    }

    public void OnSpace()
    {
        conditionSPACE();
    }

}
