using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform bossHandPositionLeft;
    [SerializeField] private Transform bossHandPositionRight;

    [SerializeField] private Animator bossAnimator;
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

    [SerializeField] private float animationDelay = 0.3f;
    private bool _isShooting = false;
    
    [SerializeField] private bool right = false;
    [SerializeField] private bool left = false;

    public bool lastPhase = false;
    
    private bool phase2_1 = true;
    //private bool phase2_2 = false;

    public int playerHealth = 30;

    //private float startAngle;
    //float currentAngle;
    //private float angleStep;
    //private float endAngle;
    
    public bool audioEnabled = false;
    
    public AudioClip [] bossBulletSpawnSounds;
    
    public AudioClip [] bossLaughSounds;
    
    public AudioClip [] bossSlamSounds;

    private void Start()
    {
        PauseController.SetPause(false);
        GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().ChangePlayerHealth(playerHealth);

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
        
        // Play animation
        animationDelay = 0.5f;
        
        //yield return new WaitForSeconds(animationDelay);
        
        // Play boss laugh audio
        //var randomInt  = UnityEngine.Random.Range(0, 3);
        //AudioManager.Instance.BossSFX(bossLaughSounds[randomInt]);
        

        float startAngle, currentAngle, angleStep, endAngle;
        //float timeBetweenProjectiles = 0f;

        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);

        for (int i = 0; i < burstCount; i++)
        {
            
            for (int j = 0; j < projectilesPerBurst; j++)
            {
                if (audioEnabled) {
                    // Play bullet spawn audio
                    AudioManager.Instance.BossSFX(bossBulletSpawnSounds[0]);
                }

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
            
            // Play boss laugh audio
            //var randomInt  = UnityEngine.Random.Range(0, 3);
            //AudioManager.Instance.BossSFX(bossLaughSounds[randomInt]);
            
            // Play animation
            animationDelay = 0.5f;
        
            yield return new WaitForSeconds(animationDelay);



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

                for (int j = 0; j < projectilesPerBurst; j++) 
                {
                    if (audioEnabled) {
                        // Play bullet spawn audio
                        AudioManager.Instance.BossSFX(bossBulletSpawnSounds[0]);
                    }

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
                    
                    if (bossHealthScript.bossPhase == 3) {
                        
                        StopAllCoroutines();
                        _isShooting = false;
                        break;
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
            burstCount = 2;
            projectilesPerBurst = 3;
            angleSpread = 100;
            startingDistance = 0.3f;
            timebetweenBursts = 1f;

            float startAngle, currentAngle, angleStep, endAngle;
            float timeBetweenProjectiles = 0f;

            TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            
            // Play animation
            animationDelay = 0.5f;
        
            yield return new WaitForSeconds(animationDelay);



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

                for (int j = 0; j < projectilesPerBurst; j++) 
                {
                    if (audioEnabled) {
                        // Play bullet spawn audio
                        AudioManager.Instance.BossSFX(bossBulletSpawnSounds[0]);
                    }

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

                    if (bossHealthScript.bossPhase == 3) {
                        
                        StopAllCoroutines();
                        _isShooting = false;
                        break;
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
        // Play boss laugh audio
        var randomInt  = UnityEngine.Random.Range(0, 3);
        AudioManager.Instance.BossSFX(bossLaughSounds[randomInt]);
        
        if (!lastPhase) {
            gameObject.SetActive(false);
        }
        
        if (lastPhase) 
        {
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            if (right) {
                // Play right hand animation
                animationDelay = 0.3f;
                bossAnimator.SetTrigger("RightHand");

                //yield return new WaitForSeconds(animationDelay);
            } 
            
            else if (left) {
                // Play left hand animation
                animationDelay = 0.3f;
                bossAnimator.SetTrigger("LeftHand");

                //yield return new WaitForSeconds(animationDelay);
            }

            if (right) {
                transform.position = bossHandPositionRight.position;
                StartCoroutine(TimerRight(6.5f));
            }

            else if (left) {
                transform.position = bossHandPositionLeft.position;
                StartCoroutine(TimerLeft(6.5f));
            }
            
            
            
            _isShooting = true;
            oscillate = false;
            stagger = false;
            
            bulletMoveSpeed = 5f;
            projectilesPerBurst = 18;
            angleSpread = 359;
            restTime = 2.5f;
            timebetweenBursts = 1.25f;
            burstCount = 3;


            float startAngle, currentAngle, angleStep, endAngle;
            //float timeBetweenProjectiles = 0f;

            TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            
            // Play boss laugh audio
            //var randomInt2  = UnityEngine.Random.Range(0, 3);
            //AudioManager.Instance.BossSFX(bossLaughSounds[randomInt2]);

            for (int i = 0; i < burstCount; i++)
            {
                yield return new WaitForSeconds(0.50f);

                if (audioEnabled) {
                    // Play bullet spawn audio
                    AudioManager.Instance.BossSFX(bossBulletSpawnSounds[0]);
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
        yield return new WaitForSeconds(0.5f);

        if (audioEnabled) {
            // Play ground slam audio
            AudioManager.Instance.BossSFX(bossSlamSounds[0]);
        }

        yield return new WaitForSeconds(time - 0.5f);
        right = false;
        left = true;
    }
    
    private IEnumerator TimerLeft(float time)
    {
        yield return new WaitForSeconds(0.5f);

        if (audioEnabled) {
            // Play ground slam audio
            AudioManager.Instance.BossSFX(bossSlamSounds[0]);
        }

        yield return new WaitForSeconds(time - 0.5f);
        left = false;
        right = true;
        
    }
    
}


