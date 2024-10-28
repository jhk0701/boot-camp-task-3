using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerView : MonoBehaviour
{
    Camera cam;
    PlayerController controller;

    Vector2 direction;

    [Header("Camera")]
    [SerializeField] Transform cameraAxis;
    [SerializeField] [Range(-90f, 90f)] float minClampY = -85f;
    [SerializeField] [Range(-90f, 90f)] float maxClampY = 85f;
    [SerializeField] float rotateSensitive = 15f;

    [Header("View")]
    [SerializeField] bool isFirstPersonView;
    [SerializeField] Transform firstPerson;
    [SerializeField] Transform thirdPerson;
    [SerializeField] AnimationCurve changingAnimation;
    WaitForSecondsRealtime waitForSec = new WaitForSecondsRealtime(0.05f);


    void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    void Start()
    {
        cam = Camera.main;

        controller.OnLookEvent += Look;
        controller.OnChangeViewEvent += ChangeView;

        isFirstPersonView = true;
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

    void ChangeView()
    {
        isFirstPersonView = !isFirstPersonView;

        if (changeViewHandler != null)
            StopCoroutine(changeViewHandler);

        if (isFirstPersonView)
            changeViewHandler = StartCoroutine(ChangeCameraTransform(firstPerson));
        else
            changeViewHandler = StartCoroutine(ChangeCameraTransform(thirdPerson));
    }

    Coroutine changeViewHandler;
    IEnumerator ChangeCameraTransform(Transform to)
    {
        cam.transform.SetParent(to);
        Vector3 pos = cam.transform.localPosition;
        Quaternion rot = cam.transform.localRotation;

        float progress = 0f;
        while (progress <= 1f)
        {
            float animation = changingAnimation.Evaluate(progress);
            cam.transform.localPosition = Vector3.Lerp(pos, Vector3.zero, animation);
            cam.transform.localRotation = Quaternion.Lerp(rot, Quaternion.identity, animation);

            yield return null;
            progress += 0.01f;
        }

        changeViewHandler = null;
    }
}