using System.Collections;
using UnityEngine;


public class Player : Singleton<Player>
{
    [HideInInspector] public PlayerStatus status;
    [HideInInspector] public PlayerController inputController;
    [HideInInspector] public PlayerInteraction interaction;
    [HideInInspector] public PlayerMovement movement;
    [HideInInspector] public Rigidbody rb;

    // 해당 클래스에서 의존성 주입해줄 것.
    [HideInInspector] public UIConsumableItems consumableItems;
    [HideInInspector] public UIInventory inventory;

    void Awake()
    {
        status = GetComponent<PlayerStatus>();
        inputController = GetComponent<PlayerController>();
        interaction = GetComponent<PlayerInteraction>();
        movement = GetComponent<PlayerMovement>();

        rb = GetComponent<Rigidbody>();
    }
}