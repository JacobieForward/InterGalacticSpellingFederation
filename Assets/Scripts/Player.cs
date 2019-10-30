using UnityEngine;

public class Player : MonoBehaviour {
    private Torpedo[] torpedoes;
    Torpedo selectedTorpedo;

    private void Start() {
        SelectClosestTorpedoFromList();
    }

    private void Update() {
        DetectInput();
    }

    private void UpdateTorpedoList() {
        torpedoes = GameObject.FindObjectsOfType<Torpedo>();
    }

    private void SelectClosestTorpedoFromList() {
        UpdateTorpedoList();
        Torpedo closestTorpedo = torpedoes[0];
        foreach (Torpedo torpedo in torpedoes) {
            // TODO: Find Torpedo which is closest to your ship
        }
        SelectTorpedo(closestTorpedo);
    }

    private void SelectTorpedo(Torpedo torpedo) {
        selectedTorpedo = torpedo;
        selectedTorpedo.FlipColor();
    }

    private void DetectInput() {
        DetectKeyboard();
        DetectClick();
    }

    private void DetectKeyboard() {
        if (Input.anyKeyDown) {
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
                if (hit.transform.name.Contains("Torpedo")) {
                    selectedTorpedo.FlipColor();
                    SelectTorpedo(hit.transform.gameObject.GetComponent<Torpedo>());
                }
            }
        }
    }
}