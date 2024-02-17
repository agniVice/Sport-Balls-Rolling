using Dreamteck.Splines;
using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IBall
{
    private bool _isMoving;
    private float _speed;

    private SplineFollower _follower;

    private void OnEnable()
    {
        GameState.Instance.GamePaused += StopMove;
        GameState.Instance.GameUnpaused += StartMove;
        GameState.Instance.GameFinished += StopMove;
    }
    private void OnDisable()
    {
        GameState.Instance.GamePaused -= StopMove;
        GameState.Instance.GameUnpaused -= StartMove;
        GameState.Instance.GameFinished -= StopMove;
    }
    private void Awake()
    {
        _follower = GetComponent<SplineFollower>();
    }
    private void FixedUpdate()
    {
        if (!_isMoving)
            return;

        _follower.followSpeed = _speed;
    }
    public void Initialize(float speed, Sprite visual, bool isForward, float distance, SplineComputer spline)
    {
        GetComponent<SpriteRenderer>().sprite = visual;

        _speed = speed;
        _isMoving = false;

        _follower.autoStartPosition = true;
        _follower.spline = spline;
        _follower.follow = false;
        _follower.followSpeed = 0f;
        _follower.SetDistance(distance);

        if (!isForward)
            _speed *= -1;
    }

    public void StartMove()
    {
        _follower.follow = true;
        _isMoving = true;
    }

    public void StopMove()
    {
        _follower.follow = false;
        _isMoving = false;
    }
}
