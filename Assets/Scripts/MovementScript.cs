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
    
    

    [SerializeField] private Animator anim;
    public string lastDirection = "Up";

    List<Vector3> _attemptedMoveDirs = new List<Vector3>();

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "TestSceneJoni" || SceneManager.GetActiveScene().name == "Teemu") 
        {
            Debug.Log("Load player pos");
            var playerPosition = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().playerPosition;
            
            transform.position = playerPosition;
            //FindFirstObjectByType<CinemachineConfiner2D>().m_BoundingShape2D = GameObject.Find(GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().mapBoundary).GetComponent<PolygonCollider2D>();
        }
        
        
    }
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        //GameObject.FindGameObjectWithTag("SaveController").GetComponent<SaveController>().SaveGame();
        
        //_rb2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canParry) 
        {anim.SetTrigger("Parry");
            Parry();
            _canParry = false;
        }
        //Debug.Log(lastDirection);
        Inputs();
        HandleAnimations();

        if (Input.GetKeyDown(KeyCode.X)) 
        {
            GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().playerPosition = transform.position;
            SceneManager.LoadScene("Teemu2");    
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
        if (_canParry) {
            _rb2d.MovePosition(transform.position + moveDirection.normalized * (moveSpeed / 50));
        }
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
}