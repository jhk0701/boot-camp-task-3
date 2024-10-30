using System.Collections;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] Transform platform;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [Space(10f)]
    [SerializeField] float speed = 1f;
    [SerializeField] float stopTime = 3f;

    WaitForSeconds waitForSec;

    void Start()
    {
        platform.localPosition = startPoint.localPosition;

        waitForSec = new WaitForSeconds(stopTime);
        Move(1);
    }

    void Move(int direction)
    {
        if(moveHandler != null)
        {
            StopCoroutine(moveHandler);
            moveHandler = null;
        }

        moveHandler = StartCoroutine(MovePlatform(direction));
    }

    Coroutine moveHandler;
    IEnumerator MovePlatform(int direction)
    {
        float target = direction > 0 ? 1f : 0f;
        float progress = direction > 0 ? 0f : 1f;

        while (progress != target)
        {   
            progress += speed * Time.deltaTime * direction;

            platform.localPosition = Vector3.Lerp(startPoint.localPosition, endPoint.localPosition, progress);

            yield return null;

            if ((direction > 0 && progress > target) || (direction < 0 && progress < target))
                progress = target;
        }

        yield return waitForSec;

        Move(-direction);
    }

}