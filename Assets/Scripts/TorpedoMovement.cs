using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoMovement : MonoBehaviour {
    private float movementSpeed = 1.0f;
    private Animation animations;

    private void Start() {
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

    private int DetermineGoingRightOrLeft() {
        if (gameObject.transform.position.y == 1) {
            return Constants.RIGHT;
        }
        if (gameObject.transform.position.y == -1) {
            return Constants.LEFT;
        }
        return -1; // Will return -1 if torpedo is in animation transition
    }


}
