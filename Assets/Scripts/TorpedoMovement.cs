using UnityEngine;

public class TorpedoMovement : MonoBehaviour {
    private float movementSpeed = 1.0f;
    private Animation animations;

    private void Awake() {
        animations = gameObject.transform.parent.GetComponent<Animation>();
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
        } else if (rightOrLeft == Constants.LEFT) {
            transform.parent.position += Vector3.back * Time.deltaTime * movementSpeed;
        }
    }

    public int DetermineGoingRightOrLeft() {
        if (gameObject.transform.position.y == 1) {
            return Constants.RIGHT;
        } else {
            return Constants.LEFT;
        }
    }

    public void ChangeTorpedoDirection() {
        if (DetermineGoingRightOrLeft() == Constants.LEFT) {
            animations.Play("FlipTorpedoToRight");
        } else {
            animations.Play("FlipTorpedoToLeft");
        }
        movementSpeed += Constants.MOVEMENTINCREASEPERSOLVE;
    }
}
