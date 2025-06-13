using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControler // in de toekomst playercontroler manager maken die elk player script aan stuurt maar nu gewoon deze als de volledige player gebruiken
{
    GameObject player;
    public int maxHp = 6;
    public int hp;
    public int hpRegen = 0;
    public float hpRegenTimer = 10;
    public float hpRegenSetTimer = 60;
    private Weapon equipedWeapon;

    private List<GameObject> weapons;

    private float horizontalInput;
    private float verticalInput;

    private Vector2 moveDirection;

    public float movementSpeed;
    private float playerDrag;

    private Rigidbody2D rb;
    private PlayerProfile profile;

    private GameManager gameManager;

    private Ar ar;
    private Pistol pistol;
    private MiniGun minigun;

    public bool dashUnlocked = false;
    public bool dashAtivated = false;
    private float dashDistance = 2f;
    private float dashCooldown = 2.0f;
    public float dashCooldownTimer = 0f;
    public float dashForce = 10f;

    public PlayerControler(GameObject player, float movementSpeed, float playerDrag, PlayerProfile profile, List<GameObject> weapons, GameManager gameManager)
    {
        hp = maxHp;
        rb = player.GetComponent<Rigidbody2D>();
        this.movementSpeed = movementSpeed;
        this.playerDrag = playerDrag;
        rb.drag = playerDrag;
        this.profile = profile;
        this.weapons = weapons;
        this.gameManager = gameManager;
        SetupWeapons();
    }

    public void Shooting(Vector2 direction)
    {
        equipedWeapon.fireCooldown -= Time.deltaTime;

        if (Input.GetMouseButton(0) && equipedWeapon.fireCooldown <= 0f)
        {
            equipedWeapon.Shoot(direction, this.gameManager);
            equipedWeapon.fireCooldown = equipedWeapon.fireRate; // Reset cooldown
        }
    }

    public void ChangeWeapon()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            equipedWeapon = pistol;
            ChangeVisibleWeapon(0);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            equipedWeapon = ar;
            ChangeVisibleWeapon(1);
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            equipedWeapon = minigun;
            ChangeVisibleWeapon(2);
        }
    }

    public void ChangeVisibleWeapon(int weaponNumber)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weaponNumber == i)
            {
                weapons[i].SetActive(true);
            }
            else
            {
                weapons[i].SetActive(false);
            }
        }
    }

    public void TimersCountDown()
    {
        hpRegenTimer -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
        if (hpRegenTimer <= 0)
        {
            hpRegenTimer = hpRegenSetTimer;
            hp += hpRegen;
            if (hp > maxHp)
            {
                hp = maxHp;
            }
        }
    }

    public void Dash()
    {
        Vector2 dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (dashDirection == Vector2.zero)
        {
            dashDirection = Vector2.up;
        }

        Vector2 dashTarget = rb.position + dashDirection * dashDistance;

        rb.MovePosition(dashTarget); //teleport option

        //rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);
        dashCooldownTimer = dashCooldown;
        dashAtivated = false;
    }

    public void SetDragAndSpeed(float speed, float drag)
    {
        movementSpeed = speed;
        rb.drag = drag;
    }

    public void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    public void MovePlayerLogic()
    {
        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;
        rb.AddForce(moveDirection * movementSpeed, ForceMode2D.Force);
        Debug.Log(movementSpeed);
    }

    private void SetupWeapons()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            switch (weapons[i].name)
            {
                case "Ar":
                    ar = new Ar(weapons[i]);
                    break;
                case "Pistol":
                    pistol = new Pistol(weapons[i]);
                    break;
                case "MiniGun":
                    minigun = new MiniGun(weapons[i]);
                    break;
            }
        }
        equipedWeapon = ar;
    }
}
