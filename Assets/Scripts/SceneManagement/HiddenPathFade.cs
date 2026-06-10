using UnityEngine;

public class HiddenPathFade : MonoBehaviour
{
    public GameObject FadeObject;
    [SerializeField] Animator transitionAnim;
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player")) transitionAnim.SetTrigger("Start");
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player")) transitionAnim.SetTrigger("End");
    }
    
}
