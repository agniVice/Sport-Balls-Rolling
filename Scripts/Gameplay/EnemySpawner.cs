using Dreamteck.Splines;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IInitializable, ISubscriber
{
    [SerializeField] private EnemyInfo _enemyInfo;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _splinePrefab;

    private List<GameObject> _enemies = new List<GameObject>();
    private GameObject _spline;

    private bool _isInitialized;
    private void OnEnable()
    {
        if (!_isInitialized)
            return;

        SubscribeAll();
    }
    private void OnDisable()
    {
        UnsubscribeAll();
    }
    public void Initialize()
    {
        _spline = Instantiate(_splinePrefab);
        _isInitialized = true;
    }
    public void SubscribeAll()
    {
        GameState.Instance.GameStarted += SpawnEnemy;
        GameState.Instance.DifficultyChanged += SpawnEnemy;
    }
    public void UnsubscribeAll()
    {
        GameState.Instance.GameStarted -= SpawnEnemy;
        GameState.Instance.DifficultyChanged -= SpawnEnemy;
    }
    private void SpawnEnemy()
    {
        var spawnedEnemy = Instantiate(_enemyPrefab);
        _enemies.Add(spawnedEnemy);

        spawnedEnemy.GetComponent<Enemy>().Initialize(GetRandomSpeed(), GetRandomSprite(), GetRandomDirection(), GetRandomDistance(), _spline.GetComponent<SplineComputer>());
        spawnedEnemy.GetComponent<Enemy>().StartMove();

        AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.BallSpawned, 1f);
    }
    private float GetRandomSpeed()
    { 
        return Random.Range(_enemyInfo.StartSpeed-1, _enemyInfo.StartSpeed+5) + (_enemyInfo.IncreaseSpeedRandomValue * GameState.Instance.Difficulty);
    }
    private Sprite GetRandomSprite()
    {
        return _enemyInfo.Sprites[Random.Range(0, _enemyInfo.Sprites.Length)];
    }
    private float GetRandomDistance()
    {
        return Random.Range(0f, 1f);
    }
    private bool GetRandomDirection()
    {
        float random = Random.Range(0, 100);

        if (random >= 50)
            return false;
        else
            return true;
    }
}
