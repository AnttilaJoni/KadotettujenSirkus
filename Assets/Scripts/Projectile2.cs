using UnityEngine;

public class Projectile2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    public int projectileDamage = 1;

    public bool bounceProjectile = false;
    
    void Start()
    {
        
    }

    // Update is called once per frame
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
            
            
            
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().TakeDamage(projectileDamage);
            Destroy(gameObject);
        }
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Parry")) {
            
            
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
