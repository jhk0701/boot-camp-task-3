using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Player Input의 입력을 받아 처리할 클래스
public class PlayerController : MonoBehaviour
{
    // 입력을 받으면 뿌려줄 것임
    public event Action<Vector2> OnMoveEvent;
    public event Action OnJumpEvent;
    public event Action OnInteractEvent;
    public event Action<Vector2> OnLookEvent;


    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            Vector2 input = context.ReadValue<Vector2>();
            OnMoveEvent?.Invoke(input);
        }
        else if(context.phase == InputActionPhase.Canceled)
            OnMoveEvent?.Invoke(Vector2.zero);
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Canceled)
        {
            OnJumpEvent?.Invoke();
        }
    }

    
}
