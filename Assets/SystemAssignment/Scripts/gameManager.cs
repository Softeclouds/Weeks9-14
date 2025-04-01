using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class gameManager : MonoBehaviour
{
    // Game Canvases //
    public Canvas gameCanvas;
    public Canvas outGameCanvas;
    public Canvas menuCanvas;
    public Canvas difficultySelectCanvas;

    public bool difficultyChosen = false;

    public void startUp()
    {
        gameCanvas.enabled = false;
        outGameCanvas.enabled = false;
        menuCanvas.enabled = true;
        difficultySelectCanvas.enabled = false;
    }

    public void inGame()
    {
        difficultyChosen = true;
        gameCanvas.enabled = true;
        outGameCanvas.enabled = false;
        menuCanvas.enabled = false;
        difficultySelectCanvas.enabled = false;
    }

    public void outGame()
    {
        difficultyChosen = false;
        gameCanvas.enabled = false;
        outGameCanvas.enabled = true;
        menuCanvas.enabled = false;
        difficultySelectCanvas.enabled = false;
    }

    public void playButton()
    {
        gameCanvas.enabled = false;
        outGameCanvas.enabled = false;
        menuCanvas.enabled = false;
        difficultySelectCanvas.enabled = true;
    }

}
