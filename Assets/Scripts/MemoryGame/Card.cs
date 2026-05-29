using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    private bool flipped = true;
    [SerializeField] private Image iconImage;

    public Sprite hiddenIconSprite;
    public Sprite iconSprite;

    public bool isSelected;

    public CardsController controller;
    void Start()
    {
        StartCoroutine(StartSetup());
    }
    /* void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Flip();
    } */
    
    IEnumerator StartSetup()
    {
        yield return new WaitForSeconds(2f);
        Flip();
        StartCoroutine(StartDelayedFlip());
    }
    IEnumerator StartDelayedFlip()
    {
        yield return new WaitForSeconds(2f);
        Flip();
    }
    public void Flip()
    {
        flipped = !flipped;
        transform.DORotate(new(0, flipped ? 0f : 180f, 0), 0.25f);
    }
    public void Reset()
    {
        StartCoroutine(StartSetup());
    }

    public void OnCardClick()
    {
        controller.SetSelected(this);
    }
    public void SetIconSprite(Sprite sp)
    {
        iconSprite = sp;
        iconImage.sprite = iconSprite;
        
    }
    public void Show()
    {
        Flip();
        isSelected = true;
        
        
    }
    public void Hide()
    {
        Flip();
        isSelected = false;
        
        
    }
}