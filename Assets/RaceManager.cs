using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance;

    [Header("RaceData")]
    [SerializeField]private int _lapsToWin = 3;
    private Text _winnerOutput;
    [SerializeField]private List<PlayerController> _players;


    public Text GetWinnerText()
    {
        return _winnerOutput;
    }
    
    
    //create instance of raceManager to call it in PlayerController class
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        if (_players == null)
        {
            _players[0] = FindObjectOfType<PlayerController>();
        }
    }

    public void CheckWinPlayer(PlayerController player)
    {
        if (player.GetCurrentLap() >= _lapsToWin)
        {
            Debug.Log($"{player.name} WIN!");
            Time.timeScale = 0.0f;
        }
        
    }
}
