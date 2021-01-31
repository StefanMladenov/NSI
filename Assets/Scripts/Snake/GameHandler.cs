using System.Collections.Generic;
using UnityEngine;
using PlayNLearn;

public class GameHandler : MonoBehaviour
{
    private GameHandler instance;   
    //public Text equationText;

    private  string equation;
    public  int previousResult = 0;

    [SerializeField] private Snake snakePrefab;
     private Snake snake;
	private LevelGrid levelGrid;
    private int counter = -1;
    private List<RandomMath.EquationStruct> lista = new List<RandomMath.EquationStruct>();

    private void Awake()
    { 
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameHandler");
    	levelGrid = new LevelGrid(20,20);
        snake = Instantiate(snakePrefab);
    	snake.Setup(levelGrid);
    	levelGrid.Setup(snake);
        lista = RandomMath.GenerateRandomEquation(30);
    }

    public string GetEquation()
    {
        return equation;
    }

    public void AddEquation()
    {
        counter++;
        
        if (counter > lista.Count)
        {
            lista = RandomMath.GenerateRandomEquation(30);
            counter = -1;
        }

        equation = lista[counter++].equation;
        previousResult = lista[counter++].result;
    }
}
