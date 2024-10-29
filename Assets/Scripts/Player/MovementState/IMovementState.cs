public interface IMovementState
{
    PlayerMovement Movement { get; set; }

    void Move();
    void Jump();

    void FixedUpdate();
}