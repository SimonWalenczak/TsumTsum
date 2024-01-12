using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private int _timerStartValue;
    private float _actualTimer;
    [SerializeField] private Image _timerDebug;

    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _scoreText;

    [HideInInspector] public bool StartTimer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There is already another UIManager in this scene !");
        }
    }

    private void Start()
    {
        _actualTimer = _timerStartValue;
        _timerText.text = _actualTimer.ToString();
        _scoreText.text = "0";
    }

    private void Update()
    {
        if (StartTimer)
        {
            _actualTimer -= Time.deltaTime;
            _timerText.text = ((int)_actualTimer).ToString();

            _timerDebug.fillAmount = _actualTimer / _timerStartValue;
        }
    }
}