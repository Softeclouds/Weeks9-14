using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EventsDemo : MonoBehaviour
{
    public UnityEvent OnTimerFinished;
    public RectTransform image;

    public float t;
    public float timerT = 3;

    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t = t + Time.deltaTime;
        if(t>timerT)
        {
            t = 0;
            OnTimerFinished.Invoke();
        }
    }

    public void OnMouseOverImage()
    {
       
        Debug.Log("the mouse went over");
        image.localScale = Vector3.one *1.5f;
    }

    public void OnMouseLeftImage()
    {
        Debug.Log("the mouse left the image");
        image.localScale = Vector3.one;
    }

 
}
