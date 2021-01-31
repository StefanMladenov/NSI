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
        GameObject go = _currentScreen;

        switch (button)
        {
            case MainScreenButtonEnum.Play:
                {

                    _currentScreen = Instantiate(PlayScreen);
                    Destroy(go);
                    break;
                }
            case MainScreenButtonEnum.Highscores:
                {
                    _currentScreen = Instantiate(HighscoreScreen);
                    Destroy(go);
                    break;
                }
            case MainScreenButtonEnum.Settings:
                {
                    _currentScreen = Instantiate(SettingsScreen);
                    Destroy(go);
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
