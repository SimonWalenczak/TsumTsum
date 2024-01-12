using System.Collections.Generic;
using UnityEngine;

public class TsumManager : MonoBehaviour
{
    public static TsumManager Instance;
    
    [Header("Tsum Selection")]
    public List<Tsum> TsumsSelected;
    public Tsum FirstSelected;

    [Space (10)][Header("Combo")]
    public int nbCombo;
    [SerializeField] private float _timerComboReset;
    [SerializeField] private float _actualTimerCombo;

    [Space(10)] [Header("Score")] 
    private Dictionary<int, int> ScoreDico = new Dictionary<int, int>();
    public int ActualScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There is already another TsumManager in this scene !");
        }
    }

    private void Start()
    {
        InitializedScore();
        
        _actualTimerCombo = _timerComboReset;
        _actualTimerCombo = 0;
    }

    private void InitializedScore()
    {
        ScoreDico.Add(3,10);
        ScoreDico.Add(4,25);
        ScoreDico.Add(5,50);
        ScoreDico.Add(6,100);
        ScoreDico.Add(7,150);
        ScoreDico.Add(8,220);
        ScoreDico.Add(9,250);
        ScoreDico.Add(10,300);
    }
    private void Update()
    {
        //Check the selection on mouse up
        #region Selection
        if (Input.GetMouseButtonUp(0) && TsumsSelected != null)
        {
            foreach (var tsum in TsumsSelected)
            {
                tsum.IsSelected = false;
            }

            if (TsumsSelected.Count >= 3)
            {
                foreach (var tsum in TsumsSelected)
                {
                    Destroy(tsum.gameObject);
                }

                nbCombo++;
                _actualTimerCombo = _timerComboReset;
                
                #region Score
                if (ScoreDico.ContainsKey(TsumsSelected.Count))
                {
                    ActualScore += ScoreDico[TsumsSelected.Count];
                }
                #endregion
                
                for (int i = 0; i < TsumsSelected.Count; i++)
                {
                    LevelGenerator.Instance.GenerateLevel();
                }
            }

            TsumsSelected.Clear();
            FirstSelected = null;
        }
        #endregion

        #region Combo
        if (nbCombo != 0)
        {
            _actualTimerCombo -= Time.deltaTime;

            if (_actualTimerCombo <= 0)
            {
                _actualTimerCombo = _timerComboReset;
                nbCombo = 0;
            }
        }
        #endregion
    }
}