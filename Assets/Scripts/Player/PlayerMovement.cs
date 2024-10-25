using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    PlayerController controller;

    [SerializeField] float speed = 5f;
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
    }

    void FixedUpdate()
    {
        rb.velocity = Vector3.forward * movement.y + Vector3.right * movement.x;
        rb.velocity *= speed;
    }

    void Move(Vector2 input)
    {
        movement = input;
    }
}