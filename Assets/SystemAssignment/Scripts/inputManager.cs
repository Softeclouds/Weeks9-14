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
    public arrowManager arrowManager;
    KeyCode Up = KeyCode.UpArrow;
    KeyCode Down = KeyCode.DownArrow;
    KeyCode Right = KeyCode.RightArrow;
    KeyCode Left = KeyCode.LeftArrow;
    // Sequences //
    private List<Sequence> easySequences = new List<Sequence>();
    private List<Sequence> mediumSequences = new List<Sequence>();
    private List<Sequence> hardSequences = new List<Sequence> ();
    private List<Sequence> powerUpSequences = new List<Sequence>();
    private Sequence currentSequence;
    private int currentIndex = 0;
    private int penalty;
    // UI //
    public Slider timer;
    private float t = 0f;
    private float timerT;
    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI difficultyText;
    // UnityEvents //
    public UnityEvent onEasySelected;
    public UnityEvent onMediumSelected;
    public UnityEvent onHardSelected;
    public UnityEvent onPowerUpEnabled;

    public gameManager gameManager;

    private List<Sequence> selectedList = new List<Sequence>(); // currentList to pick from

    void Start()
    {
        CreateSequences(); // Creates all sequences
        gameManager.startUp();
        selectedList = easySequences;
        SelectSequence();
    }

    void Update()
    {
        scoreText.text = "Score: " + score; // Update the score

        if (gameManager.gameCanvas.enabled) // is the game has started, then increase time and check for a game over
        {
            // increase time and update slider
            t += Time.deltaTime;
            timer.value = t % timer.maxValue;

            // if the time excedes the limit, end the game
            if (t >= timerT)
            {
                Debug.Log("Game Over");
                t = 0f;
                gameManager.outGame();
            }

            // checking for anykey so that mistakes can be accounted for
            if (Input.anyKeyDown)
            {
                // checking to see if the current index hasnt exceded the list count and if it matches the button pressed
                if (currentIndex < currentSequence.keyList.Count && IsMatchingInput(currentSequence.keyList[currentIndex]))
                {
                    arrowManager.CorrectArrow(currentIndex); // change the current arrow to invisible
                    currentIndex++; // update the current index to the next one
                }

                else
                {
                    OnMistake();
                    t = t + penalty; // increases the time by the difficulties pentaly ammount to increase challenge
                }
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

        powerUpSequences.Add(new Sequence(new List<KeyCode> { Up }));
        powerUpSequences.Add(new Sequence(new List<KeyCode> { Left }));
        powerUpSequences.Add(new Sequence(new List<KeyCode> { Down }));
        powerUpSequences.Add(new Sequence(new List<KeyCode> { Right }));
        Debug.Log("PowerUp: " + powerUpSequences.Count);
    }

    void SelectSequence()
    {

        currentSequence = selectedList[Random.Range(0, selectedList.Count)];
        currentIndex = 0;
        arrowManager.DisplaySequence(currentSequence.keyList);
    }

    // Takes the expected index keycode and checks if it matches any of the possible key inputs for that keycode
    bool IsMatchingInput(KeyCode expectedKey)
    {
        if (expectedKey == KeyCode.UpArrow)
            return Input.GetKeyDown(Up) || Input.GetKeyDown(KeyCode.W);

        if (expectedKey == KeyCode.DownArrow)
            return Input.GetKeyDown(Down) || Input.GetKeyDown(KeyCode.S);

        if (expectedKey == KeyCode.LeftArrow)
            return Input.GetKeyDown(Left) || Input.GetKeyDown(KeyCode.A);

        if (expectedKey == KeyCode.RightArrow)
            return Input.GetKeyDown(Right) || Input.GetKeyDown(KeyCode.D);

        return false; // False if it doesnt match
    }

    void OnMistake()
    {
        currentIndex = 0;
        arrowManager.WrongArrows();
    }



    public void SelectDifficulty(string difficultyLevel)
    {
        // if the difficulty level is equal to one of the levels, update the selectedList
        if (difficultyLevel == "EASY") { onEasySelected?.Invoke(); }
        else if (difficultyLevel == "MEDIUM") { onMediumSelected?.Invoke(); }
        else if (difficultyLevel == "HARD") { onHardSelected?.Invoke(); }
        else if (difficultyLevel == "POWERUP") { onPowerUpEnabled?.Invoke(); }
        difficultyText.text = ("Difficulty: " + difficultyLevel);

    }

    public void SetListEasy()
    {
        Debug.Log("You have chosen EASY");
        selectedList = easySequences;
        timerT = 120;
        penalty = 1;
        gameManager.inGame();
    }

    public void SetListMedium()
    {
        Debug.Log("You have chosen MEDIUM");
        selectedList.AddRange(easySequences);
        selectedList.AddRange(mediumSequences);
        timerT = 60;
        penalty = 2;
        gameManager.inGame();
    }

    public void SetListHard()
    {
        Debug.Log("You have chosen HARD");
        selectedList.AddRange(easySequences);
        selectedList.AddRange(mediumSequences);
        selectedList.AddRange(hardSequences);
        timerT = 30;
        penalty = 3;
        gameManager.inGame();
    }

    public void SetListPowerUp()
    {
        selectedList = powerUpSequences;
    }

   

}
