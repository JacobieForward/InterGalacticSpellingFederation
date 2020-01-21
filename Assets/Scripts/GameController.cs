using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController instance;
    Dictionary<int, Player> players = new Dictionary<int, Player>();
    List<Torpedo> torpedoes = new List<Torpedo>();

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

    // TODO: Get rid of this, it is temporary
    public List<Torpedo> GetTorpedoList() {
        return torpedoes;
    }
}
