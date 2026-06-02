using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform bossHandPositionLeft;
    [SerializeField] private Transform bossHandPositionRight;
    
    [SerializeField] private GameObject bossHandCollider;
    [SerializeField] private BossHealthScript bossHealthScript;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletPrefab2;
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
    
    [SerializeField] private bool right = false;
    [SerializeField] private bool left = false;

    public bool lastPhase = false;
    
    private bool phase2_1 = true;
    private bool phase2_2 = false;

    //private float startAngle;
    //float currentAngle;
    //private float angleStep;
    //private float endAngle;

    private void Start()
    {
        PauseController.SetPause(false);
    }

    private void Update()
    {
        /*
        if (countDownScript.gameActive) {

            Attack();
        }
        */
        
        Attack();
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
    
    
    // Phase 1
    private IEnumerator Phase1()
    {
        _isShooting = true;
        
        projectilesPerBurst = 4;
        angleSpread = 30;
        
        

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
    
    // Phase 2
    private IEnumerator Phase2()
    {

        if (phase2_1) {

            _isShooting = true;
            stagger = true;
            oscillate = true;
            burstCount = 3;
            projectilesPerBurst = 8;
            angleSpread = 100;
            startingDistance = 0.3f;
            timebetweenBursts = 1;
            restTime = 0.5f;

            float startAngle, currentAngle, angleStep, endAngle;
            float timeBetweenProjectiles = 0f;

            TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);



            if (stagger) {
                timeBetweenProjectiles = timebetweenBursts / projectilesPerBurst;
            }


            for (int i = 0; i < burstCount; i++) {

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

                for (int j = 0; j < projectilesPerBurst; j++) {
                    Vector2 pos = FindBulletSpawnPos(currentAngle);


                    GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                    newBullet.transform.right = newBullet.transform.position - transform.position;

                    if (newBullet.TryGetComponent(out Projectile projectile)) {
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

        if (phase2_1) 
        {
            restTime = 0.5f;
            _isShooting = true;
            burstCount = 3;
            projectilesPerBurst = 3;
            angleSpread = 100;
            startingDistance = 0.3f;
            timebetweenBursts = 1f;

            float startAngle, currentAngle, angleStep, endAngle;
            float timeBetweenProjectiles = 0f;

            TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);



            if (stagger) {
                timeBetweenProjectiles = timebetweenBursts / projectilesPerBurst;
            }


            for (int i = 0; i < burstCount; i++) {

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

                for (int j = 0; j < projectilesPerBurst; j++) {
                    Vector2 pos = FindBulletSpawnPos(currentAngle);


                    GameObject newBullet = Instantiate(bulletPrefab2, pos, Quaternion.identity);
                    newBullet.transform.right = newBullet.transform.position - transform.position;

                    if (newBullet.TryGetComponent(out Projectile projectile)) {
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

    }
    
    
    
    // Phase 3
    private IEnumerator Phase3()
    {
        if (!lastPhase) {
            gameObject.SetActive(false);
        }
        
        if (lastPhase) {


            if (right) {
                transform.position = bossHandPositionRight.position;
                StartCoroutine(TimerRight(5f));
            }

            else if (left) {
                transform.position = bossHandPositionLeft.position;
                StartCoroutine(TimerLeft(5f));
            }



            _isShooting = true;
        
            projectilesPerBurst = 12;
            angleSpread = 359;
        
        

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

    private IEnumerator TimerRight(float time)
    {
        
        yield return new WaitForSeconds(time);
        right = false;
        left = true;
    }
    
    private IEnumerator TimerLeft(float time)
    {
        
        yield return new WaitForSeconds(time);
        left = false;
        right = true;
        
    }

    private IEnumerator Phase2_1Timer()
    {
        yield return new WaitForSeconds(3f);
        phase2_1 = false;
        phase2_2 = true;
    }
    
    private IEnumerator Phase2_2Timer()
    {
        yield return new WaitForSeconds(3f);
        phase2_2 = false;
        phase2_1 = true;
    }
}


