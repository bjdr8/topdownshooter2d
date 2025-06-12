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
    public List<GameObject> GameOverButtons;


    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        playerControler = new PlayerControler(player, playerMovementSpeed, playerDrag);
        skillManager = new SkillManager(rootNode, skillButtonPrefab, skilltreePanel, playerControler, this);
        enemySpawner = new EnemySpawner(this, player, spawnPoints, enemyWaves, enemyPrefab, eEnemyPrefab);
        enemySpawner.ReadNextWave();
        StartCoroutine(enemySpawner.SpawnEnemies());
        StartCoroutine(enemySpawner.SpawnEEnemies());

    }

    public void ShowMenuButttons()
    {
        foreach (var button in menuButtons)
        {
            button.gameObject.SetActive(true);
        }
    }

    public void HideMenuButtons()
    {
        foreach (var button in menuButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void ShowSkillButttons()
    {
        foreach (var button in skillButtonList)
        {
            button.gameObject.SetActive(true);
        }
    }

    public void HideSkillButtons()
    {
        foreach (var button in skillButtonList)
        {
            button.gameObject.SetActive(false);
        }
    }


    public void ShowGameOverButttons()
    {
        foreach (var button in GameOverButtons)
        {
            button.gameObject.SetActive(true);
        }
    }

    public void HideGameOverButtons()
    {
        foreach (var button in GameOverButtons)
        {
            button.gameObject.SetActive(false);
        }
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
        playerControler.SetDragAndSpeed(playerMovementSpeed, playerDrag);
    }
    private void FixedUpdate()
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
