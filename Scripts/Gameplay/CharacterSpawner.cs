using Dreamteck.Splines;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour, IInitializable
{
    [SerializeField] private float _characterStartSpeed;
    [SerializeField] private float _characterStartAccelerateSpeed;
    [SerializeField] private float _characterStartBrakeSpeed;
    [SerializeField] private Sprite _characterStartSprite;

    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private GameObject _splinePrefab;

    private GameObject _character;
    private GameObject _spline;

    public void Initialize()
    {
        _spline = Instantiate(_splinePrefab);

        SpawnCharacter();
    }
    private void SpawnCharacter()
    {
        if (_character != null)
            ClearCharacter();

        _character = Instantiate(_characterPrefab);

        var spawnedCharacter = _character.GetComponent<Character>();
        spawnedCharacter.Initialize(_characterStartAccelerateSpeed, _characterStartBrakeSpeed,
            _characterStartSpeed, _characterStartSprite, _spline.GetComponent<SplineComputer>());
    }
    private void ClearCharacter()
    {
        Destroy(_character);

        _character = null;
    }
}
