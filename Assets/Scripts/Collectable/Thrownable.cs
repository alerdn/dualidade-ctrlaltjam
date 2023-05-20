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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            gameObject.tag = "Thrownable";
            _breakSfx.Play();
            Destroy(gameObject);
        }
    }
}
