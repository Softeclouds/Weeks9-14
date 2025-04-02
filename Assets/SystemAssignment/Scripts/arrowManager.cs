using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class arrowManager : MonoBehaviour
{
    public List<Sprite> arrowSprites;    // List of arrow sprites (Up, Right, Down, Left)
    public GameObject arrowPrefab;       // Prefab with an Image component
    public Transform parentTransform;    // Empty GameObject to hold arrows
    public float spacing = 100f;         // Space between arrows

    private List<GameObject> activeArrows = new List<GameObject>();

    public void DisplaySequence(List<KeyCode> sequence)
    {
        // starts drawing sprites from the center, keeping the spacing the same
        float totalWidth = (sequence.Count - 1) * spacing;
        float startX = -totalWidth / 2f;


        // for all arrows in that sequence, instantiate the prefab, set the image, position and color, and add it to the active list
        for (int i = 0; i < sequence.Count; i++)
        {
            GameObject newArrow = Instantiate(arrowPrefab, parentTransform);
            newArrow.GetComponent<Image>().sprite = GetArrowSprite(sequence[i]);
            newArrow.transform.localPosition = new Vector3(startX + (i * spacing), 0, 0);
            newArrow.GetComponent<Image>().color = Color.white;         // no color
            activeArrows.Add(newArrow);
        }
    }

    // Makes the arrows invisible if the correct key is pressed
    public void CorrectArrow(int index)
    {
        if (index < activeArrows.Count) // if the current sequence hasnt been completed
        {
            activeArrows[index].GetComponent<Image>().color = Color.clear; // set the current arrow in the sequence to invisible
        }
    }

    // Sets all arrows back to being visible if the wrong key is pressed
    public void WrongArrows()
    {
        foreach (GameObject arrow in activeArrows)
        {
            arrow.GetComponent<Image>().color = Color.white;
        }
    }

    // Destroys active arrows and clears the list. Used for after completing a sequence
    private void ClearArrows()
    {
        foreach (GameObject arrow in activeArrows)
        {
            Destroy(arrow);
        }
        activeArrows.Clear();
    }

    // get the correct sprite
    private Sprite GetArrowSprite(KeyCode key)
    {
        if (key == KeyCode.UpArrow)
            return arrowSprites[0];
        if (key == KeyCode.RightArrow)
            return arrowSprites[1];
        if (key == KeyCode.DownArrow)
            return arrowSprites[2];
        if (key == KeyCode.LeftArrow)
            return arrowSprites[3];

        return null; // if nothing matches 
    }
}
