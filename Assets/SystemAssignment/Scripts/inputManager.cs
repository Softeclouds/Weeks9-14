using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class inputManager : MonoBehaviour
{
    public class Sequence // making a sequence class
    {
        public List<KeyCode> keyList;
        public int difficulty;

        public Sequence(List<KeyCode> keyList)
        {
            this.keyList = keyList;
        }
    }

    // VARIABLES //
    // arrows //
    KeyCode Up = KeyCode.UpArrow;
    KeyCode Down = KeyCode.DownArrow;
    KeyCode Right = KeyCode.RightArrow;
    KeyCode Left = KeyCode.LeftArrow;
    // Sequences //
    private List<Sequence> easySequences = new List<Sequence>();
    private List<Sequence> mediumSequences = new List<Sequence>();
    private List<Sequence> hardSequences = new List<Sequence> ();
    private List<Sequence> powerUpSequences = new List<Sequence>();
    private bool difficultyChosen = false;
    // UI //
    public Slider timer;
    private float t = 0f;
    private float timerT;
    private int score;
    public TextMeshProUGUI scoreText;
    // UnityEvents //
    public UnityEvent onEasySelected;
    public UnityEvent onMediumSelected;
    public UnityEvent onHardSelected;
    public UnityEvent onPowerUpEnabled;

    private List<Sequence> selectedList = new List<Sequence>(); // currentList to pick from

    void Start()
    {
        CreateSequences(); // Creates all sequences
    }

    void Update()
    {
        scoreText.text = "Score: " + score; // Update the score

        if (difficultyChosen) // is the game has started, then increase time and check for a game over
        {
            // increase time and update slider
            t += Time.deltaTime;
            timer.value = t % timer.maxValue;

            // if the time excedes the limit, end the game
            if (t >= timerT)
            {
                Debug.Log("Game Over");
                t = 0f;
            }
        }

    }

    void CreateSequences()
    {
        // creating easy sequences
        easySequences.Add(new Sequence(new List<KeyCode> { Up, Up, Up }));
        easySequences.Add(new Sequence(new List<KeyCode> { Down, Up, Down }));
        easySequences.Add(new Sequence(new List<KeyCode> { Right , Left, Up }));
        easySequences.Add(new Sequence(new List<KeyCode> { Right, Left, Down }));
        easySequences.Add(new Sequence(new List<KeyCode> { Right, Down, Down }));
        easySequences.Add(new Sequence(new List<KeyCode> { Left, Up, Up }));
        easySequences.Add(new Sequence(new List<KeyCode> { Left, Down, Up, Up }));
        easySequences.Add(new Sequence(new List<KeyCode> { Down, Down, Left, Up }));
        easySequences.Add(new Sequence(new List<KeyCode> { Up, Right, Down, Left }));
        easySequences.Add(new Sequence(new List<KeyCode> { Left, Right, Left, Right }));
        Debug.Log("Easy: " + easySequences.Count);

        mediumSequences.Add(new Sequence(new List<KeyCode> { Up, Down, Right, Right, Left }));
        mediumSequences.Add(new Sequence(new List<KeyCode> { Left, Right, Up, Right, Down }));
        mediumSequences.Add(new Sequence(new List<KeyCode> { Down, Right, Down, Left, Down, Up }));
        mediumSequences.Add(new Sequence(new List<KeyCode> { Right, Up, Down, Down, Left, }));
        Debug.Log("Medium: " +  mediumSequences.Count);

        hardSequences.Add(new Sequence(new List<KeyCode> { Down, Up, Right, Left, Up, Up, Left }));
        hardSequences.Add(new Sequence(new List<KeyCode> { Right, Up, Left, Down, Down, Right, Right }));
        hardSequences.Add(new Sequence(new List<KeyCode> { Left, Right, Right, Down, Left, Up, Down }));
        hardSequences.Add(new Sequence(new List<KeyCode> { Up, Left, Right, Down, Down, Left, Up }));
        Debug.Log("Hard: " + hardSequences.Count);


        Debug.Log("PowerUp: " + powerUpSequences.Count);
    }

    public void SelectDifficulty(int difficultyLevel)
    {
        // if the difficulty level is equal to one of the levels, update the selectedList
        if (difficultyLevel == 1) { onEasySelected?.Invoke(); }
        else if (difficultyLevel == 2) { onMediumSelected?.Invoke(); }
        else if (difficultyLevel == 3) { onHardSelected?.Invoke(); }
        else if (difficultyLevel == 4) { onPowerUpEnabled?.Invoke(); }

    }

    public void SetListEasy()
    {
        Debug.Log("You have chosen EASY");
        selectedList = easySequences;
        timerT = 120;
    }

    public void SetListMedium()
    {
        Debug.Log("You have chosen MEDIUM");
        selectedList.AddRange(easySequences);
        selectedList.AddRange(mediumSequences);
        timerT = 60;
    }

    public void SetListHard()
    {
        Debug.Log("You have chosen HARD");
        selectedList.AddRange(easySequences);
        selectedList.AddRange(mediumSequences);
        selectedList.AddRange(hardSequences);
        timerT = 30;
    }

    public void SetListPowerUp()
    {
        selectedList = powerUpSequences;
    }

}
