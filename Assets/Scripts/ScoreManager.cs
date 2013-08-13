using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour 
{
	
	public OTTextSprite team1ProgressTxt;
	public OTTextSprite team2ProgressTxt;
	
	public OTTextSprite team1Wins;
	public OTTextSprite team2Wins;
	
	public OTTextSprite roundWinnerTxt;
	public OTTextSprite roundNextTxt;
	
	public int nextRoundTime = 5;
	
	private int team1Progress = 0;
	private int team2Progress = 0;
	
	private int team1Score = 0;
	private int team2Score = 0;
	
	private int nextRoundTimer = 0;
	
	private Color orange = new Color (0.47f,0.29f,0f);// new Color(0.94f,0.59f,0f);
	private Color blue = new Color(0f,0.34f,0.45f);// new Color(0f,0.69f,94f);
	
	public float scoreRate = 0.1f;//0.1F;
    protected float nextFire = 0f;
	
	protected float decreaseRate = 0.5f;
	protected float nextDecrease = 0f;
	
	protected int levelNum = 0;
	
	public Transform[] level;
	
	void Start()
	{
	}
	
	// player is holding the ball in own goal
	public void IncreaseScore(string team)
	{
		if(Time.time > nextFire)
		{
			nextFire = Time.time + scoreRate;
			if(team == "Team1")
			{				
				if(team1Progress < 100)
				{
					team1Progress += 1;
					team1ProgressTxt.text = team1Progress.ToString("D3") + "%"; // leading zeroes!
				}
				
				if(team1Progress >= 100)
				{
					Team1Wins();
				}
				
			}
			if(team == "Team2")
			{
				if(team2Progress < 100)
				{
					team2Progress += 1;
					team2ProgressTxt.text = team2Progress.ToString("D3") + "%"; // leading zeroes!
				}
				
				if(team2Progress >= 100)
				{
					Team2Wins();
				}
			}
			
			xa.audioManager.PlayProgress();
		}
	}
	
	// player is holding the ball outside own goal
	public void DecreaseScore(string team)
	{
		if(Time.time > nextDecrease)
		{
			nextDecrease = Time.time + decreaseRate;
			if(team == "Team1")
			{				
				if(team1Progress > 0)
				{
					team1Progress -= 1;
					team1ProgressTxt.text = team1Progress.ToString("D3") + "%"; // leading zeroes!
				}
			}
			if(team == "Team2")
			{
				if(team2Progress > 0)
				{
					team2Progress -= 1;
					team2ProgressTxt.text = team2Progress.ToString("D3") + "%"; // leading zeroes!
				}
			}
		}
	}
	
	void Team1Wins()
	{
		team1Score += 1;
		team1Wins.text = team1Score.ToString();
		
		StartCoroutine(ShowResults("ORANGE WINS!", orange));
		xa.audioManager.PlayWin();
	}

	void Team2Wins()
	{
		team2Score += 1;
		team2Wins.text = team2Score.ToString();
		
		StartCoroutine(ShowResults("BLUE WINS!", blue));
		xa.audioManager.PlayWin();
	}
	
	IEnumerator ShowResults(string results, Color col)
	{
		xa.gameOver = true;
		
		roundWinnerTxt.renderer.enabled = true;
		roundWinnerTxt.text = results.ToString();
		//roundWinnerTxt.color = col;
		roundWinnerTxt.tintColor = col;
		iTween.PunchScale(roundWinnerTxt.gameObject,iTween.Hash("amount",new Vector3(0.1f,0.1f,0.1f),"time",1f));
		
		yield return new WaitForSeconds(2f);
		
		roundNextTxt.renderer.enabled = true;
		
		nextRoundTimer = nextRoundTime;
		//roundNextTxt.scale = new Vector3(0.8f,0.8f,0.8f);
		while(nextRoundTimer > 0)
		{
			roundNextTxt.text = nextRoundTimer.ToString();
			nextRoundTimer -= 1;
			xa.audioManager.PlayBeep1();
			iTween.PunchScale(roundNextTxt.gameObject,iTween.Hash("amount",new Vector3(0.1f,0.1f,0.1f),"time",0.5f));
			yield return new WaitForSeconds(1f);
		}
	
		StartNextRound();
		roundWinnerTxt.renderer.enabled = false;
		
		iTween.PunchScale(roundNextTxt.gameObject,iTween.Hash("amount",new Vector3(0.15f,0.15f,0.15f),"time",0.75f));
		//roundNextTxt.scale = new Vector3(1,1,1);
		roundNextTxt.text = "GO!";
		xa.audioManager.PlayBeep2();
		yield return new WaitForSeconds(1f);
		
		roundNextTxt.renderer.enabled = false;
	}
	
	void ResetProgress()
	{
		team1Progress = 0;
		team1ProgressTxt.text = team1Progress.ToString("D3") + "%"; // leading zeroes!
		
		team2Progress = 0;
		team2ProgressTxt.text = team2Progress.ToString("D3") + "%"; // leading zeroes!
	}
	
	void StartNextRound()
	{
		xa.player1.Respawn();
		xa.player2.Respawn();
		
		// uncomment for 4 players
		//xa.player3.Respawn();
		//xa.player4.Respawn();
		
		xa.ball.ResetBall();
		ResetProgress();
		xa.gameOver = false;
		
		NextLevel();
 	}
	
	void NextLevel()
	{
		for(int i=0;i<level.Length;i+=1)
		{
			level[i].position = new Vector3(-22,0,0);
		}
		levelNum += 1;
		
		if(levelNum == level.Length)
		{
			levelNum = 0;
		}
		
		if(level.Length > 0)
		{
			print(level.Length+", "+levelNum);
			level[levelNum].position = new Vector3(0,0,0);
		}
	}
}
