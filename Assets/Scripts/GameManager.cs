using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player info")]
    public GameObject player;
    public float playerMovementSpeed;
    public float playerDrag;
    private PlayerControler playerControler;
    public PlayerProfile playerProfile = new PlayerProfile();

    [Header("Player Cam")]
    private Camera _camera;

    [Header("SKillTree info")]
    public ScriptableSkillNode rootNode;
    public GameObject skillButtonPrefab;
    public RectTransform skilltreePanel;
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
    public List<GameObject> playingUI;
    private enum gameState {
        StartingMenu,
        GameOverMenu,
        SkillTreeMenu,
        Playing
    }
    private gameState state = gameState.StartingMenu;


    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        playerControler = new PlayerControler(player, playerMovementSpeed, playerDrag, playerProfile);
        skillManager = new SkillManager(rootNode, skillButtonPrefab, skilltreePanel, playerControler, this, playerProfile);
        enemySpawner = new EnemySpawner(this, player, spawnPoints, enemyWaves, enemyPrefab, eEnemyPrefab);
        enemySpawner.ReadNextWave();
        StartCoroutine(enemySpawner.SpawnEnemies());
        StartCoroutine(enemySpawner.SpawnEEnemies());

    }

    public void StartGame()
    {
        enemySpawner.ReadNextWave();
        StartCoroutine(enemySpawner.SpawnEnemies());
        StartCoroutine(enemySpawner.SpawnEEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        playerControler.SetDragAndSpeed(playerMovementSpeed, playerDrag); // for tweak reasons
        switch (state)
        {
            case gameState.Playing:
                SwitchButtonState(menuButtons, false);
                SwitchButtonState(playingUI, true);
                break;
            case gameState.StartingMenu:
                SwitchButtonState(menuButtons, true);
                SwitchButtonState(skillButtonList, false);
                SwitchButtonState(gameOverButtons, false);
                break;
            case gameState.SkillTreeMenu:
                SwitchButtonState(menuButtons, false);
                SwitchButtonState(skillButtonList, true);
                break;
            case gameState.GameOverMenu:
                SwitchButtonState(gameOverButtons, true);
                SwitchButtonState(playingUI, false);
                break;
        }
    }

    private void FixedUpdate()
    {
        if (state == gameState.Playing)
        {
            playerControler.MovePlayer();
            if (aliveEnemies.Count > 0 && aliveEnemies != null)
            {
                foreach (var enemy in aliveEnemies)
                {
                    enemy.MoveEnemy();
                }
            }
            Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        }
    }

    public void SwitchButtonState(List<GameObject> buttonsList, bool state)
    {
        foreach (var button in buttonsList)
        {
            button.gameObject.SetActive(state);
        }
    }
}
