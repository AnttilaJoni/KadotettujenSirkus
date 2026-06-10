using UnityEngine;

public class Projectile2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    public int projectileDamage = 1;

    public bool bounceProjectile = false;
    
    public AudioClip[] playerSounds;
    public AudioClip[] playerParrySound;
    void Start()
    {
        
    }
    
    void Update()
    {
        if (!bounceProjectile) {
            transform.Translate(Vector3.right * (Time.deltaTime * moveSpeed), Space.Self);
        }

        else {
            
            // left
            if (transform.position.x < GameObject.FindGameObjectWithTag("Player").transform.position.x - transform.position.x) {
                transform.Translate(-Vector3.right * (Time.deltaTime * moveSpeed), Space.World);
            }
            
            
            // right
            if (transform.position.x > GameObject.FindGameObjectWithTag("Player").transform.position.x - transform.position.x) {
                transform.Translate(Vector3.right * (Time.deltaTime * moveSpeed), Space.World);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) 
        {
            Vector2 dir = other.transform.position - transform.position;
            
            // Play bullet hit audio
            
            int randomInt = UnityEngine.Random.Range(0, 3);
            {
                if (randomInt == 0) {
                    AudioManager.Instance.PlayerSFX(playerSounds[randomInt]);
                }
                
                else if (randomInt == 1)
                {
                    AudioManager.Instance.PlayerSFX(playerSounds[randomInt]);
                }
                
                else if (randomInt == 2) 
                {
                    AudioManager.Instance.PlayerSFX(playerSounds[randomInt]);
                }
            }
            
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().TakeDamage(projectileDamage);
            Destroy(gameObject);
        }
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Parry")) {
            
            // Play parry audio
            AudioManager.Instance.PlayerSFX(playerParrySound[0]);
            
            var speed = GetComponentInParent<Projectile>().moveSpeed;
            //transform.parent = null;
            moveSpeed = speed * 2;
            Debug.Log("Parry");
            bounceProjectile = true;
            //Destroy(gameObject);
        }
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall")) 
        {
            Destroy(gameObject);
        }
    }
}
