using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private int _timerStartValue;
    private float _actualTimer;
    [SerializeField] private Image _timerDebug;

    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _comboPanel;
    private TMP_Text _comboText;
    
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
        
        _comboText = _comboPanel.GetComponentInChildren<TMP_Text>();
        _comboText.text = "0";
    }

    private void Update()
    {
        if (StartTimer)
        {
            _actualTimer -= Time.deltaTime;
            _timerText.text = ((int)_actualTimer).ToString();

            _timerDebug.fillAmount = _actualTimer / _timerStartValue;
        }

        if (TsumManager.Instance.nbCombo != 0)
        {
            _comboPanel.gameObject.SetActive(true);
            _comboText.text = TsumManager.Instance.nbCombo.ToString();
        }
        else
        {
            _comboPanel.gameObject.SetActive(false);
        }
    }
}