using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    Player player;
    PlayerController controller;
    Rigidbody rb;

    Vector2 movement;
    [Header("Move")]
    [SerializeField] float baseSpeed = 5f;
    public float Speed => baseSpeed + player.dexterity / 5;

    [Header("Jump")]
    [SerializeField] float jumpPower = 5f;
    [SerializeField] float staminaAmountOfJumpUsage = 10f;
    [SerializeField] LayerMask jumpableLayerMask;


    void Awake()
    {
        player = GetComponent<Player>();
        controller = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // 생애주기를 함께할 것이라 구독해제는 따로 구현하지 않음
        controller.OnMoveEvent += Move;
        controller.OnJumpEvent += Jump;
    }

    void FixedUpdate()
    {
        Vector3 move = transform.forward * movement.y + transform.right * movement.x;
        move *= Speed;
        move.y = rb.velocity.y;
        
        rb.velocity = move;
    }


    void Move(Vector2 input)
    {
        movement = input;
    }

    void Jump()
    {
        if (IsJumpable())
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            PlayerManager.Instance.player.UseStamina(staminaAmountOfJumpUsage);
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
}