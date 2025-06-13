using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player info")]
    public GameObject player;
    public float playerMovementSpeed;
    public float playerDrag;
    private PlayerControler playerControler;
    private PlayerProfile playerProfile = new PlayerProfile();
    private BoxCollider2D playerCollider;

    [Header("Gun info")]
    public List<GameObject> weapons;
    public GameObject bulletObjectPrefab;
    public List<Bullet> bulletList = new List<Bullet>();

    [Header("Player Cam")]
    private Camera _camera;

    [Header("SKillTree info")]
    public ScriptableSkillNode rootNode;
    public GameObject skillButtonPrefab;
    public RectTransform skilltreePanel;
    public PassiveEffect passiveEffect;
    public List<BaseEffect> AllEffects;
    private SkilltreeSave skilltreeData;
    private SkillManager skillManager;

    [Header("Enemy Info")]
    public List<float> enemyWaves;
    public List<GameObject> spawnPoints;
    public GameObject enemyPrefab;
    public GameObject eEnemyPrefab;
    public List<BaseEnemy> aliveEnemies = new List<BaseEnemy>();
    private EnemySpawner enemySpawner;

    [Header("UI Elements")]
    public List<GameObject> menuButtons;
    public List<GameObject> skillButtonList;
    public List<GameObject> gameOverButtons;
    public List<GameObject> WinUIList;
    public List<GameObject> playingUI;
    public TextMeshProUGUI hpCounter;
    public TextMeshProUGUI dashCooldownCounter;
    public TextMeshProUGUI xpCounter;
    public TextMeshProUGUI WinText;

    private enum gameState {
        StartingMenu,
        GameOverMenu,
        SkillTreeMenu,
        WinMenu,
        Playing
    }
    private gameState state = gameState.StartingMenu;


    // Start is called before the first frame update
    void Start()
    {
        passiveEffect = new PassiveEffect(AllEffects);
        skilltreeData = new SkilltreeSave(passiveEffect);
        TurnOffAllUI();
        _camera = Camera.main;
        playerCollider = player.GetComponent<BoxCollider2D>();
        playerControler = new PlayerControler(player, playerMovementSpeed, playerDrag, playerProfile, weapons, this);
        skillManager = new SkillManager(rootNode, skillButtonPrefab, skilltreePanel, playerControler, this, playerProfile, skilltreeData);
        enemySpawner = new EnemySpawner(this, player, spawnPoints, enemyWaves, enemyPrefab, eEnemyPrefab);
    }

    public void SetMenuStartGame()
    {
        passiveEffect.Apply(playerControler);
        playerControler.hp = playerControler.maxHp;
        //enemySpawner.waveCounter = 0; //activate again when u can play waves one after the other
        enemySpawner.ReadNextWave();
        StartCoroutine(enemySpawner.SpawnEnemies());
        StartCoroutine(enemySpawner.SpawnEEnemies());
        state = gameState.Playing;
    }


    public void SetMenuSkillTree()
    {
        state = gameState.SkillTreeMenu;
    }

    public void SetMenuGameOver()
    {
        state = gameState.GameOverMenu;
    }

    public void SetMenuMainMenu()
    {
        state = gameState.StartingMenu;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = (mousePos - playerPos).normalized;

        //playerControler.SetDragAndSpeed(playerMovementSpeed, playerDrag); // for tweak reasonss
        switch (state)
        {
            case gameState.Playing:
                TurnOffAllUI();
                SwitchButtonState(playingUI, true);

                hpCounter.text = ("Max Hp = " + playerControler.maxHp +
                                  " Current Hp = " + playerControler.hp);

                if (playerControler.dashUnlocked)
                {
                    if (playerControler.dashCooldownTimer <= 0)
                    {
                        dashCooldownCounter.text = ("Dashcooldown Timer = " + 0);
                    }
                    else
                    {
                        dashCooldownCounter.text = ("Dashcooldown Timer = " + playerControler.dashCooldownTimer);
                    }
                }
                else
                {
                    dashCooldownCounter.text = "";
                }

                xpCounter.text = ("Total XP = " + playerProfile.xp);

                playerControler.ChangeWeapon();
                playerControler.TimersCountDown();
                if (Input.GetKeyDown(KeyCode.Space) && playerControler.dashCooldownTimer <= 0 && playerControler.dashUnlocked == true)
                {
                    playerControler.dashAtivated = true;
                }
                playerControler.Shooting(shootDirection);

                if (playerControler.hp <= 0)
                {
                    enemySpawner.ResetWaves();
                    state = gameState.GameOverMenu;
                }

                if (aliveEnemies.Count <= 0 || aliveEnemies == null)
                {
                    state = gameState.WinMenu;
                }

                if (aliveEnemies == null)
                { 
                    return;
                }
                for (int i = aliveEnemies.Count - 1; i >= 0; i--)
                {
                    var enemy = aliveEnemies[i];
                    if (enemy.hp <= 0)
                    {
                        Destroy(enemy.enemy);
                        playerProfile.AddXp(enemy.xpWorth);
                        aliveEnemies.RemoveAt(i);
                        continue; // Skip rest since enemy is removed
                    }
                    if (playerCollider.bounds.Intersects(enemy.collider.bounds))
                    {
                        playerControler.hp -= enemy.damage;
                        Destroy(enemy.enemy);
                        aliveEnemies.RemoveAt(i);
                        continue;
                    }
                    if (bulletList != null)
                    {
                        for (int O = bulletList.Count - 1; O >= 0; O--)
                        {
                            var bullet = bulletList[O];
                            if (bullet.bulletObject == null || bullet.lifeSpan <= 0)
                            {
                                bulletList.RemoveAt(O);
                                continue;
                            }
                            CircleCollider2D bulletCollider = bullet.bulletObject.GetComponent<CircleCollider2D>();
                            if (bulletCollider.bounds.Intersects(enemy.collider.bounds))
                            {
                                enemy.hp -= bullet.damage;
                                Destroy(bullet.bulletObject);
                                bulletList.RemoveAt(O);
                            }
                        }
                    }
                }
                break;
            case gameState.StartingMenu:
                TurnOffAllUI();
                passiveEffect.Revert(playerControler);
                SwitchButtonState(menuButtons, true);
                break;
            case gameState.SkillTreeMenu:
                TurnOffAllUI();
                SwitchButtonState(skillButtonList, true);
                xpCounter.text = ("Total XP = " + playerProfile.xp);
                foreach (SkillLeaf leaf in skillManager.skillsList)
                {
                    leaf.ImageChange();
                }
                break;
            case gameState.GameOverMenu:
                TurnOffAllUI();
                SwitchButtonState(gameOverButtons, true);
                break;
            case gameState.WinMenu:
                TurnOffAllUI();
                SwitchButtonState(WinUIList, true);

                if (enemySpawner.waveCounter >= 5)
                {
                    WinText.text = ("You Win");
                }
                else
                {
                    WinText.text = ("You have beaten wave " + enemySpawner.waveCounter);
                }
                break;
        }
    }

    private void FixedUpdate()
    {
        if (state == gameState.Playing)
        {
            playerControler.MyInput();
            playerControler.MovePlayerLogic();

            if (playerControler.dashAtivated == true)
            {
                playerControler.Dash();
            }
            if (aliveEnemies.Count > 0 && aliveEnemies != null)
            {
                foreach (var enemy in aliveEnemies)
                {
                    enemy.MoveEnemy();
                }
            }
            if (bulletList != null)
            {
                foreach (var bullet in bulletList)
                {
                    bullet.MoveBullet();
                }
            }
            Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        }
    }

    public void SwitchButtonState(List<GameObject> UIList, bool state)
    {
        foreach (var UI in UIList)
        {
            UI.gameObject.SetActive(state);
        }
    }

    public void QuitGame()
    {
        skillManager.SaveSkills();
        Application.Quit();
    }

    public void SaveSkills()
    {
        skillManager.SaveSkills();
    }

    public void LoadSkills()
    {
        skillManager.LoadSkills();
    }

    public void ResetSkills()
    {
        skillManager.ResetSkills();
    }

    private void TurnOffAllUI()
    {
        SwitchButtonState(menuButtons, false);
        SwitchButtonState(skillButtonList, false);
        SwitchButtonState(gameOverButtons, false);
        SwitchButtonState(WinUIList, false);
        SwitchButtonState(playingUI, false);
    }
}
