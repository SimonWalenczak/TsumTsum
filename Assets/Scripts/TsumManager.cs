using System;
using System.Collections.Generic;
using UnityEngine;

public class TsumManager : MonoBehaviour
{
    public static TsumManager Instance;
    
    public List<Tsum> TsumsInGame;
    public List<Tsum> TsumsSelected;

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
    
    
}
