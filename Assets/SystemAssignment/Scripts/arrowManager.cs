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
