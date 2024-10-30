using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Transform savePoint;

    public void SetSavePoint(Transform point)
    {
        savePoint = point;
    }

    public void ReviveOnSavePoint()
    {
        Player.Instance.transform.position = savePoint.position + Vector3.forward;
        Player.Instance.rb.velocity = Vector3.zero;

        Player.Instance.status.health.Initialize();
        Player.Instance.status.stamina.Initialize();
        Player.Instance.status.mana.Initialize();
    }
}
