using UnityEngine;
using System.Collections;

public class MenuCameraController : MonoBehaviour
{
    [Header("Focus Points")]
    public Transform defaultFocus;

    [Header("Settings")]
    public float transitionDuration = 1.2f;
    public AnimationCurve transitionCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Coroutine currentMove;

    public void FocusOn(Transform target)
    {
        if (currentMove != null) StopCoroutine(currentMove);
        currentMove = StartCoroutine(MoveToTarget(target));
    }

    public void ReturnToDefault()
    {
        if (defaultFocus == null) return;
        if (currentMove != null) StopCoroutine(currentMove);
        currentMove = StartCoroutine(MoveToTarget(defaultFocus));
    }

    IEnumerator MoveToTarget(Transform target)
    {
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / transitionDuration;
            float curve = transitionCurve.Evaluate(t);
            transform.position = Vector3.Slerp(startPos, target.position, curve);
            transform.rotation = Quaternion.Slerp(startRot, target.rotation, curve);
            yield return null;
        }

        transform.position = target.position;
        transform.rotation = target.rotation;
    }
}
