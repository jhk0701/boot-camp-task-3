using UnityEngine;

public class PlayerView : MonoBehaviour
{
    PlayerController controller;

    Vector2 direction;
    [Header("Camera")]
    [SerializeField] Transform cameraAxis;
    [SerializeField] [Range(-90f, 90f)] float minClampY = -85f;
    [SerializeField] [Range(-90f, 90f)] float maxClampY = 85f;
    [SerializeField] float rotateSensitive = 15f;

    void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    void Start()
    {
        controller.OnLookEvent += Look;
    }

    void LateUpdate()
    {
        // direction.x : 좌우 방향은 캐릭터
        // direction.y : 카메라 앵글
        if(direction.magnitude > 0.1f)
        {
            float speed = rotateSensitive * Time.deltaTime;

            transform.eulerAngles += Vector3.up * direction.x * speed;

            float rotateY = Mathf.Clamp(speed * -direction.y, minClampY, maxClampY);
            cameraAxis.localEulerAngles += Vector3.right * rotateY;
        }
    }

    void Look(Vector2 dir)
    {
        direction = dir;
    }
}