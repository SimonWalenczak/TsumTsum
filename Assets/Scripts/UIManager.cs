using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private GameObject _comboPanel;
    private TMP_Text _comboText;

    [HideInInspector] public bool StartTimer;

    [Space(10)] [Header("SuperPower")] public bool SPUnlock;
    [SerializeField] private GameObject SPButton;
    [SerializeField] private GameObject FreezeDebug;
    float actualFreezeTime;

    [Space(10)] [Header("GameOverPanel")] [SerializeField]
    private GameObject GameOverPanel;

    public List<Image> Stars;
    [SerializeField] private TMP_Text FinalScoreText;

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

        _comboText = _comboPanel.GetComponentInChildren<TMP_Text>();
        _comboText.text = "0";

        actualFreezeTime = LevelGenerator.Instance.FreezeTimeSP;
    }

    private void Update()
    {
        if (SPUnlock)
        {
            SPButton.SetActive(true);
        }
        else
        {
            SPButton.SetActive(false);
        }

        if (LevelGenerator.Instance.TimeFreeze)
        {
            FreezeDebug.SetActive(true);

            actualFreezeTime -= Time.deltaTime;

            if (actualFreezeTime <= 0)
            {
                actualFreezeTime = LevelGenerator.Instance.FreezeTimeSP;
                FreezeDebug.SetActive(false);
                LevelGenerator.Instance.TimeFreeze = false;
            }
        }

        _scoreText.text = TsumManager.Instance.ActualScore.ToString();

        if (StartTimer)
        {
            if (_actualTimer > 0)
            {
                if (LevelGenerator.Instance.TimeFreeze == false)
                {
                    _actualTimer -= Time.deltaTime;
                    _timerText.text = ((int)_actualTimer).ToString();

                    _timerDebug.fillAmount = _actualTimer / _timerStartValue;
                }
            }
            else
            {
                GameOverPanel.SetActive(true);
                FinalScoreText.text = _scoreText.text;
                StartCoroutine(StarRewards());
            }
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

    IEnumerator StarRewards()
    {
        yield return new WaitForSeconds(1);
        if (TsumManager.Instance.ActualScore >= LevelGenerator.Instance.Star1Score)
        {
            Stars[0].color = Color.white;

            if (TsumManager.Instance.ActualScore >= LevelGenerator.Instance.Star2Score)
            {
                yield return new WaitForSeconds(1);
                Stars[1].color = Color.white;

                if (TsumManager.Instance.ActualScore >= LevelGenerator.Instance.Star3Score)
                {
                    yield return new WaitForSeconds(1);
                    Stars[2].color = Color.white;
                }
            }
        }
    }

    public void ActiveSP()
    {
        print("BOOOOM !");
        SPUnlock = false;
        TsumManager.Instance.SPGauge.fillAmount = 0;
        LevelGenerator.Instance.TimeFreeze = true;
    }
}