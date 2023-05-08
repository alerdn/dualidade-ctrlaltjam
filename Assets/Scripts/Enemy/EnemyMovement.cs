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

    [Header("Enemy Setup")]
    [SerializeField] private float _pauseWalkDuration = 3f;
    [SerializeField] private float _rotationDuration = .35f;

    [Header("Enemy Routine")]
    [SerializeField] private List<Transform> _pathTransforms;
    [SerializeField] private float _movementSpeed;

    private Animator _animator;
    private List<Vector3> _path;
    private float _directionSpeed;

    private Coroutine _walkRoutine;
    private Coroutine _checkDestinationRoutine;
    private Tween _currentMovementTween;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _path = new();
        _pathTransforms.ForEach(path => _path.Add(path.position));

        _walkRoutine = StartCoroutine(WalkRoutine());
    }

    public void CheckDestination(Vector2 destination)
    {
        if (_checkDestinationRoutine == null)
        {
            _checkDestinationRoutine = StartCoroutine(CheckDestinationRoutine(destination));
        }
        else
        {
            StopCoroutine(_checkDestinationRoutine);
            _checkDestinationRoutine = StartCoroutine(CheckDestinationRoutine(destination, true));
        }
    }

    private IEnumerator CheckDestinationRoutine(Vector2 destination, bool persuing = false)
    {
        _currentMovementTween?.Kill();
        StopCoroutine(_walkRoutine);

        _animator.ResetTrigger("Walk");
        _animator.SetTrigger("Idle");
        _directionSpeed = transform.position.x - destination.x;
        _currentMovementTween = transform.DORotate(Vector2.up * (_directionSpeed > 0 ? 180f : 0f), _rotationDuration);
        yield return _currentMovementTween.WaitForCompletion();

        if (!persuing)
        {
            yield return new WaitForSeconds(_pauseWalkDuration / 2f);
        }

        _animator.ResetTrigger("Idle");
        _animator.SetTrigger("Walk");
        _currentMovementTween = transform.DOMoveX(destination.x, CalculatePathDuration(destination));
        yield return _currentMovementTween.WaitForCompletion();

        _animator.ResetTrigger("Walk");
        _animator.SetTrigger("Idle");
        yield return new WaitForSeconds(_pauseWalkDuration);

        _walkRoutine = StartCoroutine(WalkRoutine());
        _checkDestinationRoutine = null;
    }

    private IEnumerator WalkRoutine()
    {
        foreach (Vector2 destination in Destinations())
        {
            _directionSpeed = transform.position.x - destination.x;
            _currentMovementTween = transform.DORotate(Vector2.up * (_directionSpeed > 0 ? 180f : 0f), _rotationDuration);
            yield return _currentMovementTween.WaitForCompletion();

            _animator.ResetTrigger("Idle");
            _animator.SetTrigger("Walk");
            _currentMovementTween = transform.DOMoveX(destination.x, CalculatePathDuration(destination));
            yield return _currentMovementTween.WaitForCompletion();

            _animator.ResetTrigger("Walk");
            _animator.SetTrigger("Idle");
            yield return new WaitForSeconds(_pauseWalkDuration);
        }
    }

    private IEnumerable<Vector2> Destinations()
    {
        int currentDestination = 0;
        while (true)
        {
            if (currentDestination < _path.Count)
            {
                yield return _path[currentDestination];
                currentDestination++;
            }
            else
            {
                currentDestination = 0;
            }
        }
    }

    private float CalculatePathDuration(Vector2 destination)
    {
        return Vector3.Distance(transform.position, destination) / _movementSpeed;
    }
}
