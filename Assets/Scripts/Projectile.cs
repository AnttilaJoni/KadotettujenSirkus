using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    //[SerializeField] private float projectileRange = 20f;
    public int projectileDamage = 1;
    public bool bounceProjectile = false;
    
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
            Vector2 dir = other.transform.position - transform.position;
            
            
            
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().TakeDamage(projectileDamage);
            Destroy(gameObject);
        }
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Parry")) 
        {
            Debug.Log("Parry");
            bounceProjectile = true;
            //Destroy(gameObject);
        }
        
        

        if (other.gameObject.layer == LayerMask.NameToLayer("Wall")) 
        {
            
        }
        
        
    }

    private void MoveProjectile()
    {
        if (!bounceProjectile) {
            transform.Translate(Vector3.right * (Time.deltaTime * moveSpeed));
        }

        else {
            transform.Translate(-Vector3.right * (Time.deltaTime * moveSpeed));
        }
    }

    
}
