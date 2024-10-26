using UnityEngine;

public class Player : MonoBehaviour
{
    public StatusStat health;
    public StatusStat stamina;

    void Start()
    {
        PlayerManager.Instance.player = this;
    }
}