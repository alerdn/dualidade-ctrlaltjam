using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrownable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            gameObject.tag = "Thrownable";
            Destroy(gameObject, .5f);
        }
    }
}
