using System;
using System.Collections.Generic;
using UnityEngine;

public class TsumManager : MonoBehaviour
{
    public static TsumManager Instance;

    public List<Tsum> TsumsSelected;

    public Tsum FirstSelected;

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

    private void Update()
    {
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
                
                for (int i = 0; i < TsumsSelected.Count; i++)
                {
                    LevelGenerator.Instance.GenerateLevel();
                }
            }

            TsumsSelected.Clear();
            FirstSelected = null;
        }
    }
}