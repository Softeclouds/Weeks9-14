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
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            Debug.Log("PowerUp spawned !");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
