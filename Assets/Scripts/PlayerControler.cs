using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControler // in de toekomst playercontroler manager maken die elk player script aan stuurt maar nu gewoon deze als de volledige player gebruiken
{
    GameObject player;
    public int maxHp = 6;
    public int hp = 6;
    private Weapon equipedWeapon;

    private List<GameObject> weapons;

    private float horizontalInput;
    private float verticalInput;

    private Vector2 moveDirection;

    private float movementSpeed;
    private float playerDrag;

    private Rigidbody2D rb;
    private PlayerProfile profile;

    private GameManager gameManager;

    private Ar ar;
    private Pistol pistol;
    private MiniGun minigun;

    public PlayerControler(GameObject player, float movementSpeed, float playerDrag, PlayerProfile profile, List<GameObject> weapons, GameManager gameManager)
    {
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

    public void MovePlayer()
    {
        MyInput();
        MovePlayerLogic();
    }

    public void SetDragAndSpeed(float speed, float drag)
    {
        movementSpeed = speed;
        rb.drag = drag;
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayerLogic()
    {
        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;
        rb.AddForce(moveDirection * movementSpeed, ForceMode2D.Force);
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
