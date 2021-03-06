﻿
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalloonGameController : MonoBehaviour
{
    public Text expressionText;
    public BalloonScript BallonScriptPrefab;
    public Transform BallonHolder;
    private List<BalloonScript> listOfBallons = new List<BalloonScript>();
    List<RandomMath.EquationStruct> listOfExpressions;
    private int index = 0;
    private int indexOfCorrect = 0;
    private RandomMath.EquationStruct currentEquasion;
    public MathOperationsSetupController MathOperationSetupController;
    public Button BackButton;
    public Button QuitButton;
    public Text goodAnswerText;
    private int goodAnswers = 0;

    // Start is called before the first frame update
    void Start()
    {
        listOfExpressions = RandomMath.GenerateRandomEquation(MathOperationsSetupController.max);
        BackButton.onClick.AddListener(backButtonClicked);
        QuitButton.onClick.AddListener(quitButtonClicked);
        InstantiateBallons();
        PickNewQuestion();
        goodAnswerText.text = goodAnswers.ToString();
    }

    private void quitButtonClicked()
    {
        Application.Quit();
    }

    private void backButtonClicked()
    {
        Instantiate(MathOperationSetupController);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateBallons()
    {
        for (int i = 0; i < listOfBallons.Count; i++)
        {
            Destroy(listOfBallons[i]);
        }

        listOfBallons.Clear();

        for (int i = 0; i < 4; i++)
        {
            listOfBallons.Add(Instantiate(BallonScriptPrefab, BallonHolder));
            listOfBallons[i].onClick += BalloonGameController_onClick;
            LeanTween.cancel(listOfBallons[i].gameObject);
            listOfBallons[i].transform.localScale = Vector3.zero;
            LeanTween.scale(listOfBallons[i].gameObject, Vector3.one, 1f).setEaseOutBounce();
        }
    }

    private void BalloonGameController_onClick(BalloonScript obj)
    {
        if (listOfBallons.IndexOf(obj) == indexOfCorrect)
        {
            Debug.Log("Answer is correct");
            goodAnswers++;
            goodAnswerText.text = goodAnswers.ToString();
        }
        else
        {
            Debug.Log("Answer is incorrect");
        }

        PickNewQuestion();
    }

    public void PickNewQuestion()
    {
        if (index >= listOfExpressions.Count)
        {
            listOfExpressions = RandomMath.GenerateRandomEquation(MathOperationsSetupController.max);
            index = 0;
        }

        currentEquasion = listOfExpressions[index];
        index++;
        expressionText.text = currentEquasion.equation;
        List<int> randomResults = new List<int>();

        for (int i = 0; i < 4; i++)
        {
            int randomResult = generateRandomResult();
            while (randomResults.Contains(randomResult))
            {
                randomResult = generateRandomResult();
            }
            randomResults.Add(randomResult);
        }

        for (int i = 0; i < 4; i++)
        {
            listOfBallons[i].result.text = randomResults[i].ToString();
        }

            indexOfCorrect = Random.Range(0, 4);
        listOfBallons[indexOfCorrect].result.text = currentEquasion.result.ToString();
    }

    public void TimeExpired()
    {
        for (int i = 0; i < listOfBallons.Count; i++)
        {
            Destroy(listOfBallons[i]);
        }

        listOfBallons.Clear();
        expressionText.text = "Time is up";
        Debug.Log("Isteklo vreme");
    }

    public int generateRandomResult()
    {
        int offset = Random.Range(1, 10) * Mathf.RoundToInt(Mathf.Sign(Random.Range(-1, 1)));
        return currentEquasion.result + offset;
    }
}
