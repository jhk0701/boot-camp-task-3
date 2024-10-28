using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour
{
    Animator animator;
    readonly int MoveX = Animator.StringToHash("MoveX");
    readonly int MoveY = Animator.StringToHash("MoveY");
    readonly int Jump = Animator.StringToHash("Jump");
    readonly int IsRunning = Animator.StringToHash("IsRunning");


    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // 생애 주기를 함께할 것이라 별도로 구독해제하지 않음
        Player player = Player.Instance;
        player.inputController.OnMoveEvent += Move;
        player.inputController.OnRunEvent += Run;
    }

    
    void Move(Vector2 direction)
    {
        animator.SetFloat(MoveX, direction.x);
        animator.SetFloat(MoveY, direction.y);
    }

    void Run(bool running)
    {
        animator.SetBool(IsRunning, running);
    }
}
