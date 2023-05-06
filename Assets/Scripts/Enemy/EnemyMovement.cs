using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    public enum EnemyState
    {
        WALKING
    }

    [SerializeField] private List<Transform> _pathTransforms;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _runSpeed;

    private Animator _animator;
    private List<Vector3> _path;
    private float _currentSpeed;
    private float _directionSpeed;
    private EnemyState _currentState;
    private Sequence _movementSequence;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _path = new();
        _pathTransforms.ForEach(path => _path.Add(path.position));
        _path.Add(_pathTransforms[0].position);
        _currentSpeed = _movementSpeed;

        HandleBehavior(EnemyState.WALKING);
    }

    private void HandleBehavior(EnemyState newState)
    {
        _currentState = newState;
        _currentSpeed = _movementSpeed;

        switch (_currentState)
        {
            case EnemyState.WALKING:
                _movementSequence = DOTween.Sequence();

                for (int i = 0; i < _path.Count - 1; i++)
                {
                    Vector3 destination = _path[i + 1];
                    _directionSpeed = _path[i].x - destination.x;

                    _movementSequence.Append(transform.DORotate(Vector2.up * (_directionSpeed > 0 ? 180f : 0f), .25f));
                    _movementSequence.Append(DOTween.To(() => 0f, (i) => { _animator.SetTrigger("Walk"); }, 1f, 0f));
                    _movementSequence.Append(transform.DOMoveX(destination.x, CalculatePathDuration(i)));
                    _movementSequence.Append(DOTween.To(() => 0f, (i) => { _animator.SetTrigger("Idle"); }, 1f, 0f));
                    _movementSequence.Append(DOTween.To(() => 0f, (time) => { }, 1f, 1f));
                }

                _movementSequence.SetLoops(-1);
                _movementSequence.Play();

                break;
        }
    }

    private float CalculatePathDuration(int index)
    {
        return Vector3.Distance(_path[index], _path[index + 1]) / _currentSpeed;
    }
}
