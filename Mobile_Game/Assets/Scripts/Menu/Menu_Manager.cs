using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Manager : MonoBehaviour
{
    public GameObject _PanelsContainer;
    public GameObject _mainMenuPanel;
    public GameObject _gameoverPanel;

    public static Menu_Manager Singleton;

    private void Awake()
    {
        if (Singleton == null) Singleton = this;
        else Destroy(this);

        _PanelsContainer.SetActive(true);
        _mainMenuPanel.SetActive(true);
        _gameoverPanel.SetActive(false);
    }

    public void On_PlayButton_Pressed() 
    {
        Start_PlayRoutine();
    }

    public void On_ReplayButton_Pressed()
    {
        Start_ReplayRoutine();
    }

    public void On_ExitButton_Pressed() 
    {
        Application.Quit();
    }

    private void Start_PlayRoutine() 
    {
        StartCoroutine(PlayRoutine());
    }

    private void Start_ReplayRoutine()
    {
        StartCoroutine(ReplayRoutine());
    }

    private IEnumerator PlayRoutine() 
    {
        yield return null;
        yield return StartCoroutine(Curtains_Manager.Singletons.CloseCurtainsRoutine());
        _mainMenuPanel.SetActive(false);
        _gameoverPanel.SetActive(true);
        _PanelsContainer.SetActive(false);
        yield return StartCoroutine(Curtains_Manager.Singletons.OpenCurtainsRoutine());
        Game_Manager.Singleton.StartGameRoutine();
    }

    private IEnumerator ReplayRoutine()
    {
        yield return StartCoroutine(Curtains_Manager.Singletons.CloseCurtainsRoutine());
        _PanelsContainer.SetActive(false);
        yield return StartCoroutine(Curtains_Manager.Singletons.OpenCurtainsRoutine());
        Game_Manager.Singleton.StartGameRoutine();
    }
}
