using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    Player player;
    
    Rigidbody rb;

    Vector2 movement;

    [Header("Move")]
    [SerializeField] float baseSpeed = 5f;
    [SerializeField] [Range(1.1f, 3f)] float timesOfSpeedOnRunning = 1.5f;
    public float Speed => baseSpeed + player.dexterity / 5;
    [SerializeField] float staminaUsageOfRun = 1f;
    bool isRunning = false;

    [Header("Jump")]
    [SerializeField] float jumpPower = 5f;
    [SerializeField] float staminaUsageOfJump = 10f;
    [SerializeField] LayerMask jumpableLayerMask;
    [SerializeField] float fallingThreshold = 2f;
    float jumpTime;
    
    bool isJumping = false;
    bool isFalling = false;

    public event Action OnPlayerJump;
    public event Action OnPlayerFalling;


    void Start()
    {
        player = Player.Instance;
        rb = player.rb;

        // 생애주기를 함께할 것이라 구독해제는 따로 구현하지 않음
        PlayerController controller = player.inputController;
        controller.OnMoveEvent += OnMove;
        controller.OnJumpEvent += OnJump;
        controller.OnRunEvent += OnRun;
    }

    void FixedUpdate()
    {
        if (isJumping || isFalling)
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(ray, 0.1f, jumpableLayerMask))
            {
                isJumping = false;
                isFalling = false;
            }
            else if (Time.time - jumpTime > fallingThreshold && !isFalling)
            {
                isJumping = false;
                isFalling = true;

                OnPlayerFalling?.Invoke();
            }

            return;
        }

        Move();
    }


    void OnMove(Vector2 input)
    {
        movement = input;
    }

    void Move()
    {
        float speed = isRunning ? Speed * timesOfSpeedOnRunning : Speed;
        
        Vector3 move = transform.forward * movement.y + transform.right * movement.x;
        move *= speed;

        // rb.velocity = move + rb.velocity;
        rb.AddForce(move, ForceMode.Force);

        if(isRunning)
            player.UseStatusStat(player.stamina, staminaUsageOfRun * Time.fixedDeltaTime);
    }

    void OnJump()
    {
        if (IsJumpable() && !isFalling)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            player.UseStatusStat(player.stamina, staminaUsageOfJump);

            OnPlayerJump?.Invoke();

            isJumping = true;
            jumpTime = Time.time;
        }
    }

    bool IsJumpable()
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

    void CheckIsFalling()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (!Physics.Raycast(ray, fallingThreshold, jumpableLayerMask))
            isFalling = true;
        else
            isFalling = false;
    }

    void OnRun(bool running)
    {
        isRunning = running;
    }
}