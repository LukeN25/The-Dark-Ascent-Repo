using UnityEngine;
using System.Collections;

public class MenuCameraController : MonoBehaviour
{
    [Header("Focus Points")]
    public Transform defaultFocus;

    [Header("Settings")]
    public float transitionDuration = 1.0f;
    public AnimationCurve transitionCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Coroutine currentMove;

    public void FocusOn(Transform target)
    {
        if (target == null) return;
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
        Vector3 endPos = target.position;
        Quaternion endRot = target.rotation;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / Mathf.Max(0.0001f, transitionDuration);
            float curve = transitionCurve.Evaluate(Mathf.Clamp01(t));
            transform.position = Vector3.Slerp(startPos, endPos, curve);
            transform.rotation = Quaternion.Slerp(startRot, endRot, curve);
            yield return null;
        }

        transform.position = endPos;
        transform.rotation = endRot;
        currentMove = null;
    }
}
