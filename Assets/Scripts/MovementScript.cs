using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementScript : MonoBehaviour
{
    private Rigidbody2D _rb2d;

    public float moveSpeed = 5f;
    public Vector3 moveDirection;
    private Vector2 input;
    private bool _canParry = true;
    private float _parryTimer = 1f;
    public float parryTime;
    private bool alreadyPlaying;
    
    

    [SerializeField] private Animator anim;
    public string lastDirection = "Up";

    List<Vector3> _attemptedMoveDirs = new List<Vector3>();
    
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        alreadyPlaying = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canParry && !PauseController.IsGamePaused && SceneManager.GetActiveScene().name == "Teemu3") 
        {
            anim.SetTrigger("Parry");
            Parry();
            _canParry = false;
        }
        
        Inputs();
        HandleAnimations();
        
        
        if (Input.GetKeyDown(KeyCode.X)) 
        {
            //GameObject.FindGameObjectWithTag("SaveController").GetComponent<SaveController>().SaveGame();
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().SavePlayerPosition();
            
            //GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().playerPosition = transform.position;
            SceneManager.LoadScene("Teemu2");    
        }
        
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            GameObject.FindGameObjectWithTag("SaveController").GetComponent<SaveController>().SaveGame();
        }
        
    }
    private void HandleAnimations()
    {
        if (anim == null) return;

        string animationName = "";

        if (moveDirection == Vector3.zero)
            animationName = "Idle";
        else
            animationName = "Walk";

        if (_canParry) {
            anim.Play(animationName + lastDirection);
        }
    }

    void Inputs()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        Vector3 finalDirection = Vector2.zero;
        if(PauseController.IsGamePaused)
        {
            alreadyPlaying = false;
            StopAllCoroutines();
            //Debug.Log("Is paused");
            moveDirection = Vector3.zero;
        }
        else
        {
            if (vertical > 0.01f)
            {
                if (ShouldMoveInDirection(Vector3.up))
                {
                    finalDirection = new Vector2(0, 1);
                    lastDirection = "Down";
                }
                if (!_attemptedMoveDirs.Contains(Vector3.up))
                    _attemptedMoveDirs.Add(Vector3.up);
            }
            else
                _attemptedMoveDirs.Remove(Vector3.up);
            
            if (vertical < -0.01f)
            {
                if (ShouldMoveInDirection(Vector3.down))
                {
                    finalDirection = new Vector2(0, -1);
                    lastDirection = "Up";
                }
                if (!_attemptedMoveDirs.Contains(Vector3.down))
                    _attemptedMoveDirs.Add(Vector3.down);
            }
            else
                _attemptedMoveDirs.Remove(Vector3.down);

            if (horizontal > 0.01f)
            {
                if (ShouldMoveInDirection(Vector3.right))
                {
                    finalDirection = new Vector2(1, 0);
                    lastDirection = "Right";
                }
                if (!_attemptedMoveDirs.Contains(Vector3.right))
                    _attemptedMoveDirs.Add(Vector3.right);
            }
            else
                _attemptedMoveDirs.Remove(Vector3.right);

            if (horizontal < -0.01f)
            {
                if (ShouldMoveInDirection(Vector3.left))
                {
                    finalDirection = new Vector2(-1, 0);
                    lastDirection = "Left";
                }
                if (!_attemptedMoveDirs.Contains(Vector3.left))
                    _attemptedMoveDirs.Add(Vector3.left);
            }
            else
                _attemptedMoveDirs.Remove(Vector3.left);

            //else
                //finalDirection = Vector2.zero;
                
            moveDirection = finalDirection;
        }
    }

    bool ShouldMoveInDirection (Vector3 dir)
    {
        return !_attemptedMoveDirs.Contains(-dir) && (!_attemptedMoveDirs.Contains(dir) || _attemptedMoveDirs[_attemptedMoveDirs.Count - 1] == dir);
    }
    private void FixedUpdate()
    {
        if (_canParry && GameObject.FindGameObjectWithTag("Fade").GetComponent<FadeScript>().fadeComplete) {
            _rb2d.MovePosition(transform.position + moveDirection.normalized * (moveSpeed / 50));
            if(moveDirection != Vector3.zero && !alreadyPlaying)
            {
                alreadyPlaying = true;
                StartCoroutine(WalkSFXCoroutine());
            }
        }
    }
     private IEnumerator WalkSFXCoroutine()
    {
        AudioManager.Instance.PlaySFX("WalkOutside");
        yield return new WaitForSeconds(0.3f);
        alreadyPlaying = false;
    }

    void Parry()
    {
        anim.SetTrigger("Parry");
        StartCoroutine(ParryCoroutine());
    }

    private IEnumerator ParryCoroutine()
    {
        
        yield return new WaitForSeconds(_parryTimer);
        _canParry = true;
    }

    public void SetPlayerPosition(Vector3 pos)
    {
        transform.position = pos;
    }
}