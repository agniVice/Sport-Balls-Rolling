using Dreamteck.Splines;
using UnityEngine;

public class Character : MonoBehaviour, IBall
{
    public float AccelerateSpeed { get; private set; }
    public float BrakeSpeed { get; private set; }
    public float NormalSpeed { get; private set; }
    public bool IsMoving { get; private set; }

    private float _currentSpeed;
    private SplineFollower _follower;

    private void OnEnable()
    {
        GameState.Instance.GameStarted += StartMove;
        GameState.Instance.GamePaused += StopMove;
        GameState.Instance.GameUnpaused += StartMove;
        GameState.Instance.GameFinished += StopMove;

        PlayerInput.Instance.PlayerMouseDown += OnPlayerMouseDown;
        PlayerInput.Instance.PlayerMouseUp += OnPlayerMouseUp;
    }
    private void OnDisable()
    {
        GameState.Instance.GameStarted -= StartMove;
        GameState.Instance.GamePaused -= StopMove;
        GameState.Instance.GameUnpaused -= StartMove;
        GameState.Instance.GameFinished -= StopMove;

        PlayerInput.Instance.PlayerMouseDown -= OnPlayerMouseDown;
        PlayerInput.Instance.PlayerMouseUp -= OnPlayerMouseUp;
    }
    private void FixedUpdate()
    {
        if (!IsMoving)
            return;

        _follower.followSpeed = _currentSpeed;
    }
    public void Initialize(float accelerateSpeed, float brakeSpeed, float normalSpeed, Sprite visual, SplineComputer spline)
    {
        AccelerateSpeed = accelerateSpeed;
        BrakeSpeed = brakeSpeed;
        NormalSpeed = normalSpeed;

        GetComponent<SpriteRenderer>().sprite = visual;

        _follower = GetComponent<SplineFollower>();

        _follower.spline = spline;
        _follower.follow = false;
        _follower.followSpeed = 0f;

        _currentSpeed = 0f;

        IsMoving = false;
    }
    private void SetAccelerateSpeed()
    {
        _currentSpeed = AccelerateSpeed;
    }
    private void SetBreakeSpeed()
    {
        _currentSpeed = BrakeSpeed;
    }
    private void SetNormalSpeed()
    {
        _currentSpeed = NormalSpeed;
    }
    private void OnPlayerMouseDown()
    {
        if (!IsMoving)
            return;

        SetAccelerateSpeed();
    }
    private void OnPlayerMouseUp() 
    {
        if (!IsMoving)
            return;

        SetNormalSpeed();
    }
    public void StartMove()
    {
        _follower.follow = true;
        IsMoving = true;
    }
    public void StopMove()
    {
        _follower.follow = false;
        IsMoving = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.ScoreAdded, 1f);
            PlayerScore.Instance.AddScore();
        }
        if (collision.CompareTag("Enemy"))
        {
            AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.BallCrashed, 1f);
            GameState.Instance.FinishGame();
        }
    }
}