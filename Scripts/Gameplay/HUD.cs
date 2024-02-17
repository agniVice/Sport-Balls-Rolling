using System.Collections;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour, IInitializable, ISubscriber
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;

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
        GetComponent<Canvas>().worldCamera = Camera.main;

        Show();

        _scoreText.gameObject.transform.position = GameObject.FindGameObjectWithTag("ScoreBoard").transform.position;
        _highScoreText.gameObject.transform.position = GameObject.FindGameObjectWithTag("HighScoreBoard").transform.position;

        _highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("HighScore", 0);
        _isInitialized = true;
    }
    public void SubscribeAll()
    {
        GameState.Instance.GameFinished += Hide;
        GameState.Instance.GamePaused += Hide;
        GameState.Instance.GameUnpaused += Show;

        GameState.Instance.ScoreAdded += UpdateScoreText;
    }
    public void UnsubscribeAll()
    {
        GameState.Instance.GameFinished -= Hide;
        GameState.Instance.GamePaused -= Hide;
        GameState.Instance.GameUnpaused -= Show;

        GameState.Instance.ScoreAdded -= UpdateScoreText;
    }
    private void UpdateScoreText()
    {
        _scoreText.text = "Score: " + PlayerScore.Instance.Score;
    }
    private void Show()
    {
        _panel.SetActive(true);
    }
    private void Hide()
    {
        _panel.SetActive(false);
    }
    public void OnPauseButtonClicked()
    {
        GameState.Instance.PauseGame();
    }
}