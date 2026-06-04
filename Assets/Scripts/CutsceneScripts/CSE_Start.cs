using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSE_Start : CutsceneElementBase
{
    [SerializeField] Canvas canvas;
    public AnimationCurve animationCurve;
    public float fadingSpeed = 1f;

    public enum Direction { FadeIn, FadeOut};

    public override void Execute()
    {
        if (canvas == null) canvas = GetComponent<Canvas>();
        CanvasGroup canvasGroup = canvas.GetComponent<CanvasGroup>();
        if (canvasGroup == null) Debug.LogError("Please assign a canvas group to the canvas!");

        if (animationCurve.length == 0)
        {
            Debug.Log("Animation curve not assigned: Create a default animation curve");
            animationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        }

        StartCoroutine(FadeCanvas(canvasGroup, Direction.FadeIn, fadingSpeed));
        //StartCoroutine(WaitAndAdvance());
        Debug.Log("Executing " + name);
    }

    private IEnumerator FadeCanvas(CanvasGroup canvasGroup, Direction direction, float duration)
    {
        var startTime = Time.time;
        var endTime = Time.time + duration;
        var elapsedTime = 0f;

        // set the canvas to the start alpha – this ensures that the canvas is ‘reset’ if you fade it multiple times
        if (direction == Direction.FadeIn) canvasGroup.alpha = animationCurve.Evaluate(0f);
        else canvasGroup.alpha = animationCurve.Evaluate(1f);

        // loop repeatedly until the previously calculated end time
        while (Time.time <= endTime)
        {
            elapsedTime = Time.time - startTime; // update the elapsed time
            var percentage = 1 / (duration / elapsedTime); // calculate how far along the timeline we are
            if ((direction == Direction.FadeOut)) // if we are fading out
            {
                canvasGroup.alpha = animationCurve.Evaluate(1f - percentage);
            }
            else // if we are fading in/up
            {
                canvasGroup.alpha = animationCurve.Evaluate(percentage);
            }

            yield return new WaitForEndOfFrame(); // wait for the next frame before continuing the loop
        }

        // force the alpha to the end alpha before finishing – this is here to mitigate any rounding errors, e.g. leaving the alpha at 0.01 instead of 0
        if (direction == Direction.FadeIn) canvasGroup.alpha = animationCurve.Evaluate(1f);
        else canvasGroup.alpha = animationCurve.Evaluate(0f);

        StartCoroutine(WaitAndAdvance());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    
}
