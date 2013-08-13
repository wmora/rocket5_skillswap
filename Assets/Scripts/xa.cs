using UnityEngine;
using System.Collections;

public class xa : MonoBehaviour 
{
	public static Ball ball;
	public static AudioManager audioManager;
	public static ScoreManager scoreManager;
	
	public static Player1 player1;
	public static Player2 player2;
	//public static Player3 player3;
	//public static Player4 player4;
	
	public static bool gameOver = false;
	
	// set to false if game is configured for CTF
	public static bool letsPlayKeepaway = false; 
	
	// layers
	public const int Team1Goal = 9;
	public const int Team2Goal = 10;
	
	void Start()
	{
		// cache these so they can be accessed by other scripts
		ball = GameObject.FindWithTag("Ball").GetComponent<Ball>();
		scoreManager = gameObject.GetComponent<ScoreManager>();
		audioManager = gameObject.GetComponent<AudioManager>();
		player1 = GameObject.Find("Player1").GetComponent<Player1>();
		player2 = GameObject.Find("Player2").GetComponent<Player2>();
		//player3 = GameObject.Find("Player3").GetComponent<Player3>();
		//player4 = GameObject.Find("Player4").GetComponent<Player4>();
	}
}
