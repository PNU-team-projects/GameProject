using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance { get; private set; }

    private PlayerControls playerControls;
    private Player player;
    private Vector2 movement;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        playerControls = new PlayerControls();
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {
        playerControls.Combat.Attack.started += _ => player.Attack();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        player.Move(movement);
    }

    private void PlayerInput() {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
    }

}
