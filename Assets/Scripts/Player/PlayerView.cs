using UnityEngine;

[RequireComponent(typeof(PlayerController))]
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
        float speed = rotateSensitive * Time.deltaTime;

        transform.Rotate(Vector3.up * direction.x * speed);

        float rotateY = Mathf.Clamp(speed * -direction.y, minClampY, maxClampY);

        cameraAxis.Rotate(Vector3.right * rotateY, Space.Self);
    }
    

    void Look(Vector2 dir)
    {
        direction = dir;
    }
    
    // TODO 1, 3인칭 변경 기능 (Tab 키)
}