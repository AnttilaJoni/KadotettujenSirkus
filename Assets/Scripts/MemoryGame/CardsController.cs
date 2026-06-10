using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardsController : MonoBehaviour
{
    [SerializeField] Card cardPrefab;
    [SerializeField] Transform gridTransform;
    [SerializeField] Sprite[] sprites;
    public bool canFlip;

    private List <Sprite> spritePairs;

    Card firstSelected;
    Card secondSelected;

    int matchCounts;
    int wins;
    public TextMeshProUGUI text_health;
    public TextMeshProUGUI text_wins;
    public GameObject text_gameover;
    public GameObject text_win;
    public GameObject notClickable;

    public GameObject cards_placeholder;
    private GameObject[] cards;
    public AudioClip[] cardSounds;
    public bool flipping;
    public bool hasPlayed;
    //private GameObject _selected;

    private void Start()
    {
        canFlip = false;
        PauseController.SetPause(false);
        text_wins.text = $"0/3";
        wins = 0;
        text_gameover.SetActive(false);
        notClickable.SetActive(true);
        cards_placeholder.SetActive(false);
        StartCoroutine(SetHealth());
        PrepareSprites();
        CreateCards();
        AudioManager.PlayCardSFX(cardSounds[1]);
        AudioManager.Instance.PlayMusic("MemoryGame");
    }
    private void Update()
    {
        if(flipping && !hasPlayed)
        {
            AudioManager.PlayCardSFX(cardSounds[2]);
            hasPlayed = true;
            flipping = false;
        }
        if(!PauseController.IsGamePaused)
        {
            if(EventSystem.current.currentSelectedGameObject == null)
            {
                return;
            }
            if(EventSystem.current.currentSelectedGameObject.name == "Button (resume) (1)" && !notClickable.activeSelf)
            {
                EventSystem.current.SetSelectedGameObject(cards[0]);
            }
            /* if(EventSystem.current.currentSelectedGameObject == _selected)
            {
                foreach (GameObject go in cards)
                {
                    if (EventSystem.current.currentSelectedGameObject == go)
                    {
                        transform.DOPunchScale(new Vector3(0.08f, 0.08f, 0.08f), (float) 0.4, 3, 1F);
                        _selected = go;
                    }  
                }
            } */
        }
        
        /* if(EventSystem.current.currentSelectedGameObject == cards[UnityEngine.Range(0, cards.Length)]])
        {
            
        } */
        //EventSystem.current.currentSelectedGameObject
        //transform.DOPunchScale(new Vector3(0.08f, 0.08f, 0.08f), (float) 0.4, 3, 1F);
        
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
        StartCoroutine(ClickableAgain());
        
    }
    IEnumerator ClickableAgain()
    {
        yield return new WaitForSeconds(4f);
        text_gameover.SetActive(false);
        notClickable.SetActive(false);
        cards = GameObject.FindGameObjectsWithTag("Card");
        EventSystem.current.SetSelectedGameObject(cards[0]);
    }
    public void DestroyCards()
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
                //AudioManager.PlayCardSFX();
                AudioManager.PlayCardSFX(cardSounds[2]);
                return;
            }
            if(secondSelected == null)
            {
                secondSelected = card;
                AudioManager.PlayCardSFX(cardSounds[2]);
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
            AudioManager.PlayCardSFX(cardSounds[0]);
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
                    text_win.SetActive(true);
                    DestroyCards();
                    matchCounts = 0;
                    canFlip = true;
                }
                
            }
            a.transform.DOPunchScale(new Vector3(0.08f, 0.08f, 0.08f), (float) 0.4, 3, 1F);
            b.transform.DOPunchScale(new Vector3(0.08f, 0.08f, 0.08f), (float) 0.4, 3, 1F);
        }
        else
        {
            AudioManager.PlayCardSFX(cardSounds[3]);
            a.Hide();
            b.Hide();
            UpdateHealth();
        }
    }
    IEnumerator GameWon()
    {
        yield return new WaitForSeconds(2f);
        GameObject.FindGameObjectWithTag("PlayerStats")
            .GetComponent<DontDestroyOnLoad>().MinigameCompleted(2);
        //SceneController.instance.ChangeSceneByIndex(7);
    }
    public void ExitGame()
    {
        GameObject.FindGameObjectWithTag("PlayerStats")
            .GetComponent<DontDestroyOnLoad>().playerAlive = false;
        Debug.Log("Player dead");
        
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
            StartCoroutine(GameOver());
            
        }
    }

    IEnumerator GameOver()
    {
        EventSystem.current.SetSelectedGameObject(null);
        text_gameover.SetActive(true);
        /* notClickable.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        text_gameover.SetActive(true);
        StartCoroutine(GameRestart()); */
        yield return new WaitForSeconds(2f);
        GameObject.FindGameObjectWithTag("PlayerStats")
            .GetComponent<DontDestroyOnLoad>().playerAlive = false;
        Debug.Log("Player dead");
        
        SceneController.instance.ChangeSceneByIndex(7);
    }
    
    IEnumerator GameRestart()
    {
        yield return new WaitForSeconds(3f);
        text_gameover.SetActive(false);
        text_win.SetActive(false);
        matchCounts = 0;
        wins = 0;
        DestroyCards();
        text_wins.text = $"0/3";
        canFlip = true;
        
    }
    IEnumerator GameReset()
    {
        AudioManager.PlayCardSFX(cardSounds[1]);
        notClickable.SetActive(true);
        yield return new WaitForSeconds(1f);
        PrepareSprites();
        cards_placeholder.SetActive(false);
        text_win.SetActive(false);
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
