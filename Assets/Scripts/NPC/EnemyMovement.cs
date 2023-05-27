using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    public enum EnemyMode
    {
        PATROLLING,
        STAND_WATCH
    }

    [Header("Enemy Setup")]
    [SerializeField] private float _pauseWalkDuration = 3f;
    [SerializeField] private float _rotationDuration = .35f;

    [Header("Enemy Routine")]
    [SerializeField] private EnemyMode _enemyMode = EnemyMode.PATROLLING;
    [Header("Patrolling Setup")]
    [SerializeField] private List<Transform> _pathTransforms;
    [SerializeField] private float _movementSpeed;
    [Header("Stand watch Setup")]
    [SerializeField] private float _originalStandDirection;

    private Animator _animator;
    private List<Vector3> _path;
    private float _directionSpeed;

    private Coroutine _walkRoutine;
    private Coroutine _checkDestinationRoutine;
    private Tween _currentMovementTween;
    private bool _hasStartedScript = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _path = new();
        _pathTransforms.ForEach(path => _path.Add(path.position));
    }

    private void OnBecameVisible()
    {
        if (!_hasStartedScript)
        {
            _hasStartedScript = true;
            _walkRoutine = StartCoroutine(WalkRoutine());
        }
    }

    public void StartWalkRoutine()
    {
        if (_walkRoutine != null) StopCoroutine(_walkRoutine);

        _enemyMode = EnemyMode.PATROLLING;
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
        if (_walkRoutine != null) StopCoroutine(_walkRoutine);

        _animator.ResetTrigger("Walk");
        _animator.SetTrigger("Idle");

        _directionSpeed = transform.position.x - destination.x;

        _currentMovementTween = transform.DORotate(Vector2.up * (_directionSpeed > 0 ? 180f : 0f), _rotationDuration);
        yield return _currentMovementTween.WaitForCompletion();

        if (_enemyMode == EnemyMode.STAND_WATCH)
        {
            yield return new WaitForSeconds(3f);
            yield return transform.DORotate(Vector2.up * _originalStandDirection, _rotationDuration).WaitForCompletion();
            yield break;
        }

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
        if (_enemyMode != EnemyMode.PATROLLING) yield break;

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
        while (_path.Count > 0)
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
