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

    [HideInInspector] public float baseSpeed = 5f;
    [HideInInspector] [Range(1.1f, 3f)] public float timesOfSpeedOnRunning = 1.5f;
    public float Speed => baseSpeed + player.status.dexterity.Value / 5;

    [Header("Run")]
    public bool isRunning = false;
    public float minStaminaForRun = 10f;
    public float staminaUsageOfRun = 1f;

    [Header("Jump")]
    public bool isJumping = false;
    public float jumpPower = 5f;
    public float staminaUsageOfJump = 10f;
    public LayerMask jumpableLayerMask;

    [Header("Fall")]
    public bool isFalling = false;
    [HideInInspector] public float fallingCheckRate = 0.1f;
    [HideInInspector] public float lastFallingCheck;

    [Header("Hang")]
    public bool isHanging = false;
    public float hangingDistance = 1f;
    public LayerMask hangableMask;

    public Action<bool> OnPlayerRun;
    public Action OnPlayerJump;
    public Action OnPlayerFall;
    public Action OnPlayerLand;
    public Action OnPlayerHang;


    void Awake()
    {
        normalState = new NormalState(this);
        hangingState = new HangingState(this);    
    }

    void Start()
    {
        player = Player.Instance;
        rb = player.rb;

        PlayerInputController input = player.inputController;
        input.OnMoveEvent += OnMove;
        input.OnJumpEvent += OnJump;
        input.OnRunEvent += OnRun;
        input.OnInteractEvent += OnHang;

        ChangeState(normalState);
    }

    void FixedUpdate()
    {
        if (Time.time - lastFallingCheck > fallingCheckRate && !isHanging)
        {
            lastFallingCheck = Time.time;
            
            if (isFalling && IsGrounded())
            {
                isFalling = false;
                OnPlayerLand?.Invoke();
            }
            else if(!isFalling && !isJumping && !IsGrounded())
            {
                Fall();
            }
        }

        curState.FixedUpdate();
    }


    void ChangeState(IMovementState movementState)
    {
        curState = movementState;
    }

    void OnMove(Vector2 input)
    {
        movement = input;
    }

    public void OnRun(bool running)
    {
        isRunning = player.status.stamina.Value >= minStaminaForRun && running;
        OnPlayerRun?.Invoke(isRunning);
    }

    void OnJump()
    {
        curState.Jump();
    }

    public void OnJumpEnd()
    {
        isJumping = false;

        if (IsGrounded())
        {
            isFalling = false;
        }
        else
        {
            Fall();
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

    public void Fall()
    {
        isFalling = true;
        OnPlayerFall?.Invoke();
    }

    public void OnHang()
    {
        if (IsHangable(out Vector3 point))
        {
            Debug.Log($"Hanging : {point}");
        }
    }

    public bool IsHangable(out Vector3 hitPoint)
    {
        Ray ray = new Ray(transform.position + transform.forward, transform.forward);
        if(Physics.Raycast(ray, out RaycastHit hit, hangingDistance, hangableMask))
        {
            hitPoint = hit.point;
            return true;
        }
        else
        {
            hitPoint = Vector3.zero;
            return false;
        }
    }

}