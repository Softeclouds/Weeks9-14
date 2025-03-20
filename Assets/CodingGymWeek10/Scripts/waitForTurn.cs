using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class waitForTurn : MonoBehaviour
{
    public Button buttonA;
    public Button buttonB;

    void Start()
    {
        StartCoroutine(waitTurn());
    }

    public IEnumerator waitTurn()
    {
        bool isButtonAActive = true; // sets the first button to active
        while (true)
        {
            buttonA.interactable = isButtonAActive;
            buttonB.interactable = !isButtonAActive; // makes the second button the opposite of the first buttons state

            yield return WaitForButtonPress(isButtonAActive ? buttonA : buttonB); // if button A is true, return A, otherwise return B

            isButtonAActive = !isButtonAActive; // switch the active state to its opposite
        }
    }

    public IEnumerator WaitForButtonPress(Button button)
    {
        bool pressed = false;
        button.onClick.AddListener(() => pressed = true); // Listen for when the button has been pressed

        while (!pressed) // if it has not been pressed do nothing
        {
            yield return null;
        }

        button.onClick.RemoveAllListeners(); // removes all the listeners so they dont stack up
    }
}
