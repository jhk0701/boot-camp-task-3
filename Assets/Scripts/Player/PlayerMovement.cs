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

    public event Action OnPlayerRun;
    public event Action OnPlayerJump;
    public event Action OnPlayerFall;


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
        move *= speed * rb.mass * 1.5f;

        // move.y = rb.velocity.y;
        // rb.velocity = move;
        // 수정 : 문제 내용에 따른 AddForce로 구현
        rb.AddForce(move, ForceMode.Force);

        if(isRunning)
            player.UseStatusStat(player.stamina, staminaUsageOfRun * Time.fixedDeltaTime);
    }

    void OnJump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            player.UseStatusStat(player.stamina, staminaUsageOfJump);

            OnPlayerJump?.Invoke();
        }
    }

    public void OnJumpEnd()
    {
        if (IsGrounded())
        {
            
        }
    }

    bool IsGrounded()
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
}