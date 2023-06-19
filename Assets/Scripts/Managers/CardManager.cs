using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class CardManager : MonoBehaviour
{


    public static CardManager instance;
    public List<CardController.Card> cards = new List<CardController.Card>();
    public List<int> player1Deck = new List<int>();
    public Transform player1Hand, player2Hand;
    public CardController CardControllerPrefab;
    public List<CardController> player1Cards = new List<CardController>(), 
    player2Cards = new List<CardController>(), player1HandCards = new List<CardController>(), player2HandCards = new List<CardController>(); 


    private void Awake()
    {
        instance=this;
    }


    private void Start()
    {
    GenerateCards();
    //FillDecks();
    }

    /*private void FillDecks()
    {
        foreach(CardController.Card card in cards)
        {
            player1Deck.Add(new CardController.Card(card));
            player1Deck.Add(new CardController.Card(card));
    
        }
        foreach(CardController.Card card in cards)
        {
            player2Deck.Add(new CardController.Card(card));
            player2Deck.Add(new CardController.Card(card));
        }
    }*/
    public void GenerateCards()
    {
       /* foreach(int cardIndex in player1Deck)
        {
        CardController newCard = Instantiate(CardControllerPrefab, player1Hand);
        newCard.transform.localPosition=Vector3.zero;
        newCard.Initialize(cards[cardIndex], 0);
        }*/
        foreach(CardController.Card card in cards)
        {
        CardController newCard = Instantiate(CardControllerPrefab, player1Hand.root);
        newCard.transform.localPosition=Vector3.zero;
        newCard.Initialize(card, 0, player1Hand);
        player1HandCards.Add(newCard);
        }
        foreach(CardController.Card card in cards)
        {
        CardController newCard = Instantiate(CardControllerPrefab, player2Hand.root);
        newCard.transform.localPosition=Vector3.zero;
        newCard.Initialize(card, 1, player2Hand);
        player2HandCards.Add(newCard);
        }
   }
   public void PlayCard(CardController card, int ID)
   {
    if(ID == 0)
    {
        player1Cards.Add(card);
        player1HandCards.Remove(card);
    }
    else
    {
        player2Cards.Add(card);
        player2HandCards.Remove(card);

    }
   }

   public void ProcessStartTurn(int ID)
   {
    List<CardController> cards = new List<CardController>();
    List<CardController> enemyCards = new List<CardController>();
    if(ID == 0)
    {
        cards.AddRange(player1Cards);
        enemyCards.AddRange(player2Cards);
    }
    else
    {
        cards.AddRange(player2Cards);
        enemyCards.AddRange(player1Cards);

    }
    foreach(CardController card in cards)
    {
        if(card == null) continue;
        if(card.card.health <= 0)
        {
            Destroy(card.gameObject);
        }
    }
    foreach(CardController card in enemyCards)
    {
        if(card.card.health <= 0)
        {
            Destroy(card.gameObject);
        }
    }


        player1Cards.Clear();
        player2Cards.Clear();

    foreach(CardController card in cards)
    {
        if(card != null)
        {
            if(ID == 0)
            {
             player1Cards.Add(card);

            }
            else
            {
            player2Cards.Add(card);
            }
        }
    }
    foreach(CardController card in enemyCards)
    {
        if(card != null)
        {
            if(ID == 1)
            {
             player1Cards.Add(card);

            }
            else
            {
            player2Cards.Add(card);
            }
        }
    }
    bool drawCard = false;
        if(ID==0)
        {
            drawCard = player1HandCards.Count < 7;

        }
        else
        {
            drawCard = player2HandCards.Count < 7;

        }
        if(drawCard)
        {
        int randomCard = UnityEngine.Random.Range(0, this.cards.Count);
        CardController newCard = Instantiate(CardControllerPrefab, player1Hand.root);
        newCard.transform.localPosition=Vector3.zero;
        newCard.Initialize(this.cards[randomCard], ID, ID==0? player1Hand: player2Hand);
        if(ID==0)
        player1HandCards.Add(newCard);
        else
        player2HandCards.Add(newCard);
        }



   }



   public void ProcessEndTurn(int ID)
   {
    List<CardController> cards = new List<CardController>();
    List<CardController> enemyCards = new List<CardController>();

    if(ID == 0)
    {
        cards.AddRange(player1Cards);
        enemyCards.AddRange(player2Cards);
    }
    else
    {
        cards.AddRange(player2Cards);
        enemyCards.AddRange(player1Cards);

    }
    foreach(CardController cardController in cards)
    {
        if(cardController == null) continue;
        if(AreThereCardsWIithHealth(enemyCards))
        {
        int randomEnemyCard = UnityEngine.Random.Range(0, enemyCards.Count);
        while(enemyCards[randomEnemyCard].card.health <=0)
        {
            randomEnemyCard = UnityEngine.Random.Range(0, enemyCards.Count);
        }
        enemyCards[randomEnemyCard].Damage(cardController.card.damage);
        cardController.transform.SetParent(cardController.transform.root);
        cardController.transform.DOMove(enemyCards[randomEnemyCard].transform.position, 0.35f, true).onComplete += ()=> 
        {
            cardController.ReturnToHand();

        };
        cardController.Damage(enemyCards[randomEnemyCard].card.damage);

        }
        else
        {
            int enemyID = ID == 0 ? 1 : 0;
            cardController.transform.SetParent(cardController.transform.root);
        cardController.transform.DOMove(ID==0? player2Hand.transform.position: player1Hand.transform.position, 0.35f, true).onComplete += ()=> 
        {
            cardController.ReturnToHand();

        };
            PlayerManager.instance.DamagePlayer(enemyID,cardController.card.damage);
        }
    }
   }




   private bool AreThereCardsWIithHealth(List<CardController> cards)
   {
    bool cardHasHealth = false;
    foreach(CardController card in cards)
    {
        if(card.card.health > 0)
        {
            cardHasHealth = true;
        }
    }
    return cardHasHealth;

   }

}
