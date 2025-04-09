using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class gameManager : MonoBehaviour
{
    // Game Canvases //
    public Canvas gameCanvas;
    public Canvas outGameCanvas;
    public Canvas menuCanvas;
    public Canvas difficultySelectCanvas;

    public inputManager inputManager;

    public TextMeshProUGUI scoreText;

    public void startUp()
    {
        gameCanvas.enabled = false;
        outGameCanvas.enabled = false;
        menuCanvas.enabled = true;
        difficultySelectCanvas.enabled = false;
    }

    public void inGame()
    {
        gameCanvas.enabled = true;
        outGameCanvas.enabled = false;
        menuCanvas.enabled = false;
        difficultySelectCanvas.enabled = false;
    }

    public void outGame()
    {
        gameCanvas.enabled = false;
        outGameCanvas.enabled = true;
        menuCanvas.enabled = false;
        difficultySelectCanvas.enabled = false;

        scoreText.text = ("Score: "+inputManager.score);
    }

    public void playButton()
    {
        gameCanvas.enabled = false;
        outGameCanvas.enabled = false;
        menuCanvas.enabled = false;
        difficultySelectCanvas.enabled = true;
    }

    public void replayButton()
    {
        inputManager.score = 0;
        gameCanvas.enabled = true;
        outGameCanvas.enabled = false;
        menuCanvas.enabled = false;
        difficultySelectCanvas.enabled = false;
    }

    public void menuButton()
    {
        inputManager.score = 0;
        gameCanvas.enabled = false;
        outGameCanvas.enabled = false;
        menuCanvas.enabled = true;
        difficultySelectCanvas.enabled = false;
    }

}
