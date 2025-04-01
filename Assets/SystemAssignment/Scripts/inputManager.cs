using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class inputManager : MonoBehaviour
{
    public class Sequence // making a sequence class
    {
        public List<KeyCode> keyList;
        public int difficulty;

        public Sequence(int difficulty, List<KeyCode> keyList)
        {
            this.difficulty = difficulty;
            this.keyList = keyList;
        }
    }

    // VARIABLES //
    // arrows //
    KeyCode UP = KeyCode.UpArrow;
    KeyCode DOWN = KeyCode.DownArrow;
    KeyCode RIGHT = KeyCode.RightArrow;
    KeyCode LEFT = KeyCode.LeftArrow;
    // Sequences //
    private List<Sequence> easySequences = new List<Sequence>();
    private List<Sequence> mediumSequences = new List<Sequence>();
    private List<Sequence> hardSequences = new List<Sequence> ();
    private List<Sequence> powerUpSequences = new List<Sequence>();
    // UI //
    public Slider timer;
    private float t = 0f;
    public float timerT;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        CreateSequences(); // Creates all sequences
    }

    void Update()
    {
        // increase time and update slider
        t += Time.deltaTime;
        timer.value = t % timer.maxValue;
    }

    void CreateSequences()
    {
        // creating easy sequences
        easySequences.Add(new Sequence(1, new List<KeyCode> { UP, UP, UP }));
        easySequences.Add(new Sequence(1, new List<KeyCode> { DOWN, UP, DOWN }));
        easySequences.Add(new Sequence(1, new List<KeyCode> { RIGHT, LEFT, UP }));
        Debug.Log("Easy: " + easySequences.Count);
    }

    void SelectSequence(int difficultyLevel)
    {
        List<Sequence> selectedList = new List<Sequence>(); // update the selected list to be the list from the difficulty chosen

        // if the difficulty level is equal to one of the levels, update the selectedList
        if (difficultyLevel == 1) { selectedList = easySequences; }
        else if (difficultyLevel == 2) { selectedList = mediumSequences; }
        else if (difficultyLevel == 3) { selectedList = hardSequences; }
        else if (difficultyLevel == 4) { selectedList = powerUpSequences; }

    }

}
