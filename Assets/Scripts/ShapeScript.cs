using UnityEngine;

public class ShapeScript : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_rb2d.mass < 10f) {
            _rb2d.mass = _rb2d.mass + Time.deltaTime;
        }
    }
}
