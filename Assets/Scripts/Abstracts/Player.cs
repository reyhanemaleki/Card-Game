
[System.Serializable]
public class Player 
{
public int health;
public int ID;
public bool myTurn;
public Player(int health, int ID)
{
    this.health=health;
    this.ID=ID;
}
}
