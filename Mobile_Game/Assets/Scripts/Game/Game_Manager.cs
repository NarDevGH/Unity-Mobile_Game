using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public int points;

    [Space]
    [SerializeField] private first_Stage _firstStage;

    public static Game_Manager Singleton;

    private void Awake()
    {
        HandleInstances();
        points = 0;
    }

    public void StartGameRoutine() 
    {
        StartCoroutine(GameRoutine());
    }

    private IEnumerator GameRoutine() 
    {
        yield return StartCoroutine(_firstStage.StageRoutine());
        yield return StartCoroutine(Curtains_Manager.Singletons.CloseCurtainsRoutine());
        Menu_Manager.Singleton._PanelsContainer.SetActive(true);
        Menu_Manager.Singleton._gameoverPanel.GetComponent<GameoverPanel_Manager>().SetPoints(points);
        yield return StartCoroutine(Curtains_Manager.Singletons.OpenCurtainsRoutine());
    }

    private void HandleInstances()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else 
        {
            Destroy(this);
        }
    }
}
