using System;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : MonoBehaviour {
    private float movementSpeed = 1.0f;
    private int level = 0;
    private int currentLetter = 0;
    private TextMesh spellText;
    private Animation animations;

    // TODO: Store word lists elsewhere
    private List<String> tier1Words = new List<String>();
    private List<String> tier2Words = new List<String>();
    private List<String> tier3Words = new List<String>();
    private List<String> tier4Words = new List<String>();

    private List<List<String>> listOfWordLists = new List<List<String>>();

    private void Awake() {
        initWordLists();

        animations = gameObject.transform.parent.GetComponent<Animation>();
        spellText = GetComponentInChildren<TextMesh>();
        spellText.richText = true;
        CheckForDirectionOnAwake();
    }

    private void CheckForDirectionOnAwake() {
        if (DetermineGoingRightOrLeft() == Constants.LEFT) {
            spellText.transform.RotateAround(transform.position, transform.right, 180f);
        }
    }

    private void Start() {
        SetWordForLevel();
        GameController.instance.AddTorpedoToList(this);
    }

    private void Update() {
        if (!animations.isPlaying) {
            MoveForward();
        }
    }

    private void MoveForward() {
        int rightOrLeft = DetermineGoingRightOrLeft();
        if (rightOrLeft == Constants.RIGHT) {
            transform.parent.position += Vector3.forward * Time.deltaTime * movementSpeed;
        } else if (rightOrLeft == Constants.LEFT){
            transform.parent.position += Vector3.back * Time.deltaTime * movementSpeed;
        }
    }

    private int DetermineGoingRightOrLeft() {
        if (gameObject.transform.position.y == 1) {
            return Constants.RIGHT;
        }
        if (gameObject.transform.position.y == -1) {
            return Constants.LEFT;
        }
        return -1; // Will return -1 if torpedo is in animation transition
    }

    private void SetWordForLevel() {
        spellText.text = "<color=red></color>" + GetRandomWordFromList(listOfWordLists[level]);
        currentLetter = 0;
    }

    private String GetRandomWordFromList(List<String> wordList) {
        int randomNum = (int)UnityEngine.Random.Range(0, wordList.Count);
        return wordList[randomNum];
    }

    public void ReceiveLetter(char input) {
        if (input == spellText.text.ToLower()[Constants.REDSOLVEDLETTERPOSITION + currentLetter]) {
            ColorCurrentLetter();
            currentLetter++;
            CheckForCompletedWord(currentLetter);
        }
    }
    
    // TODO: FIGURE OUT A BETTER WAY TO IMPLEMENT THIS THIS IS TERRIBLE
    public void BotSolveLetter(int playerNumber) {
        ColorCurrentLetter();
        currentLetter++;
        CheckForCompletedWord(currentLetter);
    }

    private void ColorCurrentLetter() {
        char letterChar = spellText.text[Constants.REDSOLVEDLETTERPOSITION + currentLetter];
        String newText = spellText.text.Remove(Constants.REDSOLVEDLETTERPOSITION + currentLetter, 1);
        newText = newText.Insert(Constants.REDHALFSOLVEDLETTERPOSITION + currentLetter, letterChar.ToString());
        spellText.text = newText;
    }

    private void CheckForCompletedWord(int letter) {
        // 1 is for the index, also need to add the constant for the current color to the length
        if (letter + Constants.REDSOLVEDLETTERPOSITION == spellText.text.Length) {
            if (level != Constants.MAXLEVEL) {
                level++;
            }
            SolveWord();
        }
    }

    private void SolveWord() {
        ChangeTorpedoDirection();
        SetWordForLevel();
        GetSolvingPlayer().TorpedoSolved();
        FlipSelectedByPlayer(GetSolvingPlayer().playerNumber);
    }

    private void ChangeTorpedoDirection() {
        spellText.transform.RotateAround(transform.position, transform.right, 180f);
        if (DetermineGoingRightOrLeft() == Constants.LEFT) {
            animations.Play("FlipTorpedoToRight");
        } else {
            animations.Play("FlipTorpedoToLeft");
        }
        movementSpeed += Constants.MOVEMENTINCREASEPERSOLVE;
    }

    public void FlipSelectedByPlayer(int playerNumber) {
        if (spellText.color == Constants.UNHIGHLIGHTED) {
            spellText.color = Constants.PLAYERHIGHLIGHTEDCOLORLIST[playerNumber];
        } else {
            spellText.color = Constants.UNHIGHLIGHTED;
        }
    }

    public bool IsSelectableByPlayer(int playerNumber) {
        try {
            return playerNumber == GetSolvingPlayer().playerNumber && !animations.isPlaying;
        } catch (NullReferenceException e) {
            return false;
        }
    }

    private Player GetSolvingPlayer() {
        return GameController.instance.GetPlayerBasedOnPlayerNumber(DetermineGoingRightOrLeft());
    }

    private void OnTriggerEnter(Collider other) {
        //if (other.gameObject.get)
    }

    private void initWordLists() {
        tier1Words.Add("Jam");
        tier1Words.Add("Lit");
        tier1Words.Add("Fit");
        tier1Words.Add("Hit");
        tier1Words.Add("How");
        tier1Words.Add("Cow");

        tier2Words.Add("Spit");
        tier2Words.Add("Rick");
        tier2Words.Add("Mitt");
        tier2Words.Add("Slit");
        tier2Words.Add("Yard");
        tier3Words.Add("Hard");
        tier3Words.Add("Fire");
        tier3Words.Add("Nice");

        tier3Words.Add("Flick");
        tier3Words.Add("Frizz");
        tier3Words.Add("Huzza");
        tier3Words.Add("Pizza");

        tier4Words.Add("Muzzle");
        tier4Words.Add("Puzzle");
        tier4Words.Add("Guzzle");
        tier4Words.Add("Little");

        listOfWordLists.Add(tier1Words);
        listOfWordLists.Add(tier2Words);
        listOfWordLists.Add(tier3Words);
        listOfWordLists.Add(tier4Words);
    }
}