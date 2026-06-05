using System;
using UnityEngine;

public class ShapeScript : MonoBehaviour
{
    [SerializeField] private Transform startBlock;
    private Rigidbody2D _rb2d;
    private bool _hasCollided = false;
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (_rb2d.mass < 10f) {
            _rb2d.mass = _rb2d.mass + Time.deltaTime;
        }
        */
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Play collision audio
        if (other.gameObject.CompareTag("Shape")) 
        {
            // Play bullet spawn audio
            //AudioManager.Instance.PlaySFX("SFX_Palikka hit");    
        }

        if (!_hasCollided) {
            
            var pos = (transform.position.y + startBlock.position.y) / 2;

            //Debug.Log(Mathf.Abs(pos) / 2);
            
            _rb2d.mass = Mathf.Abs(pos) / 2;
            _hasCollided = true;
        }

    }
}
