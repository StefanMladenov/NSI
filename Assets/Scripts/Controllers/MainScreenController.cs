using UnityEngine;
using UnityEngine.UI;
using PlayNLearn;

public class MainScreenController : MonoBehaviour
{
    public Text Title;
    public Button PlayButton;
    public Button HighscoresButton;
    public Button SettingsButton;
    public Button QuitButton;
    public Canvas UI;

    public delegate void ButtonClicked(MainScreenButtonEnum button);
    public static event ButtonClicked BtnClicked;

    void Start()
    {
        PlayButton.onClick.AddListener(PlayButtonClicked);
        HighscoresButton.onClick.AddListener(HighScoreButtonClicked);
        SettingsButton.onClick.AddListener(SettingsButtonClicked);
        QuitButton.onClick.AddListener(QuitButtonClicked);
    }


    private void PlayButtonClicked()
    {
        if (BtnClicked != null)
        {
            BtnClicked(MainScreenButtonEnum.Play);
        }
    }

    private void HighScoreButtonClicked()
    {
        if (BtnClicked != null)
        {
            BtnClicked(MainScreenButtonEnum.Highscores);
        }
    }

    private void SettingsButtonClicked()
    {
        if (BtnClicked != null)
        {
            BtnClicked(MainScreenButtonEnum.Settings);
        }
    }

    private void QuitButtonClicked()
    {
        if (BtnClicked != null)
        {
            BtnClicked(MainScreenButtonEnum.Quit);
        }
    }
}
