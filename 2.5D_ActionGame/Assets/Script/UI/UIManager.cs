using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI textMesh;
   public void UpdateCoinDisplay(int coins)
   {
        textMesh.text = $"Coins : {coins.ToString()}";
   }
}
