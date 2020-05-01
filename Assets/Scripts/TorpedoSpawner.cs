using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoSpawner : MonoBehaviour {
    [SerializeField] GameObject torpedoObject;
    [SerializeField] float torpedoSpawnCooldownTime = 5.0f;
    [SerializeField] float torpedoRandomlySpawnCooldownTime = 15.0f;
    [SerializeField] int maxNumberOfTorpedoes = 10;

    float spawnTorpedoRandomlyCooldown = 0.0f;
    float spawnTorpedoRightCooldown = 0.0f;
    float spawnTorpedoLeftCooldown = 0.0f;

    private void Start() {
        SpawnTorpedoGoingRight();
        SpawnTorpedoGoingLeft();
    }

    private void Update() {
        spawnTorpedoLeftCooldown += Time.deltaTime;
        spawnTorpedoRightCooldown += Time.deltaTime;
        spawnTorpedoRandomlyCooldown += Time.deltaTime;

        int numberOfTorpedoes = GameController.instance.GetTorpedoList().Count;
        int directionToSpawnTorpedo = GameController.instance.DetermineIfTorpedoShouldBeSpawned();
        if (maxNumberOfTorpedoes > GameController.instance.GetTorpedoList().Count) {
            if (directionToSpawnTorpedo == Constants.LEFT && spawnTorpedoLeftCooldown > torpedoSpawnCooldownTime) {
                SpawnTorpedoGoingLeft();
                spawnTorpedoLeftCooldown = 0.0f;
            }
            if (directionToSpawnTorpedo == Constants.RIGHT && spawnTorpedoRightCooldown > torpedoSpawnCooldownTime) {
                SpawnTorpedoGoingRight();
                spawnTorpedoRightCooldown = 0.0f;
            }
        }
        if (spawnTorpedoRandomlyCooldown > torpedoRandomlySpawnCooldownTime) {
            SpawnTorpedoGoingLeft();
            SpawnTorpedoGoingRight();
            spawnTorpedoRandomlyCooldown = 0.0f;
        }
    }

    private void SpawnTorpedoGoingRight() {
        float xPosition = Random.Range(20, -20);
        GameObject newTorpedo = Instantiate(torpedoObject, new Vector3(xPosition, 0, 0), Quaternion.identity);
        newTorpedo.transform.GetChild(0).position = new Vector3(xPosition, 1, 0);
    }

    private void SpawnTorpedoGoingLeft() {
        float xPosition = Random.Range(20, -20);
        GameObject newTorpedo = Instantiate(torpedoObject, new Vector3(xPosition, 0, 0), Quaternion.identity);
        newTorpedo.transform.GetChild(0).position = new Vector3(xPosition, -1, 0);
        newTorpedo.transform.GetChild(0).eulerAngles = new Vector3(gameObject.transform.eulerAngles.x + 180, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
        newTorpedo.transform.GetChild(0).GetChild(1).eulerAngles = new Vector3(gameObject.transform.eulerAngles.x + 90, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z + 90);
    }
}
