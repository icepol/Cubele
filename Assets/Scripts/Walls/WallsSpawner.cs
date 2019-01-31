using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsSpawner : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float wallHeight = 10f;
    [SerializeField] GameObject[] wallPrefabs;

    List<Transform> wallsOnScreen = new List<Transform>();
    int wallsCreated;

    void Start() {
        foreach (Transform child in transform) {
            wallsOnScreen.Add(child);
        }
    }

    void Update() {
        if (!LevelManager.IsGameRunning)
            return;
            
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

        PlayerStats.Distance += offset;
        PlayerStats.Score = (int)(PlayerStats.Distance * 10) + PlayerStats.Bonus;

        wallsOnScreen = currentWallsOnScreen;

        EventManager.TriggerEvent("WallMove", new Vector3(0, -offset, 0));
    }

    Transform CreateWall(Vector3 wallPosition) {
        // which wall will be the next
        int wallIndex = Random.Range(0, Mathf.Min(wallPrefabs.Length, 1 + wallsCreated / 5));

        // spawn new wall
        GameObject wallObject = Instantiate(
            wallPrefabs[wallIndex],
            new Vector2(wallPosition.x, wallPosition.y + wallHeight),
            Quaternion.identity,
            transform
        );

        wallsCreated++;

        return wallObject.transform;
    }
}
