using UnityEngine;
using PlayNLearn;
using System;

public class GameController : MonoBehaviour
{
    public GameObject MainScreen;
    public GameObject PlayScreen;
    public GameObject HighscoreScreen;
    public GameObject SettingsScreen;
    public Camera MainCamera;
    private GameObject _currentScreen = null;


    // Start is called before the first frame update
    void Start()
    {
        _currentScreen = Instantiate(MainScreen);
        MainScreenController.BtnClicked += MainScreenButtonClicked;
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    void MainScreenButtonClicked(MainScreenButtonEnum button)
    {
        _currentScreen = null;

        switch (button)
        {
            case MainScreenButtonEnum.Play:
                {
                    _currentScreen = Instantiate(PlayScreen);
                    break;
                }
            case MainScreenButtonEnum.Highscores:
                {
                    _currentScreen = Instantiate(HighscoreScreen);
                    break;
                }
            case MainScreenButtonEnum.Settings:
                {
                    _currentScreen = Instantiate(SettingsScreen);
                    break;
                }
            case MainScreenButtonEnum.Quit:
                {
                    Application.Quit();
                    break;
                }
        }
    }
}
