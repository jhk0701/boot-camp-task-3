

public class HangingState : IMovementState
{
    public PlayerMovement Movement { get; set; }

    public HangingState(PlayerMovement movement)
    {
        Movement = movement;
    }

    public void FixedUpdate()
    {
        //
        Move();
    }


    public void Move()
    {

    }

    public void Jump()
    {
        // 매달린 상태에서 점프 -> 풀기
    }
}