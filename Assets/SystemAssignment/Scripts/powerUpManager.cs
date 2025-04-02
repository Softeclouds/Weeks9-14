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
    float fadeSpeed = 2f;       // How fast it fades away
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
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
