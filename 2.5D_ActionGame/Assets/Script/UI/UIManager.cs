using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI livesText;
    public void UpdateCoinDisplay(int coins)
    {
         coinText.text = $"Coins : {coins.ToString()}";
    }

    public void UpdateLivesDisplay(int lives)
    {
        livesText.text = $"Lives : {lives.ToString()}";
    }
}
