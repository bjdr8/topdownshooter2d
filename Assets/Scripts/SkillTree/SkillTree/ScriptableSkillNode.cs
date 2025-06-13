using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New SkillNode", menuName = "Skill System/SkillNode")]
public class ScriptableSkillNode : ScriptableObject
{
    public List<ScriptableSkillNode> children;
    public List<BaseEffect> effects;

    public string skillName;
    public int xpCosts;
    public bool unlocked;
    public bool bought;
    public bool active;
}
