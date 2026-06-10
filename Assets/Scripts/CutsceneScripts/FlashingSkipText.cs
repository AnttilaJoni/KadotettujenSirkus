using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingSkipText : MonoBehaviour
{
    public TextMeshProUGUI text_skip;
    public void StartFlashing()
    {
        StartCoroutine(SetText());
        
    }
    IEnumerator SetText()
    {
        yield return new WaitForSeconds(0.6f);
        text_skip.text = $"";
        StartCoroutine(SetText2());
    }
    IEnumerator SetText2()
    {
        yield return new WaitForSeconds(0.6f);
        text_skip.text = $"Press E or Space to skip";
        StartCoroutine(SetText());
    }
}
