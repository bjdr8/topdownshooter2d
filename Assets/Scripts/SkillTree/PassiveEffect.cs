using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill System/Passive Effect")]
public class PassiveEffect : ScriptableObject
{
    public List<ScriptableObject> rawEffects;

    private List<IAppliable> _effects;

    public void Init()
    {
        _effects = new List<IAppliable>();

        foreach (var effect in rawEffects)
        {
            if (effect is IAppliable strategy)
            {
                _effects.Add(strategy);
            }
        }
    }

    public void Apply(PlayerProfile player)
    {
        foreach (var effect in _effects)
        {
            effect.ApplyEffect();
        }
    }

    public void Revert(PlayerProfile player)
    {
        foreach (var effect in _effects)
        {
            effect.RevertEffect();
        }
    }
}
