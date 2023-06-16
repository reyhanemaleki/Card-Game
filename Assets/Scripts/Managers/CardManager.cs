using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardManager : MonoBehaviour
{


    public static CardManager instance;
    public List<CardController.Card> cards = new List<CardController.Card>();
    public Transform player1Hand, player2Hand;
    public CardController CardControllerPrefab;


    private void Awake()
    {
        instance=this;
    }
    private void Start()
    {

    GenerateCards();

    }
    public void GenerateCards()
    {
        foreach(CardController.Card card in cards)
        {
        CardController newCard = Instantiate(CardControllerPrefab, player1Hand);
        newCard.transform.localPosition=Vector3.zero;
        newCard.Initialize(card);


        
    }

}
}
