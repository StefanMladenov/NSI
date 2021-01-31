
using System.Collections;
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
    // Start is called before the first frame update
    void Start()
    {
        listOfExpressions = RandomMath.GenerateRandomEquation(MathOperationsSetupController.max);
        InstantiateBallons();
        PickNewQuestion();
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

        for (int i = 0; i < 4; i++)
        {
            int offset = Random.Range(1, 10) * Mathf.RoundToInt(Mathf.Sign(Random.Range(-1, 1)));
            listOfBallons[i].result.text = (currentEquasion.result + offset).ToString();
        }

        indexOfCorrect = Random.Range(0, 4);
        listOfBallons[indexOfCorrect].result.text = currentEquasion.result.ToString();
    }

    public void TimeExpired()
    {
        Debug.Log("Isteklo vreme");
    }
}
