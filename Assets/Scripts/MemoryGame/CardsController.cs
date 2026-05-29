using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardsController : MonoBehaviour
{
    [SerializeField] Card cardPrefab;
    [SerializeField] Transform gridTransform;
    [SerializeField] Sprite[] sprites;

    private List <Sprite> spritePairs;

    Card firstSelected;
    Card secondSelected;

    int matchCounts;
    int wins;
    public TextMeshProUGUI text_health;
    public TextMeshProUGUI text_wins;
    public GameObject text_gameover;

    public GameObject cards_placeholder;
    private GameObject[] cards;

    private void Start()
    {
        text_wins.text = $"0/3";
        wins = 0;
        text_gameover.SetActive(false);
        cards_placeholder.SetActive(false);
        StartCoroutine(SetHealth());
        PrepareSprites();
        CreateCards();
    }
    IEnumerator SetHealth()
    {
        yield return new WaitForSeconds(0.2f);
        text_health.text = $"♥";
        yield return new WaitForSeconds(0.1f);
        text_health.text = $"♥♥";
        yield return new WaitForSeconds(0.1f);
        text_health.text = $"♥♥♥";
        yield return new WaitForSeconds(0.1f);
        text_health.text = $"♥♥♥♥";
        yield return new WaitForSeconds(0.1f);
        text_health.text = $"♥♥♥♥♥";
        yield return new WaitForSeconds(0.1f);
        text_health.text = $"♥♥♥♥♥♥";
        yield return new WaitForSeconds(0.1f);
        text_health.text = $"♥♥♥♥♥♥♥";
        yield return new WaitForSeconds(0.1f);
        text_health.text = $"♥♥♥♥♥♥♥♥";
        yield return new WaitForSeconds(0.1f);
        text_health.text = $"♥♥♥♥♥♥♥♥♥";
    }
    private void PrepareSprites()
    {
        spritePairs = new List<Sprite>();
        for(int i = 0; i < sprites.Length; i++)
        {
            spritePairs.Add(sprites[i]);
            spritePairs.Add(sprites[i]);
        }
        ShuffleSprites(spritePairs);
    }
    void CreateCards()
    {
        for(int i = 0; i < spritePairs.Count; i++)
        {
            Card card = Instantiate(cardPrefab, gridTransform);
            card.SetIconSprite(spritePairs[i]);
            card.controller = this;
        }
    }
    void DestroyCards()
    {
        cards_placeholder.SetActive(true);
        StartCoroutine(SetHealth());
        cards = GameObject.FindGameObjectsWithTag("Card");

        foreach (GameObject c in cards)
        {
            Destroy(c);
        }
        StartCoroutine(GameReset());
    }
    public void SetSelected(Card card)
    {
        if(card.isSelected == false)
        {
            card.Show();

            if(firstSelected == null)
            {
                firstSelected = card;
                return;
            }
            if(secondSelected == null)
            {
                secondSelected = card;
                StartCoroutine(CheckMatching(firstSelected, secondSelected));
                firstSelected = null;
                secondSelected = null;
            }
        }
    }
    IEnumerator CheckMatching(Card a, Card b)
    {
        yield return new WaitForSeconds(0.4f);
        Debug.Log("Checking match");
        if(a.iconSprite == b.iconSprite)
        {
            matchCounts++;
            Debug.Log($"Match found, count: {matchCounts}, spritePairs.Count / 2: {spritePairs.Count / 2}");
            if(matchCounts == spritePairs.Count / 2)
            {
                //StartCoroutine(EndFlip());
                wins++;
                if(wins == 3)
                {
                    text_wins.text = $"{wins}/3";
                    StartCoroutine(GameWon());
                }
                else
                {
                    text_wins.text = $"{wins}/3";
                    DestroyCards();
                    matchCounts = 0;
                }
                
            }
            a.transform.DOPunchScale(new Vector3(0.08f, 0.08f, 0.08f), (float) 0.4, 3, 1F);
            b.transform.DOPunchScale(new Vector3(0.08f, 0.08f, 0.08f), (float) 0.4, 3, 1F);
        }
        else
        {
            
            a.Hide();
            b.Hide();
            UpdateHealth();
        }
    }
    IEnumerator GameWon()
    {
        yield return new WaitForSeconds(2f);
        SceneController.instance.ChangeSceneByIndex(7);
    }
    public void ExitGame()
    {
        SceneController.instance.ChangeSceneByIndex(7);
    }
    IEnumerator EndFlip()
    {
        yield return new WaitForSeconds(0.4f);
        //cards = GameObject.FindGameObjectsWithTag("Card");

        /* for(int i = 0; i <= gridTransform.childCount; i++)
        {
            gridTransform.gameObject.transform.GetChild(i).Hide();
        } */

        /* foreach (GameObject c in cards)
        {
            gridTransform.GetChild(i);
            c.Hide();
        } */
        DestroyCards();
    }

    void UpdateHealth()
    {
        if (text_health.text == $"♥♥♥♥♥♥♥♥♥")
        {
            text_health.text = $"♥♥♥♥♥♥♥♥";
            return;
        }
        if (text_health.text == $"♥♥♥♥♥♥♥♥")
        {
            text_health.text = $"♥♥♥♥♥♥♥";
            return;
        }
        if (text_health.text == $"♥♥♥♥♥♥♥")
        {
            text_health.text = $"♥♥♥♥♥♥";
            return;
        }
        if (text_health.text == $"♥♥♥♥♥♥")
        {
            text_health.text = $"♥♥♥♥♥";
            return;
        }
        if (text_health.text == $"♥♥♥♥♥♥")
        {
            text_health.text = $"♥♥♥♥♥";
            return;
        }
        if (text_health.text == $"♥♥♥♥♥")
        {
            text_health.text = $"♥♥♥♥";
            return;
        }
        if (text_health.text == $"♥♥♥♥")
        {
            text_health.text = $"♥♥♥";
            return;
        }
        if (text_health.text == $"♥♥♥")
        {
            text_health.text = $"♥♥";
            return;
        }
        if (text_health.text == $"♥♥")
        {
            text_health.text = $"♥";
            return;
        }
        if (text_health.text == $"♥")
        {
            text_health.text = $"";
            GameOver();
            
        }
    }

    void GameOver()
    {
        text_gameover.SetActive(true);
        StartCoroutine(GameRestart());
    }
    IEnumerator GameRestart()
    {
        yield return new WaitForSeconds(3f);
        matchCounts = 0;
        wins = 0;
        DestroyCards();
        text_wins.text = $"0/3";
        text_gameover.SetActive(false);
        
    }
    IEnumerator GameReset()
    {
        yield return new WaitForSeconds(1f);
        PrepareSprites();
        cards_placeholder.SetActive(false);
        CreateCards();
        
    }
    void ShuffleSprites(List<Sprite> spriteList)
    {
        for (int i = spriteList.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            Sprite temp = spriteList[i];
            spriteList[i] = spriteList[randomIndex];
            spriteList[randomIndex] = temp;
        }
    }
}
