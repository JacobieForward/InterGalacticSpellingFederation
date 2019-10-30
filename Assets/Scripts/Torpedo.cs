using System;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : MonoBehaviour {
    private float movementSpeed = 1.0f;
    int level = 0;
    int currentLetter = 0;
    int currentDirection = Constants.RIGHT;
    TextMesh spellText;

    // TODO: Store word lists elsewhere
    List<String> tier1Words = new List<String>();
    List<String> tier2Words = new List<String>();
    List<String> tier3Words = new List<String>();
    List<String> tier4Words = new List<String>();

    List<List<String>> listOfWordLists = new List<List<String>>();

    private void Awake() {
        spellText = GetComponentInChildren<TextMesh>();
        spellText.richText = true;

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

    private void Start() {
        SetWordForLevel();
    }

    private void Update() {
        MoveForward();
    }

    private void MoveForward() {
        if (DetermineGoingRightOrLeft() == Constants.RIGHT) {
            transform.position += Vector3.forward * Time.deltaTime * movementSpeed;
        } else {
            transform.position += Vector3.back * Time.deltaTime * movementSpeed;
        }
    }

    private int DetermineGoingRightOrLeft() {
        if (currentDirection % 2 == 0) {
            return Constants.LEFT;
        } else {
            return Constants.RIGHT;
        }
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
            SetWordForLevel();
            ChangeTorpedoDirection();
        }
    }

    private void ChangeTorpedoDirection() {
        transform.RotateAround(transform.position, transform.right, 180f);
        spellText.transform.RotateAround(transform.position, transform.right, 180f);
        currentDirection += 1;
    }

    public void FlipColor() {
        if (spellText.color == Constants.UNHIGHLIGHTED) {
            spellText.color = Constants.HIGHLIGHTED;
        } else {
            spellText.color = Constants.UNHIGHLIGHTED;
        }
    }
}