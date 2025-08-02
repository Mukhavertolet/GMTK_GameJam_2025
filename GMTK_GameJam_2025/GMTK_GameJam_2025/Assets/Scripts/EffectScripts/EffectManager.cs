using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager effectManager;


    public delegate void Condition();
    public event Condition conditionShoot;
    public event Condition conditionZ;


    public List<IEffect> effects = new List<IEffect>();


    private void Awake()
    {
        effectManager = this;
    }

    private void Start()
    {
        //    conditionSPACE += AddEffect(new FlatDamageAddEffect()).ApplyEffect;
        //    conditionSPACE += AddEffect(new FlatDamageAddEffect()).ApplyEffect;


        //    conditionSPACE += AddEffect(new IncreaseBulletSizeEffect()).ApplyEffect;

    }

    private void Update()
    {
        Debug.Log(effects.Count);
    }
    public IEffect AddEffect(IEffect effect, string condition)
    {
        SubscribeTo(condition, effect);

        effects.Add(effect);
        return effect;
    }

    public void OnShoot()
    {
        if (conditionShoot != null)
            conditionShoot();
    }

    public void SubscribeTo(string condition, IEffect effect)
    {
        switch (condition)
        {
            case "OnShoot":
                conditionShoot += effect.ApplyEffect;
                break;
        }
    }

}


