using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
public static TurnManager instance;
public int currentPlayerTurn;
private void Awake()
{
    instance=this;
}
private void Start(){
    StartTurnGameplay(0);
}
public void StartTurnGameplay(int playerID)
{
    currentPlayerTurn=playerID;
    StartTurn();
}
public void StartTurn()
{
GameplayUIController.instance.UpdateCurrentPlayerTurn(currentPlayerTurn);
    PlayerManager.instance.AssignTurn(currentPlayerTurn);
    CardManager.instance.ProcessStartTurn(currentPlayerTurn);


}
public void EndTurn()
{
CardManager.instance.ProcessEndTurn(currentPlayerTurn);
StartCoroutine(WaitForAttacks(currentPlayerTurn == 0? CardManager.instance.player1Cards.Count:CardManager.instance.player2Cards.Count));
currentPlayerTurn = currentPlayerTurn == 0 ? 1 : 0;
}

private IEnumerator WaitForAttacks(float cards)
{
    yield return new WaitForSeconds(cards*0.35f);
    StartTurn();

}
}
