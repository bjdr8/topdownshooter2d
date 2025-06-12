using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[CreateAssetMenu(fileName = "New SkillNode", menuName = "SkillNode")]
public class ScriptableSkillNode : ScriptableObject
{
    public List<ScriptableSkillNode> children;

    public string skillName;
    public int xpCosts;
    public bool unlocked;
    public bool bought;
    public bool active;
}
