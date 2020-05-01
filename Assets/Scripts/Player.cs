using UnityEngine;

public class Player : MonoBehaviour {
    // TODO: Make generic player class to be inherited by both BotPlayer and new HumanPlayer class
    public Torpedo selectedTorpedo;
    public int playerNumber; // TODO: Rename player number to conform with the change making player number equal to the side of the field the player is on in accordance with the LEFT and RIGHT constants
    public int health = 1;

    private void Awake() {
        playerNumber = DetermineOnRightOrLeft();
    }

    private void Start() {
        SelectClosestTorpedoFromList();
    }

    private void Update() {
        DetectInput();
        if (selectedTorpedo == null) {
            SelectClosestTorpedoFromList();
        }
        if (health <= 0) {
            Lose();
        }
    }

    private void Lose() {
        Destroy(gameObject);
    }

    private void DetectInput() {
        DetectKeyboard();
        DetectClick();
    }

    private void DetectKeyboard() {
        if (Input.anyKeyDown && selectedTorpedo != null) {
            string input = Input.inputString;
            if (input.Length > 0) {
                char inputChar = input[0];
                selectedTorpedo.ReceiveLetter(inputChar);
            }
        }
    }

    private void DetectClick() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.name.Contains("Torpedo") && hit.transform.gameObject.GetComponent<Torpedo>().IsSelectableByPlayer(playerNumber)) {
                    SelectTorpedo(hit.transform.gameObject.GetComponent<Torpedo>());
                }
            }
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

    public void TorpedoSolved() {
        selectedTorpedo = null;
        SelectClosestTorpedoFromList();
    }

    public int DetermineOnRightOrLeft() {
        if (transform.position.z < 0 ) {
            return Constants.LEFT;
        } else {
            return Constants.RIGHT;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.Equals(selectedTorpedo)) {
            selectedTorpedo = null;
        }
        health -= 1;
        DestroyTorpedo(other.gameObject);
        SelectClosestTorpedoFromList();
    }

    private void DestroyTorpedo(GameObject torpedoToDestroy) {
        GameObject torpedoParent = torpedoToDestroy.transform.parent.gameObject;
        Destroy(torpedoParent);
        GameController.instance.RemoveTorpedoFromList(torpedoToDestroy.GetComponent<Torpedo>());
    }

    public Torpedo GetSelectedTorpedo() {
        return selectedTorpedo;
    }
}