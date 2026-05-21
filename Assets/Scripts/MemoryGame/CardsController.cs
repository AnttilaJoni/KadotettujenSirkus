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
    public TextMeshProUGUI text_health;
    public GameObject text_gameover;

    private void Start()
    {
        text_gameover.SetActive(false);
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
        if(a.iconSprite == b.iconSprite)
        {
            matchCounts++;
            if(matchCounts == spritePairs.Count / 2)
            {
                //gridTransform.transform.DOPunchScale(new Vector3(0.01f, 0.01f, 0.01f), 1, 0, 1F);
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
    void UpdateHealth()
    {
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
