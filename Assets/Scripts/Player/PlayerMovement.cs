using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    PlayerController controller;

    [SerializeField] float speed = 5f;
    [SerializeField] float jumpPower = 5f;
    [SerializeField] LayerMask jumpableLayerMask;
    Vector2 movement;

    void Awake()
    {
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
        Vector3 move = Vector3.forward * movement.y + Vector3.right * movement.x;
        move *= speed;
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
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
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