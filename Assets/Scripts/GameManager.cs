using System.Collections;
using System.Collections.Generic;
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
    public List<Bullet> bulletList;
    public List<GameObject> bulletObjectList;
    public GameObject pistol;
    public GameObject ar;
    public GameObject miniGun;

    [Header("Player Cam")]
    private Camera _camera;

    [Header("SKillTree info")]
    public ScriptableSkillNode rootNode;
    public GameObject skillButtonPrefab;
    public RectTransform skilltreePanel;
    public ScriptableSkilltreeSave skilltreeData;
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
        playerCollider = player.GetComponent<BoxCollider2D>();
        playerControler = new PlayerControler(player, playerMovementSpeed, playerDrag, playerProfile);
        skillManager = new SkillManager(rootNode, skillButtonPrefab, skilltreePanel, playerControler, this, playerProfile, skilltreeData);
        enemySpawner = new EnemySpawner(this, player, spawnPoints, enemyWaves, enemyPrefab, eEnemyPrefab);
    }

    public void SetMenuStartGame()
    {
        state = gameState.Playing;
        enemySpawner.ReadNextWave();
        StartCoroutine(enemySpawner.SpawnEnemies());
        StartCoroutine(enemySpawner.SpawnEEnemies());
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

        playerControler.SetDragAndSpeed(playerMovementSpeed, playerDrag); // for tweak reasons
        switch (state)
        {
            case gameState.Playing:
                SwitchButtonState(menuButtons, false);
                SwitchButtonState(skillButtonList, false);
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

        if (playerControler.hp <= 0)
        {
            state = gameState.GameOverMenu;
        }

        foreach (var enemy in aliveEnemies)
        {
            if (playerCollider.bounds.Intersects(enemy.collider.bounds))
            {
                Debug.Log("Collision with enemy!");
            }
            foreach (var bullet in bulletList)
            {
                CircleCollider2D bulletColider = bullet.bulletObject.GetComponent<CircleCollider2D>();
                if (bulletColider.bounds.Intersects(enemy.collider.bounds))
                {
                    //enemy.hp -= bullet.damage;
                }
            }
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
            if (aliveEnemies != null)
            {
                state = gameState.SkillTreeMenu;
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

    public void QuitGame()
    {
        skillManager.saveSkills();
        Application.Quit();
    }
}
