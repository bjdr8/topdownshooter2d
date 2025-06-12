using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControler
{
    GameObject player;
    private float horizontalInput;
    private float verticalInput;

    private Vector2 moveDirection;

    private float movementSpeed;
    private float playerDrag;

    private Rigidbody2D rb;

    public PlayerControler(GameObject player, float movementSpeed, float playerDrag)
    {
        rb = player.GetComponent<Rigidbody2D>();
        this.movementSpeed = movementSpeed;
        this.playerDrag = playerDrag;
        rb.drag = playerDrag;
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
