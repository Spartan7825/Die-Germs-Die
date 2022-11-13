using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetScores : MonoBehaviour
{
    public TextMeshProUGUI HiScore;
    public TextMeshProUGUI UrScore;
    // Start is called before the first frame update
    void Start()
    {
        HiScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        UrScore.text = PlayerPrefs.GetInt("Score").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
