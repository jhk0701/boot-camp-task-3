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
        // 점프 키를 뗐을때 호출되도록 설정
        if(context.phase == InputActionPhase.Canceled) 
        {
            OnJumpEvent?.Invoke();
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        OnLookEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            OnInteractEvent?.Invoke();
        }
    }

}
