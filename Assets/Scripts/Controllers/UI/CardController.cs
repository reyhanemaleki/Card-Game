using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardController : MonoBehaviour
{
    [System.Serializable]
        public class Card
        {
        public string cardName;
        public int health, damage, manaCost, ownerID;
        public Sprite illustration;
    }




    public Card card;
    public Image illustration;
    public TextMeshProUGUI cardName, health, manaCost, damage;
    private void Awake()
    {
    Initialize(card);
    }
    private void Start()
    {
        
    }

public void Initialize(Card card)
{
illustration.sprite=card.illustration;
cardName.text=card.cardName;
manaCost.text=card.manaCost.ToString();
damage.text=card.damage.ToString();
health.text=card.health.ToString();

}

    private void Update()
    {
        
    }



}

