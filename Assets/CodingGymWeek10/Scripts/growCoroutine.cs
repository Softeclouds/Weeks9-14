using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class growCoroutine : MonoBehaviour
{
    public Vector3 targetScale = new Vector3(2f, 2f, 2f);
    public float duration = 5f;
    public AnimationCurve curve;

    void Start()
    {
        StartCoroutine(grow(targetScale, duration));
    }

    public IEnumerator grow(Vector3 targetScale, float duration)
    {
        Vector3 scale = transform.localScale;
        float t = 0f;

        while (t < duration)
        {
            float curveValue = curve.Evaluate(t/duration);
            transform.localScale = Vector3.Lerp(scale, targetScale, curveValue);
            t += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }
}
