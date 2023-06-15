using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
public List<Player> players = new List<Player>();
private void Awake()
{
    instance=this;
}
internal void AssignTurn(int currentPlayerTurn)
{
    foreach(Player player in players)
    {

player.myTurn=player.ID==currentPlayerTurn;

    }

}

}
