using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Skill System/Passive Effect")]
public class PassiveEffect
{
    public List<BaseEffect> rawEffects;

    private List<BaseEffect> _effects;

    public List<string> activeEffects = new List<string>();

    public bool applied = false;

    public PassiveEffect(List<BaseEffect> list)
    {
        rawEffects = list;
    }

    public void Init(List<string> ActiveEffects)
    {
        _effects = new List<BaseEffect>();
        activeEffects = ActiveEffects;
        //for (int i = 0; i < ActiveEffects.Count; i++)
        //{
        //    Debug.Log(activeEffects[i] + " active effect");
        //}

        //for (int i = 0; i < rawEffects.Count; i++)
        //{
        //    Debug.Log(rawEffects[i] + "raw effect");
        //}
        //Debug.Log(rawEffects.Count);

        if (activeEffects != null)
        {
            foreach (var effect in rawEffects)
            {
                foreach (var aEffect in activeEffects)
                {
                    //Debug.Log("raw effect Name Id" + effect.nameId + "effect name" + aEffect);
                    if (effect is BaseEffect passivetest)
                    {
                        //Debug.Log("effect is a base effect");
                    }
                    if (effect.nameId == aEffect && effect is BaseEffect passive)
                    {
                        _effects.Add(passive);
                        //Debug.Log(_effects.Count + "effects active"); ////// pls check the passive effects still don't get applied right.
                    }
                }
            }
        }
    }

    public void Apply(PlayerControler player)
    {
        if (_effects != null && applied == false)
        {
            applied = true;
            foreach (var effect in _effects)
            {
                effect.ApplyEffect(player);
                Debug.Log(effect.nameId + " Active");
            }
        }
    }

    public void Revert(PlayerControler player)
    {
        if (_effects != null && applied == true)
        {
            applied = false;
            foreach (var effect in _effects)
            {
                effect.RevertEffect(player);
            }
        }
    }
}
