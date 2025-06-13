using UnityEngine;

[CreateAssetMenu(fileName = "StatModifierEffect", menuName = "Skill System/Effects/Dash Unlock")]
public class UnlockDash : BaseEffect
{
    public bool unlockDash;
    public override void ApplyEffect(PlayerControler player)
    {
        player.dashUnlocked = unlockDash;
    }

    public override void RevertEffect(PlayerControler player)
    {
        player.dashUnlocked = !unlockDash;
    }
}
