using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsSpawner : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float wallHeight = 10f;
    [SerializeField] GameObject[] wallPrefabs;

    List<Transform> wallsOnScreen = new List<Transform>();

    void Start() {
        foreach (Transform child in transform) {
            wallsOnScreen.Add(child);
        }
    }

    void Update() {
        if (!LevelManager.IsGameRunning)
            return;

        int newWallsRequired = 0;
        List<Transform> currentWallsOnScreen = new List<Transform>();

        float offset = moveSpeed * Time.deltaTime;

        // move all walls by same offset
        foreach (Transform wallTransform in wallsOnScreen) {
            wallTransform.position = new Vector2(0, wallTransform.position.y - offset);

            if (wallTransform.position.y <= -5) {
                // if we are off the screen put the next wall right up to the last one
                Transform newWallTransform = CreateWall(wallsOnScreen[0].position);
                currentWallsOnScreen.Insert(0, newWallTransform);

                // remove old wall
                Destroy(wallTransform.gameObject);
            }
            else {
                // still on screen
                currentWallsOnScreen.Add(wallTransform);
            }
        }

        wallsOnScreen = currentWallsOnScreen;
    }

    Transform CreateWall(Vector3 wallPosition) {
        // spawn new wall
        GameObject wallObject = Instantiate(
            wallPrefabs[Random.Range(0, wallPrefabs.Length)],
            new Vector2(wallPosition.x, wallPosition.y + wallHeight),
            Quaternion.identity,
            transform
        );

        return wallObject.transform;
    }
}
