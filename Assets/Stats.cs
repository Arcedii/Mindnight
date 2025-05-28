// Copyright (c) 2025 Craciun Dan. All rights reserved.
// Unauthorized use or distribution is prohibited.


using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public Text winText;
    public Text loseText;

    private int wins;
    private int losses;

    void Start()
    {
        wins = PlayerPrefs.GetInt("WinCount", 0);
        losses = PlayerPrefs.GetInt("LossCount", 0);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (winText != null)
            winText.text = "Wins: " + wins;
        if (loseText != null)
            loseText.text = "Losses: " + losses;
    }
}
