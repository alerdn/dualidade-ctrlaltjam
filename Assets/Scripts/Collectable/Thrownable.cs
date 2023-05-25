using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrownable : MonoBehaviour
{
    private AudioSource _breakSfx;

    private void Start()
    {
        _breakSfx = GetComponent<AudioSource>();
    }

    public void Throw(float speed)
    {
        Rigidbody2D throwRB = GetComponent<Rigidbody2D>();

        Vector2 delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float _angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
        if (_angle < 0) _angle += 360;

        Vector3 direction = RadianToVector2(_angle * Mathf.Deg2Rad);

        transform.parent = null;
        throwRB.gravityScale = 1;
        throwRB.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    private static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            gameObject.tag = "Thrownable";
            _breakSfx.Play();
            Destroy(gameObject, .5f);
        }
    }
}
