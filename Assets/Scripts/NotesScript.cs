using UnityEngine;

public class NotesScript : MonoBehaviour
{
    [SerializeField] private Transform goal;
    [SerializeField] private Minigame01 minigameScript;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (transform.position.y > goal.position.y - 1f) 
        {
            transform.position -= new Vector3(0 , Time.deltaTime * 2.5f, 0f);

            if (transform.position.y < goal.position.y - 1f) 
            {
                minigameScript.notesList.Remove(this.gameObject);
                Debug.Log("Note " + gameObject.name + " missed");
                Destroy(this.gameObject);
                
            }
        }
    }
}
