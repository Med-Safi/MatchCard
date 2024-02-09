using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour
{
    Card cardChoosed;

    public GameObject gameScript;
    private GameScript pairsInGameOb;
    public int pairsInGame;

    int cardFlipped = 0;

    bool sameCard = false;
    bool comparing = false;
    bool gameOver = false;
    public Camera mainCamera;
    public List<Card> cardToCompare = new List<Card>();
    // Start is called before the first frame update
    void Start()
    {
        pairsInGameOb = gameScript.GetComponent<GameScript>();
        pairsInGame = pairsInGameOb.cardNeeded;
    }

    // Update is called once per frame
    void Update()
    {
        ChooseCard();
        GameOver();
    }

    void ChooseCard()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Input.GetMouseButtonDown(0) && (!comparing))
        {
            if (Physics.Raycast(ray, out hitInfo))
            {
                cardChoosed = hitInfo.transform.GetComponent<Card>();
                AddCardToCompare();
                StartCoroutine(RotateTwoCards());
                StartCoroutine(CompareCards());
            }
        }
    }
    IEnumerator RotateTwoCards()
    {

        if ((cardFlipped < 2) && (!comparing) && (!gameOver))
        {
            //yield return new WaitForSeconds(1f);
            Debug.Log("miaou");

            cardFlipped++;
            StartCoroutine(cardChoosed.RotateCardUp());
            yield return new WaitForSeconds(20f);
            sameCard = true;
        }
    }
    void AddCardToCompare()
    {
        if (cardToCompare.Count < 2)
        {
            cardToCompare.Add(cardChoosed);
        }
    }
    IEnumerator CompareCards()
    {
        if (cardToCompare.Count == 2)
        {
            var card1 = cardToCompare[0];
            var card2 = cardToCompare[1];

            var meshCard1 = card1.GetComponent<MeshFilter>().sharedMesh;
            var meshCard2 = card2.GetComponent<MeshFilter>().sharedMesh;

            var card1Position = card1.transform.position;
            var card2Position = card2.transform.position;

            comparing = true;

            if (card1Position != card2Position)
            {
                yield return new WaitForSeconds(1f);
                if (meshCard1 == meshCard2)
                {
                    pairsInGame -= 1;
                    Debug.Log("Matched !");
                    Destroy(card1.gameObject);
                    Destroy(card2.gameObject);
                    cardToCompare.Clear();
                    cardFlipped = 0;
                    sameCard = false;
                    comparing = false;
                    yield return null;
                }
                if (meshCard1 != meshCard2)
                {
                    StopCoroutine(card1.RotateCardUp());
                    StopCoroutine(card2.RotateCardUp());
                    Debug.Log("wrong!");
                    StartCoroutine(card1.RotateCardDown());
                    StartCoroutine(card2.RotateCardDown());

                    yield return null;
                    cardToCompare.Clear();
                    sameCard = false;
                    cardFlipped = 0;
                    comparing = false;
                }
            }
            else
            {
                //StopCoroutine(card1.RotateCardUp());
                Debug.Log("Same card!");
                sameCard = true;
                cardFlipped -= 1;
                cardToCompare.RemoveAt(1);
                comparing = false;
                yield return new WaitForSeconds(1f);

            }
        }
    }
    void GameOver()
    {
        if (pairsInGame == 0)
        {
            StopAllCoroutines();
            gameOver = true;
            Debug.Log("GameOver!");
            pairsInGameOb.LoadNextLevel();
        }
    }


}
