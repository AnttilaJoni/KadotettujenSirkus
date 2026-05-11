using System;
using Cinemachine;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D confiner;
    [SerializeField] private GameObject cmCam;
    [SerializeField] private GameObject playerSpawnPoint;
    
    private GameObject _player;
    
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        cmCam = GameObject.Find("PlayerCam");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            var player = other.gameObject;
            Debug.Log("Camera Transition");
            
            GameObject.FindGameObjectWithTag("Fade").GetComponent<FadeScript>().StartFade();
            
            
            Invoke(nameof(Transition), 1f);
        }
    }

    void Transition()
    {
        
        cmCam.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = confiner;
        _player.transform.position = playerSpawnPoint.transform.position;
        
    }
    
}
