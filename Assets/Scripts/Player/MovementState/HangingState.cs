

public class HangingState : IMovementState
{
    public PlayerMovement Movement { get; set; }

    public HangingState(PlayerMovement movement)
    {
        Movement = movement;
    }

    public void Move()
    {

    }

    public void Jump()
    {
        
    }

    public void FixedUpdate()
    {
        
    }
}