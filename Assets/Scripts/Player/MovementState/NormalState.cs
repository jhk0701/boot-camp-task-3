using System;
using UnityEngine;

public class NormalState : IMovementState
{
    public PlayerMovement Movement { get; set; }

    public NormalState(PlayerMovement movement)
    {
        Movement = movement;
    }


    public void FixedUpdate()
    {
        if (Movement.isFalling)
        {
            if (Time.time - Movement.lastFallingCheck > Movement.fallingCheckRate && Movement.IsGrounded())
            {
                Movement.isFalling = false;
                Movement.OnPlayerLand?.Invoke();
            }
        }

        Move();
    }

    public void Move()
    {
        float speed = Movement.isRunning ? Movement.Speed * Movement.timesOfSpeedOnRunning : Movement.Speed;
        speed *= Movement.isFalling ? 0.5f : 1f;
        
        Vector3 move = Movement.transform.forward * Movement.movement.y + Movement.transform.right * Movement.movement.x;
        move *= speed * Movement.rb.mass * 1.5f;

        // move.y = rb.velocity.y;
        // rb.velocity = move;
        // 수정 : 문제 내용에 따른 AddForce로 구현
        Movement.rb.AddForce(move, ForceMode.Force);

        if(Movement.isRunning)
            Movement.player.status.UseStatusStat(Movement.player.status.stamina, Movement.staminaUsageOfRun * Time.fixedDeltaTime);
    }

    public void Jump()
    {
        if (Movement.IsGrounded())
        {
            Movement.rb.AddForce(Vector3.up * Movement.jumpPower, ForceMode.Impulse);
            Movement.player.status.UseStatusStat(Movement.player.status.stamina, Movement.staminaUsageOfJump);

            Movement.OnPlayerJump?.Invoke();
        }
    }
}