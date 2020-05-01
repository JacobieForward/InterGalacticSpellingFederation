using System.Collections.Generic;
using System;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController instance;
    Dictionary<int, Player> players = new Dictionary<int, Player>();
    List<Torpedo> torpedoes = new List<Torpedo>();
    public TorpedoWordList torpedoWordList = new TorpedoWordList();
    
    private void Awake() {
        SetupInstance();
    }

    private void SetupInstance() {
        // Loose Singleton, doesn't require Loader Script only an instance of GameController
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
        Player[] playerList = GameObject.FindObjectsOfType<Player>();
        foreach (Player player in playerList) {
            players.Add(player.playerNumber, player);
        }
        torpedoWordList.initWordLists();
    }

    public Player GetPlayerBasedOnPlayerNumber(int playerNumber) {
        Player result;
        if (players.TryGetValue(playerNumber, out result)) {
            return result;
        }
        return result; // Not nullsafe
    }

    public void AddTorpedoToList(Torpedo newTorpedo) {
        torpedoes.Add(newTorpedo);
    }

    public void RemoveTorpedoFromList(Torpedo torpedoToRemove) {
        torpedoes.Remove(torpedoToRemove);
    }

    // TODO: Get rid of this, it is temporary
    public List<Torpedo> GetTorpedoList() {
        return torpedoes;
    }

    public int DetermineWhichPlayerSelected(Torpedo torpedo) {
        Player result;
        for (int x = 0; x < players.Count; x++) {
            players.TryGetValue(x, out result);
            Torpedo resultTorpedo = result.GetSelectedTorpedo();
            if (resultTorpedo != null) {
                if (resultTorpedo.Equals(torpedo)) {
                    return result.playerNumber;
                }
            }
        }
        return -1;
    }

    // Used by the torpedo spawner to determine if it should spawn a torpedo
    // Returns -1 if no torpedoes should be spawned
    // Returns Constants.LEFT (0) if it should be spawned going left
    // Returns Constants.RIGHT (1) if it should be spawned going right
    public int DetermineIfTorpedoShouldBeSpawned() {
        List<Torpedo> torpedoList = GetTorpedoList();
        bool noneGoingRight = true;
        bool noneGoingLeft = true;
        foreach (Torpedo torp in torpedoList) {
            if (torp.GetComponent<TorpedoMovement>().DetermineGoingRightOrLeft() == Constants.RIGHT) {
                noneGoingRight = false;
            }
            if (torp.GetComponent<TorpedoMovement>().DetermineGoingRightOrLeft() == Constants.LEFT) {
                noneGoingLeft = false;
            }
        }
        if (noneGoingRight) {
            return Constants.RIGHT;
        }
        if (noneGoingLeft) {
            return Constants.LEFT;
        }
        return -1;
    }
}
