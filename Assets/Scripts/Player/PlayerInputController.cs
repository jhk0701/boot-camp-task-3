using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Player Input의 입력을 받아 처리할 클래스
public class PlayerInputController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action OnJumpEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action OnInteractEvent;
    public event Action OnUseItemEvent;
    public event Action OnChangeViewEvent;
    public event Action<bool> OnRunEvent;
    public event Action OnInventoryEvent;


    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
            OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
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
        if (context.phase == InputActionPhase.Started)
        {
            OnInteractEvent?.Invoke();
        }
    }

    public void OnUseItem(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            OnUseItemEvent?.Invoke();
        }
    }

    public void OnChangeView(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            OnChangeViewEvent?.Invoke();
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
            OnRunEvent?.Invoke(true);
        else if(context.phase == InputActionPhase.Canceled)
            OnRunEvent?.Invoke(false);
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            OnInventoryEvent?.Invoke();
    }

}
