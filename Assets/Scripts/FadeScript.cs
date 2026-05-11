using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup blackScreen;
    [SerializeField] private Image fadeImage;

    public float fadeInMultiplier = 2f;
    public float fadeOutMultiplier = 2f;

    public bool fadeIn = false;
    public bool fadeOut = false;
    public bool fadeComplete = true;
    
    void Update()
    {
        if (fadeIn && blackScreen.alpha <= 1) 
        {
            blackScreen.alpha += Time.deltaTime * fadeInMultiplier;

            if (blackScreen.alpha >= 1) {
                fadeIn = false;
                Invoke(nameof(FadeOut), 1f);
            }
        }

        if (fadeOut && blackScreen.alpha >= 0) {
            blackScreen.alpha -= Time.deltaTime * fadeOutMultiplier;

            if (blackScreen.alpha <= 0) {
                fadeOut = false;
                fadeImage.gameObject.SetActive(false);
                fadeComplete = true;
            }
        }
    }

    private void FadeOut()
    {
        fadeOut = true;
    }

    public void StartFade()
    {
        fadeComplete = false;
        fadeImage.gameObject.SetActive(true);
        fadeIn = true;
    }
}
