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

    public AudioClip[] playerSounds;
    public AudioClip[] playerParrySound;
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
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Parry")) 
        {
            // Play parry audio
            AudioManager.Instance.PlayerSFX(playerParrySound[0]);
            
            Debug.Log("Parry");
            bounceProjectile = true;
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
