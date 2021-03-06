﻿using System;
using UnityEngine;

public class BotPlayer : Player {
    [SerializeField] int difficulty = Constants.DIFFICULTYEASY;

    private float torpedoLetterSolveTime;
    private float torpedoLetterSolveCounter;

    private void Awake() {
        torpedoLetterSolveTime = 2.0f / difficulty;
        torpedoLetterSolveCounter = 0.0f;
    }

    private void Start() {
        SelectClosestTorpedoFromList();
    }

    private void Update() {
        SolveTorpedoAutomatically();
        DetectIfSelectedByPlayer();
        if (selectedTorpedo == null) {
            SelectClosestTorpedoFromList();
        }
        if (health <= 0) {
            Lose();
        }
    }

    private void DetectIfSelectedByPlayer() {
        
    }

    private void Lose() {
        Destroy(gameObject);
    }

    private void SolveTorpedoAutomatically() {
        torpedoLetterSolveCounter += Time.deltaTime;
        CheckForSolvedLetter();
    }

    private void CheckForSolvedLetter() {
        if (torpedoLetterSolveCounter >= torpedoLetterSolveTime && selectedTorpedo != null) {
            selectedTorpedo.BotSolveLetter(playerNumber);
            torpedoLetterSolveCounter = 0.0f;
        }
    }

    private void SelectClosestTorpedoFromList() {
        Torpedo closestTorpedo = null;
        foreach (Torpedo torpedo in GameController.instance.GetTorpedoList()) {
            if (torpedo.IsSelectableByPlayer(playerNumber)) {
                if (closestTorpedo == null || Vector3.Distance(closestTorpedo.transform.position, gameObject.transform.position) > Vector3.Distance(torpedo.transform.position, gameObject.transform.position)) {
                    closestTorpedo = torpedo;
                }
            }
        }
        if (closestTorpedo != null) {
            SelectTorpedo(closestTorpedo);
        }
    }

    private void SelectTorpedo(Torpedo torpedo) {
        selectedTorpedo = torpedo;
    }
}
