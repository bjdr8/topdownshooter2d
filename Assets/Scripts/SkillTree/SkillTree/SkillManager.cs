using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class SkillManager
{
    /// <summary>
    /// initialize these classes
    /// </summary>
    public ScriptableSkillNode rootNode; // skill tree start data
    public GameObject skillButtonPrefab; // a button prefab.
    public RectTransform skilltreePanel; // the panel where the skill tree is allowed to be
    public PlayerProfile playerProfile; // just player data that i will need to migrate a bit
    private PlayerControler playerControler;

    GameManager gameManager;

    private SkillGroup skilltree; // skill tree start
    private SkilltreeSave skilltreeData; // script to save and load data to a txt file
    
    public List<SkillLeaf> skillsList = new List<SkillLeaf>();

    public SkillManager(ScriptableSkillNode startingNode,
           GameObject buttonPrefab,
           RectTransform skilltreeBorders,
           PlayerControler player,
           GameManager gameManager,
           PlayerProfile playerProfile,
           SkilltreeSave skilltreeDatapre)
    {
        rootNode = startingNode;
        skillButtonPrefab = buttonPrefab;
        skilltreePanel = skilltreeBorders;
        playerControler = player;
        skilltreeData = skilltreeDatapre;
        skilltreeData.skillManager = this;

        this.gameManager = gameManager;

        //gameDataFacade = new GameDataFacade();
        this.playerProfile = playerProfile;

        skilltree = GenerateSkilltree(this.rootNode);
        Debug.Log("Skill tree gegenereerd!");
        LogSkillTree(skilltree);
        CreateSkillUI(skilltree, new Vector2(0, 0));
    }

    private SkillGroup GenerateSkilltree(ScriptableSkillNode rootNode)
    {
        SkillGroup currentNode = new SkillGroup(playerProfile, skilltreeData);
        skillsList.Add(currentNode);
        currentNode.skillName = rootNode.skillName;
        currentNode.xpCosts = rootNode.xpCosts;
        currentNode.unlocked = rootNode.unlocked;
        currentNode.effects = rootNode.effects;

        if (currentNode.effects != null) {
            foreach (var effect in currentNode.effects)
            {
                effect.nameId = rootNode.skillName;
                //Debug.Log(effect.nameId);
            }
        }

        foreach (ScriptableSkillNode child in rootNode.children)
        {
            if (child.children.Count == 0)
            {
                SkillNode skillNode = new SkillNode(playerProfile, skilltreeData);
                skillNode.skillName = child.skillName;
                skillNode.xpCosts = child.xpCosts;
                skillNode.effects = child.effects;

                if (skillNode.effects != null)
                {
                    foreach (var effect in skillNode.effects)
                    {
                        effect.nameId = child.skillName;
                    }
                }

                currentNode.children.Add(skillNode);
                skillsList.Add(skillNode);
            }
            else
            {
                SkillGroup childGroup = GenerateSkilltree(child);
                currentNode.children.Add(childGroup);
            }
        }

        return currentNode;
    }

    private void LogSkillTree(SkillLeaf node, string indent = "")
    {
        if (node is SkillGroup group)
        {
            //Debug.Log($"{indent}Group: {group.skillName} (XP: {group.xpCosts})");

            foreach (SkillLeaf child in group.children)
            {
                LogSkillTree(child, indent + "  ");
            }
        }
        else if (node is SkillNode skill)
        {
            //Debug.Log($"{indent}Skill: {skill.skillName} (XP: {skill.xpCosts})");
        }
    }

    void CreateSkillUI(SkillLeaf skill, Vector2 position)
    {
        GameObject buttonObj = GameObject.Instantiate(skillButtonPrefab, skilltreePanel);
        gameManager.skillButtonList.Add(buttonObj);
        RectTransform rt = buttonObj.GetComponent<RectTransform>();
        rt.anchoredPosition = position;

        Image Image = buttonObj.GetComponentInChildren<Image>();
        
        skill.image = Image;

        TextMeshProUGUI text = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
        text.text = skill.skillName;
        Button button = buttonObj.GetComponent<Button>();
        //button.onClick.AddListener(() => Debug.Log($"Clicked on {skill.skillName}"));
        button.onClick.AddListener(() => skill.Buy(playerProfile));


        if (skill is SkillGroup group)
        {
            float xSpacing = 160f;
            float ySpacing = 100f;
            int count = group.children.Count;
            int index = 0;

            float totalWidth = GetSubtreeWidth(group);
            float currentX = position.x - (totalWidth * xSpacing / 2f);

            foreach (SkillLeaf skillnode in group.children)
            {
                float childWidth = GetSubtreeWidth(skillnode);
                Vector2 childPos = position + new Vector2(currentX + (childWidth * xSpacing / 2f), -ySpacing);
                CreateSkillUI(skillnode, childPos);
                currentX += childWidth * xSpacing;
                index++;
            }
        }
    }

    private void ObjectiveySkillTree(SkillLeaf node, string indent = "")
    {
        if (node is SkillGroup group)
        {
            Debug.Log($"{indent}Group: {group.skillName} (XP: {group.xpCosts})");

            foreach (SkillLeaf child in group.children)
            {
                LogSkillTree(child, indent + "  ");
            }
        }
        else if (node is SkillNode skill)
        {
            Debug.Log($"{indent}Skill: {skill.skillName} (XP: {skill.xpCosts})");
        }
    }

    float GetSubtreeWidth(SkillLeaf skill)
    {
        if (skill is SkillGroup group)
        {
            float width = 0f;
            foreach (var child in group.children)
            {
                width += GetSubtreeWidth(child);
            }
            return Mathf.Max(width, 1f); // Zorg dat lege groepen minstens 1 breedte-eenheid zijn
        }
        return 1f; // Een enkele node is 1 eenheid breed
    }

    public void SaveSkills()
    {
        //gameDataFacade.SaveAll();
        skilltreeData.Save();
    }

    public void LoadSkills()
    {
        skilltreeData.Load();
    }

    public void ResetSkills()
    {
        skilltreeData.ResetSkills();
    }
}
