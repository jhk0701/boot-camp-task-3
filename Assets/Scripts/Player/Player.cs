using UnityEngine;

public class Player : MonoBehaviour
{
    public StatusStat health;
    public StatusStat stamina;

    void Awake()
    {
        PlayerManager.Instance.player = this;
    }
}