using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public Player player;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Vector2 movement;

    IMovementState curState;
    IMovementState normalState;
    IMovementState hangingState;

    [Header("Move")]
    [HideInInspector] public float baseSpeed = 5f;
    [HideInInspector] [Range(1.1f, 3f)] public float timesOfSpeedOnRunning = 1.5f;
    public float Speed => baseSpeed + player.status.dexterity.Value / 5;
    [HideInInspector] public float staminaUsageOfRun = 1f;
    [HideInInspector] public bool isRunning = false;

    [Header("Jump")]
    public float jumpPower = 5f;
    public float staminaUsageOfJump = 10f;
    public LayerMask jumpableLayerMask;
    [HideInInspector] public bool isFalling = false;
    [HideInInspector] public float fallingCheckRate = 0.1f;
    [HideInInspector] public float lastFallingCheck;

    public Action OnPlayerRun;
    public Action OnPlayerJump;
    public Action OnPlayerFall;
    public Action OnPlayerLand;

    void Awake()
    {
        normalState = new NormalState(this);
        hangingState = new HangingState(this);    
    }

    void Start()
    {
        player = Player.Instance;
        rb = player.rb;

        PlayerController controller = player.inputController;
        controller.OnMoveEvent += OnMove;
        controller.OnJumpEvent += OnJump;
        controller.OnRunEvent += OnRun;

        ChangeState(normalState);
    }

    void FixedUpdate()
    {
        curState.FixedUpdate();
    }


    void OnMove(Vector2 input)
    {
        movement = input;
    }

    void OnJump()
    {
        curState.Jump();
        return;
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            player.status.UseStatusStat(player.status.stamina, staminaUsageOfJump);

            OnPlayerJump?.Invoke();
        }
    }

    public void OnJumpEnd()
    {
        if (IsGrounded())
        {
            isFalling = false;
        }
        else
        {
            isFalling = true;
            lastFallingCheck = Time.time;
            OnPlayerFall?.Invoke();
        }
    }

    public bool IsGrounded()
    {
        Ray[] rays = new Ray[]
        {
            new Ray(transform.position + Vector3.forward * 0.2f + Vector3.up * 0.1f, Vector3.down),
            new Ray(transform.position - Vector3.forward * 0.2f + Vector3.up * 0.1f, Vector3.down),
            new Ray(transform.position + Vector3.right * 0.2f + Vector3.up * 0.1f, Vector3.down),
            new Ray(transform.position - Vector3.right * 0.2f + Vector3.up * 0.1f, Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.2f, jumpableLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    void OnRun(bool running)
    {
        isRunning = running;
    }

    void ChangeState(IMovementState movementState)
    {
        curState = movementState;
    }
}