using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Thrownable : MonoBehaviour
{
    public event Action OnDefeatEnemy;

    [SerializeField] private ParticleSystem _vfxSoundWave;

    private AudioSource _breakSfx;
    private bool _canBreakLight;
    private bool _canKillEnemy;

    private void Start()
    {
        _breakSfx = GetComponent<AudioSource>();
    }

    public void Throw(float speed, bool canBreakLight, bool canKillEnemy)
    {
        Rigidbody2D throwRB = GetComponent<Rigidbody2D>();
        _canBreakLight = canBreakLight;
        _canKillEnemy = canKillEnemy;

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

            /// Remove ele do prefab para n�o ser destru�do antes da hora
            _vfxSoundWave.transform.parent = null;
            _vfxSoundWave.Play();

            Destroy(gameObject, .5f);
        }

        if (_canBreakLight)
        {
            if (collision.CompareTag("Bulb"))
            {
                collision.transform.parent.GetComponentsInChildren<Light2D>().ToList().ForEach((light) => light.gameObject.SetActive(false));
                _breakSfx.Play();
                Destroy(gameObject, .5f);
            }
        }

        if (_canKillEnemy)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy)
            {
                if (enemy.IsProtected) return;

                enemy.Kill(WeaponType.RANGED);
                OnDefeatEnemy?.Invoke();
                _breakSfx.Play();
                Destroy(gameObject);
            }
        }
    }
}
