using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class powerUpManager : MonoBehaviour
{

    float minSpawnTime = 5f;    // Min time before spawning
    float maxSpawnTime = 10f;   // max time before spawning
    float minVisibleTime = 3f;  // min time it stays visible
    float maxVisibleTime = 6f;  // max time it stays visible
    float fadeSpeed = 0.5f;       // How fast it fades away
    float powerUpDuration = 3f; // How long the power up lasts



    public inputManager inputManager;
    public arrowManager arrowManager;

    private Image image;
    private Button button;
    private bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        inputManager.onPowerUpActivated.AddListener(inputManager.SetListPowerUp);
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
                isActive = false;
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

        button.interactable = true;
    }

    IEnumerator FadeOut()
    {
        button.interactable = false;
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

    public void ActivatePowerUp()
    {
        
        Debug.Log("PowerUp Activated!");
        isActive = false;
        Debug.Log(inputManager.selectedList.Count);

        inputManager.onPowerUpActivated.Invoke();
        arrowManager.ClearArrows();
        inputManager.SelectSequence();

        StartCoroutine(FadeOut());
        StartCoroutine(PowerUpEffect());
    }

    IEnumerator PowerUpEffect()
    {
        string lastDifficulty = inputManager.gameDifficulty;
        Debug.Log(lastDifficulty);
        yield return new WaitForSeconds(powerUpDuration);
        Debug.Log("PowerUp effect ended!");

        inputManager.powerUpEnabled = false;

        if (lastDifficulty == "EASY") { inputManager.selectedList = inputManager.easySequences; }
        else if (lastDifficulty == "MEDIUM") { inputManager.selectedList = inputManager.mediumSequences; }
        else if (lastDifficulty == "HARD") { inputManager.selectedList = inputManager.hardSequences; }

    }
}
