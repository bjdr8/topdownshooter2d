using System.Collections.Generic;
using UnityEngine;


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

        if (activeEffects != null)
        {
            foreach (var effect in rawEffects)
            {
                foreach (var aEffect in activeEffects)
                {
                    if (effect.nameId == aEffect && effect is BaseEffect passive)
                    {
                        _effects.Add(passive);
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
