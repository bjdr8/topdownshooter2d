using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControler // in de toekomst playercontroler manager maken die elk player script aan stuurt maar nu gewoon deze als de volledige player gebruiken
{
    GameObject player;
    private float horizontalInput;
    private float verticalInput;

    private Vector2 moveDirection;

    private float movementSpeed;
    private float playerDrag;

    private Rigidbody2D rb;
    private PlayerProfile profile;

    public PlayerControler(GameObject player, float movementSpeed, float playerDrag, PlayerProfile profile)
    {
        rb = player.GetComponent<Rigidbody2D>();
        this.movementSpeed = movementSpeed;
        this.playerDrag = playerDrag;
        rb.drag = playerDrag;
        this.profile = profile;
    }

    public void MovePlayer()
    {
        MyInput();
        MovePlayerLogic();
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
    }

    public void SetDragAndSpeed(float speed, float drag)
    {
        movementSpeed = speed;
        rb.drag = drag;
    }
}
