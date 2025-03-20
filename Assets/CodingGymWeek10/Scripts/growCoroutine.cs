using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class growCoroutine : MonoBehaviour
{
    public Vector3 targetScale = new Vector3(2f, 2f, 2f);
    public float duration = 5f;
    public AnimationCurve curve;
    public Button button;

    void Start()
    {
        
    }

    public IEnumerator grow(Vector3 targetScale, float duration)
    {
        Debug.Log("currently attacking");
        button.interactable = false;
        Vector3 scale = transform.localScale;
        float t = 0f;

        while (t < duration)
        {
            float curveValue = curve.Evaluate(t/duration);
            transform.localScale = Vector3.Lerp(scale, targetScale, curveValue);
            t += Time.deltaTime;
            yield return null;
        }

        if(t >= duration)
        {
            Debug.Log("Attact is over");
            button.interactable = true;
        }

        transform.localScale = targetScale;
    }


    public void onAttack()
    {
        StartCoroutine(grow(targetScale, duration));
    }
}
