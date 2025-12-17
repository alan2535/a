using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailPanelController : CanvasController
{
    [SerializeField] private Button _restartButton;

    void Start()
    {
        _restartButton.onClick.AddListener(OnClickRestartButton);
    }

    public void OnClickRestartButton()
    {
        LevelManager.Instance.SetCurrentLevel(0);

        PlayerPrefs.SetInt("Coin", 0);

        GameManager.Instance.RestartGame();

        AudioManager.Instance.StopSound("GameOverSound");

        HidePanel();
    }
}