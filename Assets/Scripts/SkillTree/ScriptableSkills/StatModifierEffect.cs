using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StatModifierEffect", menuName = "Skill System/Effects/Stat Modifier")]
public class StatModifierEffect : BaseEffect
{
    public string statType;
    public float value;

    public override void ApplyEffect(PlayerControler player)
    {
        ModifyStat(player, value);
    }

    public override void RevertEffect(PlayerControler player)
    {
        ModifyStat(player, -value);
    }

    private void ModifyStat(PlayerControler player, float amount)
    {
        switch (statType)
        {
            case "movementSpeed":
                player.movementSpeed += amount;
                break;
            case "maxHp":
                player.maxHp += (int)amount;
                break;
            case "hpRegen":
                player.hpRegen += (int)amount;
                break;
            // Add more cases as needed
            default:
                Debug.LogWarning($"Stat type {statType} not handled.");
                break;
        }
    }
}
