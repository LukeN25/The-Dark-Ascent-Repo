using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [SerializeField, Range(0.001f, 0.01f)] private float Amount = 0.002f;
    [SerializeField, Range(1f, 50f)] private float Frequency = 10.0f;
    [SerializeField, Range(1f, 100f)] private float Smooth = 10.0f;

    Vector3 StartPos;

    void Start()
    {
        StartPos = transform.localPosition;
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

    private Vector3 StartHeadBob()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * Frequency) * Amount * 1.4f, Smooth * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * Frequency / 2f) * Amount * 1.6f, Smooth * Time.deltaTime);
        transform.localPosition += pos;

        return pos;
    }

    private void StopHeadBob()
    {
        if (transform.localPosition == StartPos) return;
        transform.localPosition = Vector3.Lerp(transform.localPosition, StartPos, 1 * Time.deltaTime);
    }
}
