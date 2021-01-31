using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoresScreenController : MonoBehaviour
{
    public VerticalLayoutGroup ScoresTable;
    public Text Title;
    public GameObject UserScorePrefab;

    private Dictionary<string, int> userScores;
    public Dictionary<string, int> UserScores { get => userScores; set => userScores = value; }
    

    // Start is called before the first frame update
    void Start()
    {
        foreach(KeyValuePair<string, int>  entry in userScores)
        {
            Instantiate(UserScorePrefab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
