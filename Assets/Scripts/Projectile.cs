using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private float projectileRange = 20f;
    
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
        this.projectileRange  = projectileRange;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) 
        {
            //playerStats.Take
            Destroy(gameObject);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Wall")) 
        {
            Destroy(gameObject);
        }
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * (Time.deltaTime * moveSpeed));
    }
}
