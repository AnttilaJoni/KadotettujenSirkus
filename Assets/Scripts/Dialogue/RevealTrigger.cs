using UnityEngine;

public class RevealTrigger : MonoBehaviour
{
    public NPC nPC;
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player")) nPC.Interact();
    }
}
