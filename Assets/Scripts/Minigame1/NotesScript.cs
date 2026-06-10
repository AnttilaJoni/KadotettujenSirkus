using UnityEngine;

public class NotesScript : MonoBehaviour
{
    [SerializeField] private Transform goal;
    [SerializeField] private Minigame01 minigameScript;
    
    public AudioClip [] missSounds;
    
    void Update()
    {
        if (transform.position.y > goal.position.y - 2f) 
        {
            transform.position -= new Vector3(0 , Time.deltaTime * minigameScript.speedMultiplier, 0f);

            if (transform.position.y < goal.position.y - 0.76f) 
            {
                AudioManager.Instance.DDRMissSFX(missSounds[0]);
                minigameScript.notesList.Remove(this.gameObject);
                Debug.Log("Note " + gameObject.name + " missed");
                GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().TakeDamage(minigameScript.damage);
                Destroy(this.gameObject);
                
                
            }
        }
    }
}
