using System.Collections;
using UnityEngine;

public class Minigame2 : MonoBehaviour
{
    [SerializeField] private GameObject shapes;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject launcher;
    [SerializeField] private Transform leftMost;
    [SerializeField] private Transform rightMost;
    [SerializeField] private float moveSpeed;
    public bool minigameActive = true;
    private bool _canSpawnShape = true;

    private Transform _spawnedShape;

    public int playerHealth = 10;

    public bool _moveLauncher = false;
    void Start()
    {
        GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<DontDestroyOnLoad>().ChangePlayerHealth(playerHealth);
        
        SpawnShape();
    }

    
    void Update()
    {
        
        if (minigameActive) {
            MoveLauncher();
        }

        if (Input.GetKeyDown(KeyCode.Space) && minigameActive && _canSpawnShape) 
        {
            //_spawnedShape.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            _spawnedShape.transform.parent = null;
            _spawnedShape.GetComponent<Rigidbody2D>().gravityScale = 1;
            //_spawnedShape.GetComponent<Rigidbody2D>().AddForce(-launcher.transform.up * 5, ForceMode2D.Impulse);
            //_spawnedShape.GetComponent<ShapeScript>().active = true;

            
            StartCoroutine(ShapeTimer());
            _canSpawnShape = false;

        }

        if (Input.GetKeyDown(KeyCode.E)) 
        {
            _spawnedShape.transform.Rotate(new Vector3(0, 0, -90f));
        }
        
    }

    void CalculateSpeed()
    {
        if (launcher.transform.position.x > rightMost.position.x / 4) 
        {
            Debug.Log("right");
            moveSpeed = 7f;
        }
        
        else if (launcher.transform.position.x < leftMost.position.x / 4) 
        {
            Debug.Log("left");
            moveSpeed = 7f;
        }

        else {
            moveSpeed += Time.deltaTime * 10;
        }
        
    }

    void MoveLauncher()
    {
        if (launcher.transform.position.x > leftMost.position.x && !_moveLauncher) 
        {
            launcher.transform.position = new Vector3(launcher.transform.position.x - Time.deltaTime * moveSpeed, launcher.transform.position.y, 0);

            if (launcher.transform.position.x <= leftMost.position.x) {
                _moveLauncher = true;
            }
        }
        
        else if (launcher.transform.position.x < rightMost.position.x && _moveLauncher) 
        {
            launcher.transform.position = new Vector3(launcher.transform.position.x + Time.deltaTime * moveSpeed, launcher.transform.position.y, 0);
            
            if (launcher.transform.position.x >= rightMost.position.x) {
                _moveLauncher = false;
            }
        }
        
    }

    void SpawnShape()
    {
        if (minigameActive) {
            int randomInt = Random.Range(0, shapes.transform.childCount);

            _spawnedShape = Instantiate(shapes.transform.GetChild(randomInt), spawnPoint.transform.position, spawnPoint.transform.rotation,
                spawnPoint.transform);

            //_spawnedShape.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            _spawnedShape.GetComponent<Rigidbody2D>().gravityScale = 0;

            _spawnedShape.gameObject.SetActive(true);
            
        }
    }

    private IEnumerator ShapeTimer()
    {
        
        yield return new WaitForSeconds(1f);
        SpawnShape();
        _canSpawnShape = true;
    }
}
