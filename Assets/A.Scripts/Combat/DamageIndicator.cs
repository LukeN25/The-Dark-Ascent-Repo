using UnityEngine;
using System;
using System.Collections;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] private Color flashColour = Color.red;
    [SerializeField]
    [Range(0.01f, 1f)]
    private float flashDuration = 0.3f;
    [SerializeField] private SkinnedMeshRenderer rend;

    private Color originalColour;

    private void Start()
    {
        originalColour = rend.material.color;
    }

    private IEnumerator FlashTimed()
    {
        rend.material.color = flashColour;
        yield return new WaitForSeconds(flashDuration);
        rend.material.color = originalColour;
    }

    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(FlashTimed());
    }
}
