using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance;

    [SerializeField] private GameObject _tsumsParent;
    [SerializeField] private GameObject _tsumPrefab;
    public List<Sprite> TsumSprites;

    [SerializeField] private int _nbTsumsByGeneration;
    private int _nbTsumsSpawned;

    [SerializeField] private Vector2Int _spawnRange;
    [SerializeField] float timerReset = 0.2f;
    private float actualTimer;

    [SerializeField] private float _timeBeforeGameStart = 2;
    [SerializeField] private GameObject StartText;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There is already another LevelGenerator in this scene !");
        }
    }

    private void Start()
    {
        float actualTimer = timerReset;
    }

    private void Update()
    {
        actualTimer -= Time.deltaTime;

        if (actualTimer <= 0 && _nbTsumsSpawned < _nbTsumsByGeneration)
        {
            actualTimer = timerReset;
            GenerateLevel();
            _nbTsumsSpawned++;
        }

        if (_nbTsumsSpawned == _nbTsumsByGeneration)
        {
            StartCoroutine(StartGame());
            _nbTsumsSpawned++;
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2);
        StartText.SetActive(true);
        yield return new WaitForSeconds(1);
        StartText.SetActive(false);
        yield return new WaitForSeconds(1);
        UIManager.Instance.StartTimer = true;
    }

    public void GenerateLevel()
    {
        //Choose spawn position
        float spawnPosX = Random.Range(_spawnRange.x, _spawnRange.y);
        Vector3 SpawnPos = new Vector3(spawnPosX, 6.5f, 0);

        //Choose tsum sprite
        int indexTsum = Random.Range(0, TsumSprites.Count);
        Sprite spawnSprite = TsumSprites[indexTsum];

        var actualTsum = Instantiate(_tsumPrefab, SpawnPos, Quaternion.identity);

        actualTsum.transform.parent = _tsumsParent.transform;

        actualTsum.GetComponent<SpriteRenderer>().sprite = spawnSprite;
        actualTsum.GetComponent<Tsum>().IndexType = indexTsum;
    }
}