using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject bossHandCollider;
    [SerializeField] private BossHealthScript bossHealthScript;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletMoveSpeed;
    [SerializeField] private int burstCount;
    [SerializeField] private float timebetweenBursts;
    [SerializeField] private int projectilesPerBurst;
    [SerializeField] [Range(0, 359)] private float angleSpread;
    [SerializeField] private float startingDistance = 0.1f;
    [SerializeField] private float restTime = 1f;
    [SerializeField] private bool stagger;
    [SerializeField] private bool oscillate;
    [SerializeField] private CountDownScript countDownScript;
    

    private bool _isShooting = false;
    
    

    //private float startAngle;
    //float currentAngle;
    //private float angleStep;
    //private float endAngle;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (countDownScript.gameActive) {

            Attack();
        }
    }

    private void Attack()
    {
        if (!_isShooting) 
        {
            if (bossHealthScript.bossPhase == 1) 
            {
                StartCoroutine(Phase1());
            }
            
            else if (bossHealthScript.bossPhase == 2) 
            {
                StartCoroutine(Phase2());
            }
            
            else if (bossHealthScript.bossPhase == 3) 
            {
                StartCoroutine(Phase3());
            }
            
        }
        
    }
    
    private IEnumerator Phase1()
    {
        _isShooting = true;
        stagger = true;
        oscillate = true;
        burstCount = 3;
        projectilesPerBurst = 8;
        angleSpread = 100;
        startingDistance = 0.3f;
        timebetweenBursts = 1;
        
        float startAngle, currentAngle, angleStep, endAngle;
        float timeBetweenProjectiles = 0f;
        
        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);

        

        if (stagger) {
            timeBetweenProjectiles = timebetweenBursts / projectilesPerBurst;
        }
        
        
        for (int i = 0; i < burstCount; i++)
        {

            if (!oscillate) {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }

            if (oscillate && i % 2 != 1) {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }
            
            else if (oscillate) {
                currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1;
            }

            for (int j = 0; j < projectilesPerBurst; j++)
            {
                Vector2 pos = FindBulletSpawnPos(currentAngle);


                GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                newBullet.transform.right = newBullet.transform.position - transform.position;

                if (newBullet.TryGetComponent(out Projectile projectile))
                {
                    projectile.UpdateMoveSpeed(bulletMoveSpeed);
                }

                currentAngle += angleStep;

                if (stagger) {
                    yield return new WaitForSeconds(timeBetweenProjectiles);
                }
            }
            currentAngle = startAngle;

            yield return new WaitForSeconds(timebetweenBursts);
            
        }

        yield return new WaitForSeconds(restTime);
        _isShooting = false;
    }

    private IEnumerator Phase2()
    {
        _isShooting = true;
        
        projectilesPerBurst = 3;
        angleSpread = 15;

        float startAngle, currentAngle, angleStep, endAngle;
        float timeBetweenProjectiles = 0f;

        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);

        for (int i = 0; i < burstCount; i++)
        {
            for (int j = 0; j < projectilesPerBurst; j++)
            {
                Vector2 pos = FindBulletSpawnPos(currentAngle);


                GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                newBullet.transform.right = newBullet.transform.position - transform.position;

                if (newBullet.TryGetComponent(out Projectile projectile))
                {
                    projectile.UpdateMoveSpeed(bulletMoveSpeed);
                }

                currentAngle += angleStep;
            }
            currentAngle = startAngle;

            yield return new WaitForSeconds(timebetweenBursts);
            TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
        }

        yield return new WaitForSeconds(restTime);
        _isShooting = false;
    }
    
    private IEnumerator Phase3()
    {
        bossHandCollider.gameObject.SetActive(true);
        _isShooting = true;
        
        projectilesPerBurst = 5;
        angleSpread = 90;

        float startAngle, currentAngle, angleStep, endAngle;
        float timeBetweenProjectiles = 0f;

        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);

        for (int i = 0; i < burstCount; i++)
        {
            for (int j = 0; j < projectilesPerBurst; j++)
            {
                Vector2 pos = FindBulletSpawnPos(currentAngle);


                GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                newBullet.transform.right = newBullet.transform.position - transform.position;

                if (newBullet.TryGetComponent(out Projectile projectile))
                {
                    projectile.UpdateMoveSpeed(bulletMoveSpeed);
                }

                currentAngle += angleStep;
            }
            currentAngle = startAngle;

            yield return new WaitForSeconds(timebetweenBursts);
            TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
        }

        yield return new WaitForSeconds(restTime);
        _isShooting = false;
        
        
    }

    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep, out float endAngle)
    {
        Vector2 targetDirection = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread = 0f;
        angleStep = 0f;
        if (angleSpread != 0)
        {
            angleStep = angleSpread / (projectilesPerBurst - 1);
            halfAngleSpread = angleSpread / 2f;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;

        }
    }

    private Vector2 FindBulletSpawnPos(float currentAngle)
    {
        float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);
        
        Vector2 pos = new Vector2(x, y);

        return pos;
    }
}
