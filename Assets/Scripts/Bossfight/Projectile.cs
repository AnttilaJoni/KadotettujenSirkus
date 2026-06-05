using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed = 22f;
    //[SerializeField] private float projectileRange = 20f;
    public int projectileDamage = 1;
    public bool bounceProjectile = false;
    public bool canBounce = true;
    
    private Vector3 _startPosition;
    
    [SerializeField] private GameObject playerStats;
    void Start()
    {
        _startPosition = transform.position;
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats");
    }

    
    void Update()
    {
        MoveProjectile();
    }

    public void UpdateMoveSpeed(float newMoveSpeed)
    {
        this.moveSpeed = newMoveSpeed;
    }

    public void UpdateProjectileRange(float newProjectileRange)
    {
        //this.projectileRange  = projectileRange;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) 
        {
            // Play bullet hit audio
            int randomInt = UnityEngine.Random.Range(1, 4);
            {
                if (randomInt == 1) 
                {   
                    //AudioManager.Instance.PlaySFX("SFX_E NDamage 1");
                }
                
                else if (randomInt == 2)
                {
                    //AudioManager.Instance.PlaySFX("SFX_E NDamage 2");
                }
                
                else if (randomInt == 3) 
                {
                    //AudioManager.Instance.PlaySFX("SFX_E NDamage 3");
                }
            }
            
            //Vector2 dir = other.transform.position - transform.position;
            
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().TakeDamage(projectileDamage);
            Destroy(gameObject);
        }
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Parry")) 
        {
            // Play parry audio
            //AudioManager.Instance.PlaySFX("SFX_F Bparry");
            
            Debug.Log("Parry");
            bounceProjectile = true;
            //Destroy(gameObject);
        }
        
        

        if (other.gameObject.layer == LayerMask.NameToLayer("Wall")) 
        {
            //Debug.Log("Wall");
            Destroy(gameObject);
        }
        
        
        
    }

    private void MoveProjectile()
    {
        
        if (!bounceProjectile) {
            transform.Translate(Vector3.right * (Time.deltaTime * moveSpeed));
        }

        else if (canBounce && bounceProjectile) {
            transform.Translate(-Vector3.right * (Time.deltaTime * moveSpeed));
        }
    }

    
}
