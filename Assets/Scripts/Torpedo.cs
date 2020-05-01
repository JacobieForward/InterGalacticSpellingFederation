using System;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : MonoBehaviour {
    [SerializeField] float movementSpeed = 1.0f;

    int level = 0;
    int currentLetter = 0;
    TextMesh spellText;
    Animation animations;
    TorpedoMovement torpedoMovement;

    private void Awake() {
        animations = gameObject.transform.parent.GetComponent<Animation>();
        spellText = GetComponentInChildren<TextMesh>();
        torpedoMovement = GetComponent<TorpedoMovement>();
        spellText.richText = true;
    }

    private void Start() {
        SetWordForLevel();
        GameController.instance.AddTorpedoToList(this);
    }

    private void Update() {
        HighlightForSelectedPlayer(GameController.instance.DetermineWhichPlayerSelected(this));
    }

    private void HighlightForSelectedPlayer(int playerNumberSelectedBy) {
        // If not selected by a player -1 will be passed into this function from GameController.instance.DetermineWhichPlayerSelected so no change takes place
        if (playerNumberSelectedBy >= 0) {
            spellText.color = Constants.PLAYERHIGHLIGHTEDCOLORLIST[playerNumberSelectedBy];
        } else {
            spellText.color = Constants.UNHIGHLIGHTED;
        }
    }

    private void SetWordForLevel() {
        spellText.text = "<color=red></color>" + GameController.instance.torpedoWordList.GetRandomWordForLevel(level);
        currentLetter = 0;
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
        torpedoMovement.ChangeTorpedoDirection();
        SetWordForLevel();
        GetSolvingPlayer().TorpedoSolved();
    }

    public bool IsSelectableByPlayer(int playerNumber) {
        try {
            return playerNumber == GetSolvingPlayer().playerNumber && !animations.isPlaying;
        } catch (NullReferenceException e) {
            return false;
        }
    }

    private Player GetSolvingPlayer() {
        return GameController.instance.GetPlayerBasedOnPlayerNumber(torpedoMovement.DetermineGoingRightOrLeft());
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(this);
    }
}