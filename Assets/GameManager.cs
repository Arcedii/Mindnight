// Copyright (c) 2025 Craciun Dan. All rights reserved.
// Unauthorized use or distribution is prohibited.

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int enemiesAlive;

    public CanvasController canvasController;

    private void Awake()
    {
        instance = this;
        enemiesAlive = FindObjectsOfType<Enemy>().Length;
    }

    public void EnemyDied()
    {
        enemiesAlive--;
        if (enemiesAlive <= 0)
        {
            canvasController.TriggerWin();
        }
    }

}
