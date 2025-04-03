using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveWhenClick : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 targetPos;
    private bool isMoving = false;

    public Tilemap stoneTilemap;

    public AudioSource audioSource;
    public AudioClip[] clips;

    private SpriteRenderer sr;
    private Animator animator;

    private bool isOnStone = false;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.position;

        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) // if mouse is being pressed
        {
            // Debug.Log("CLICK");
            SetTargetPos(Input.mousePosition);
        }

        if (isMoving)
        {
            // Debug.Log("MOVE");
            Move();
            CheckTile();
        }
    }

    void SetTargetPos(Vector2 mousePos)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0f)); // get the click location and change it to a world location
        if (stoneTilemap.HasTile(stoneTilemap.WorldToCell(worldPosition))) // Check if the tile exists in the stone tilemap
        {
            targetPos = new Vector2(worldPosition.x, worldPosition.y);
            isMoving = true;
        }
        else
        {
            Debug.Log("Click on the stone, not grass");
        }
    }

    void Move()
    {
        Vector2 pos = transform.position;
        Vector2 direction = (targetPos - pos).normalized;

        sr.flipX = direction.x < 0;
        animator.SetFloat("Movement", Mathf.Abs(direction.x));

        transform.position = new Vector2(pos.x + direction.x * speed * Time.deltaTime, pos.y + direction.y * speed * Time.deltaTime); // update position with direction and speed


        if (Vector2.Distance(transform.position, targetPos) < 0.1f)
        {
            isMoving = false; // stop moving if target is reached
            animator.SetFloat("Movement", 0);
        }
    }

    void CheckTile()
    {
        Vector2 playerPos = transform.position;

        Vector3 cellWorldPos = stoneTilemap.WorldToCell(playerPos); // convert players pos to tilemap world position
        Vector3 cellCenterWorldPosition = stoneTilemap.GetCellCenterWorld(stoneTilemap.WorldToCell(playerPos)); // calculate center of the tile

        if (stoneTilemap.HasTile(stoneTilemap.WorldToCell(playerPos)) && Vector3.Distance(playerPos, cellCenterWorldPosition) < 0.5f) // check if the player is on a stone tile
        {
            Debug.Log("on stone!");
            isOnStone = true;

        }
        else
        {
            isOnStone = false;
        }
    }

        public void playFootStep()
        {
            if (isOnStone) // only plays audio is the player is on stone
            {
                audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
                // syncs audio with animation
            }
        }
    
}
