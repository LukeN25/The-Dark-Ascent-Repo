using UnityEngine;
using System;
using System.Collections;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] private Color flashColour = Color.red;
    [SerializeField]
    [Range(0.01f, 1f)]
    private float flashDuration = 0.3f;

    private Color originalColour;
    private Renderer rend;

    private void Start()
    {
        originalColour = rend.material.color;
        rend = GetComponent<MeshRenderer>();
    }

    private IEnumerator Flash()
    {
        rend.material.color = flashColour;
        yield return new WaitForSeconds(flashDuration);
        rend.material.color = originalColour;
    }
}
