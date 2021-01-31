using System;
using UnityEngine;
using UnityEngine.UI;

public class MathOperationsSetupController : MonoBehaviour
{

    public InputField CalculateUpToInputField;
    public Slider Duration;
    public Toggle AdditionOption;
    public Toggle SubstractiomOption;
    public Toggle MultiplicationOption;
    public Toggle DivisionOption;
    public ToggleGroup GameType;
    public Button PlayButton;
    public GameHandler SlitterScreenPrefab;
    public BalloonGameController BallonController;

    public static int max = 0;
    public static float duration = 0;
    // Start is called before the first frame update
    void Start()
    {
        PlayButton.onClick.AddListener(PlayButtonClicked);   
    }

    private void PlayButtonClicked()
    {
        duration = Duration.value * 60;

        if (CalculateUpToInputField.text != string.Empty) {
            max = Convert.ToInt32(CalculateUpToInputField.text);
        }
        else
        {
            max = 20;
        }
        
        Instantiate(BallonController);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
