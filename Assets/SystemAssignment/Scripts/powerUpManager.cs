using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class powerUpManager : MonoBehaviour
{

    float minSpawnTime = 5f;    // Min time before spawning
    float maxSpawnTime = 10f;   // max time before spawning
    float minVisibleTime = 3f;  // min time it stays visible
    float maxVisibleTime = 6f;  // max time it stays visible
    float fadeSpeed = 0.5f;       // How fast it fades away
    float powerUpDuration = 5f; // How long the power up lasts

    private Image image;
    private Button button;
    private bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        // Getting components 
        image = GetComponent<Image>();
        button = GetComponent<Button>();

        StartCoroutine(SpawnPowerUp());
    }

    IEnumerator SpawnPowerUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime)); // wait a random amount of time within the range before spawing a powerup
            Debug.Log("PowerUp spawned !");
            RectTransform rectTransform = GetComponent<RectTransform>(); // Getting the transform of the button
            Vector2 randomPos = new Vector2 (Random.Range(100, Screen.width - 100), Random.Range(100, Screen.height - 100)); // Get a random screen position
            rectTransform.position = randomPos; // Apply position to the button transfrom

            StartCoroutine(FadeIn()); // Fade in at new position
            isActive = true;
            yield return new WaitForSeconds(Random.Range(minVisibleTime, maxVisibleTime)); // Wait the random visiblity time

            if (isActive) // If it hasnt been clicked in time, fade out
            {
                yield return StartCoroutine(FadeOut());
            }
        }
    }

    IEnumerator FadeIn()
    {
        float alpha = 0;
        Color color = image.color;
        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            color.a = alpha;
            image.color = color;
            yield return null;
        }
        color.a = 1;
        image.color = color;
    }

    IEnumerator FadeOut()
    {
        float alpha = 1;
        Color color = image.color;
        while (alpha < 0)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            color.a = alpha;
            image.color = color;
            yield return null;
        }
        color.a = 0;
        image.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
