using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    [SerializeField] private SOInt _soInt;
    [SerializeField] private List<Sprite> _sprites;

    private SpriteRenderer _renderer;
    private bool _canCollect;

    private InteractableObject _interaction;

    private void Start()
    {
        _interaction = GetComponent<InteractableObject>();
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = _sprites[Random.Range(0, _sprites.Count)];
    }

    private void Update()
    {
        if (!_interaction.CanInteract) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_canCollect) Collect();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            _canCollect = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            _canCollect = false;
        }
    }

    private void Collect()
    {
        _soInt.Value++;
        Destroy(gameObject);
    }
}
