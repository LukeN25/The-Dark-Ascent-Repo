using System.Collections;
using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [SerializeField, Range(0.001f, 0.01f)] private float Amount = 0.002f;
    [SerializeField, Range(1f, 50f)] private float Frequency = 10.0f;
    [SerializeField, Range(1f, 100f)] private float Smooth = 10.0f;

    [Header("Hit Shake")]
    [SerializeField] private float hitShakeAmount = 0.008f;
    [SerializeField] private float hitShakeFrequency = 30f;
    [SerializeField] private float hitShakeDuration = 0.25f;

    Vector3 StartPos;
    private float originalAmount;
    private float originalFrequency;

    void Start()
    {
        StartPos = transform.localPosition;
        originalAmount = Amount;
        originalFrequency = Frequency;
    }

    public void TriggerHitShake()
    {
        Debug.Log("Hit shake triggered");   
        StopAllCoroutines();
        StartCoroutine(HitShakeRoutine());
    }

    private IEnumerator HitShakeRoutine()
    {
        Amount = hitShakeAmount;
        Frequency = hitShakeFrequency;
        yield return new WaitForSeconds(hitShakeDuration);
        Amount = originalAmount;
        Frequency = originalFrequency;
    }

    void Update()
    {
        CheckForHeadbobTrigger();
        StopHeadBob();
    }

    private void CheckForHeadbobTrigger()
    {
        float inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;

        if (inputMagnitude > 0)
        {
            StartHeadBob();
        }
    }

    private void StartHeadBob()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * Frequency) * Amount * 1.4f, Smooth * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * Frequency / 2f) * Amount * 1.6f, Smooth * Time.deltaTime);
        transform.localPosition += pos;
    }

    private void StopHeadBob()
    {
        if (transform.localPosition == StartPos) return;
        transform.localPosition = Vector3.Lerp(transform.localPosition, StartPos, 1 * Time.deltaTime);
    }
}
