using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    Camera cam;

    Vector2 direction;

    [Header("Camera")]
    [SerializeField] Transform cameraAxis;
    float camRotateX = 0f;
    [SerializeField] Vector2 clampForFirstPerson;
    [SerializeField] Vector2 clampForThirdPerson;
    [SerializeField] float rotateSensitive = 15f;

    [Header("View")]
    [SerializeField] bool isFirstPersonView;
    [SerializeField] Transform firstPerson;
    [SerializeField] Transform thirdPerson;
    [SerializeField] AnimationCurve changingAnimation;

    void Start()
    {
        cam = Camera.main;

        PlayerController controller = Player.Instance.inputController;
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

        camRotateX += speed * -direction.y;
        if(isFirstPersonView)
            camRotateX = Mathf.Clamp(camRotateX, clampForFirstPerson.x, clampForFirstPerson.y);
        else
            camRotateX = Mathf.Clamp(camRotateX, clampForThirdPerson.x, clampForThirdPerson.y);
        cameraAxis.localEulerAngles = Vector3.right * camRotateX;
    }
    

    void Look(Vector2 dir)
    {
        direction = dir;
    }
    
    // 1, 3인칭 변경 기능 (Tab 키)
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