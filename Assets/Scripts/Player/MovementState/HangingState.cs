

using UnityEngine;

public class HangingState : IMovementState
{
    public PlayerMovement Movement { get; set; }

    public HangingState(PlayerMovement movement)
    {
        Movement = movement;
    }

    public void FixedUpdate()
    {
        Move();
    }


    public void Move()
    {
        Vector3 direction = Movement.movement;
        Vector3 speed = Movement.transform.up * direction.y + Movement.transform.right * direction.x;
        speed *= Movement.Speed * 0.1f;

        Movement.rb.velocity = speed;
    }

    public void Jump()
    {
        // 매달린 상태에서 점프 -> 풀기
        Movement.OffHanging();
    }
}