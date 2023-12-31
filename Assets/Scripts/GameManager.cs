using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool gameStarted;
    // Start is called before the first frame update
    void Start()
    {
        gameStarted = false;
    }

    public void StartGame()
    {
        gameStarted = true;
    }

    public bool HasGameStarted()
    {
        return gameStarted;
    }

    private void Update()
    {
        // Start game if game has not started and a keyboard key was pressed
        if (HasGameStarted() || !Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            return;
        }
        StartGame();
    }
}
