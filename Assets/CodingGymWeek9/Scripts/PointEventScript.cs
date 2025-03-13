using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PointEventScript : MonoBehaviour
{
    public SpriteRenderer sr;
    public List<Sprite> spriteList;
    public Sprite defaultSprite;
    public GameObject alien;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pointerEnter()
    {
        sr.sprite = spriteList[Random.Range(0, 4)];
    }

    public void pointerExit()
    {
        sr.sprite = defaultSprite;
    }

    public void pointerClick()
    {
        Vector2 spawnPos = new Vector2(Random.Range(-8, 8), Random.Range(4, -4));
        
        GameObject aliens = Instantiate(alien, spawnPos, Quaternion.identity);

    }
}
