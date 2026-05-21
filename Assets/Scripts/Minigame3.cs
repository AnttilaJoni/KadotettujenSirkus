using UnityEngine;

public class Minigame3 : MonoBehaviour
{
    [SerializeField] private GameObject shapes;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject launcher;

    private Transform _spawnedShape;

    public bool _launcherRotation = false;
    void Start()
    {
        SpawnShape();
    }

    
    void Update()
    {
        RotateLauncher();

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            //_spawnedShape.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            _spawnedShape.transform.parent = null;
            _spawnedShape.GetComponent<Rigidbody2D>().gravityScale = 1;
            _spawnedShape.GetComponent<Rigidbody2D>().AddForce(-launcher.transform.up * 5, ForceMode2D.Impulse);
            //_spawnedShape.GetComponent<ShapeScript>().active = true;
            Invoke(nameof(SpawnShape), 1f);
        }

        if (Input.GetKeyDown(KeyCode.E)) 
        {
            _spawnedShape.transform.Rotate(new Vector3(0, 0, -90f));
        }
            
    }

    void RotateLauncher()
    {
        if (launcher.transform.rotation.z > -0.9f && !_launcherRotation) 
        {
            launcher.transform.Rotate(new Vector3(0, 0, -1));
            
            if (launcher.transform.rotation.z <= -0.9f) 
            {
                _launcherRotation = true;
            }
        }
        
        else if (launcher.transform.rotation.z < 0.9f && _launcherRotation) 
        {
            launcher.transform.Rotate(new Vector3(0, 0, 1));
            if (launcher.transform.rotation.z > 0.9f) 
            {
                _launcherRotation = false;
            }
            
        }
        
    }

    void SpawnShape()
    {
        int randomInt = Random.Range(0, shapes.transform.childCount);
        
        _spawnedShape = Instantiate(shapes.transform.GetChild(randomInt), spawnPoint.transform.position, spawnPoint.transform.rotation, spawnPoint.transform);
        
        //_spawnedShape.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        _spawnedShape.GetComponent<Rigidbody2D>().gravityScale = 0;
        
        _spawnedShape.gameObject.SetActive(true);
    }
}
