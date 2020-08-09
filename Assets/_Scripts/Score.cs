using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour
{
    private int score = 0;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Transform[] collectibles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Spheres collected: " + score.ToString() + "/" + collectibles.Length.ToString();
    }

    public void AddScore()
	{
        score++;
     
	}
}
