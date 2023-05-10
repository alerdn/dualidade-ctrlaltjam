using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcRenderer : MonoBehaviour
{
    public bool ReadyToThrow => _readyToThrow;

    [Header("Setup")]
    [SerializeField] private float _minVelocity = 6f;
    [SerializeField] private float _angle = 65;
    [SerializeField] private int _resolution = 10;

    private LineRenderer _lineRenderer;
    private float _g;
    private float _radianAngle;
    private float _velocity;
    private bool _readyToThrow;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _g = Mathf.Abs(Physics2D.gravity.y);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            _readyToThrow = !_readyToThrow;
        }

        if (_readyToThrow)
        {
            _velocity = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            Vector2 delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            _angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
            if (_angle < 0) _angle += 360;


            if (_velocity <= 0)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                _velocity = (-_velocity > _minVelocity ? _velocity : _minVelocity);
            }
            else
            {
                transform.rotation = Quaternion.identity;
                _velocity = _velocity > _minVelocity ? _velocity : _minVelocity;
            }

            RenderArc();
        }
    }

    private void RenderArc()
    {
        _lineRenderer.positionCount = _resolution + 1;
        _lineRenderer.SetPositions(CalculateArcArray());
    }

    private Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[_resolution + 1];

        _radianAngle = Mathf.Deg2Rad * _angle;
        float maxDistance = (_velocity * _velocity * Mathf.Sin(2 - _radianAngle)) / _g;

        for (int i = 0; i <= _resolution; i++)
        {
            float t = (float)i / (float)_resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }

        return arcArray;
    }

    private Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(_radianAngle) - ((_g * x * x) / (2 * _velocity * _velocity * Mathf.Cos(_radianAngle) * Mathf.Cos(_radianAngle)));
        return new Vector3(x, y);
    }
}
