using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameEndUIController : MonoBehaviour
{
public TextMeshProUGUI winnerName;
public Button restart, quit;
private void Awake()
{
SetupButtons();
}
private void SetupButtons()
{
    
}
public void Initialize(Player winner)
{
    winnerName.text="Player:" + winner.ID+" has won!";
}
}
