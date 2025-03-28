using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class knight : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator animator;
    public float speed = 2f;
    public bool canRun = true;

    public AudioSource audioSource;
    public AudioClip[] clips;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float direction = Input.GetAxis("Horizontal");

        sr.flipX = direction < 0;
        animator.SetFloat("Movement", Mathf.Abs(direction));

        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
            canRun = false;
        }

        if(canRun)
        {
            transform.position += transform.right * direction * speed * Time.deltaTime;
        }
    }

    public void endAttack()
    {
        Debug.Log("Attack has finished");
        canRun = true;
    }

    public void playFootStep()
    {
        audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
        // syncs audio with animation
    }
}
