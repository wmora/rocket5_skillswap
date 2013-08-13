using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	public ParticleSystem particleVFX;
	
	private Transform thisTransform;
	private Rigidbody thisRigidbody;
	
	private string team;
	
	protected float fireRate = 0.05f;//0.1F;
    protected float nextFire = 0.0F;
	
	private bool hasBall = false;
	private bool scoringPoints = false;
	
	private Color orange = new Color(0.94f,0.59f,0f);
	private Color blue = new Color(0f,0.69f,94f);
	private Color green = new Color(0.76f,1f,0f);
	
	void Awake()
	{
		thisTransform = transform;
		thisRigidbody = rigidbody;
	}
	
	// Use this for initialization
	IEnumerator Start () 
	{
		yield return new WaitForSeconds(0.1f);
		ResetBall();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// screen wrap
		if(thisTransform.position.x > 8.35f && hasBall == false)
		{
			thisTransform.position = new Vector3(-8.35f,thisTransform.position.y, 0);
		}
		if(thisTransform.position.x < -8.35f && hasBall == false)
		{
			thisTransform.position = new Vector3(8.35f,thisTransform.position.y, 0);
		}
		
		if(hasBall == true && scoringPoints == false)
		{
			DecreaseScore();
		}
	}
	
	public void PickUp(Transform trans, string tm)
	{
		team = tm;
		thisRigidbody.isKinematic = true;
		thisRigidbody.useGravity = false;
		thisTransform.position = new Vector3(trans.position.x, trans.position.y + 0.65f, trans.position.z);
		thisTransform.parent = trans;
		xa.audioManager.PlayPickup();
		hasBall = true;
		scoringPoints = false;
		
		if(tm == "Team2")
		{
			particleVFX.startColor = blue;
		}
		else
		{
			particleVFX.startColor = orange;
		}
	}
	
	public void ResetBall()
	{
		thisTransform.parent = null;
		thisRigidbody.isKinematic = false;
		thisRigidbody.useGravity = true;
		thisTransform.position = new Vector3(0,3.5f,0);
		team = "None";
		hasBall = false;
		scoringPoints = false;
		particleVFX.startColor = green;
		
		//rigidbody.velocity = new Vector3(Random.Range(-2,2), 3.5f, 0); // randomize ball position
	}
	
	public void Pass(float velX)
	{
		//print ("pass ball"); 
		thisTransform.parent = null;
		thisRigidbody.isKinematic = false;
		thisRigidbody.useGravity = true;
		rigidbody.velocity = new Vector3(velX, 10, 0);
		team = "None";
		xa.audioManager.PlayPass();
		hasBall = false;
		particleVFX.startColor = green;
	}
	
	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.layer == xa.Team1Goal && xa.gameOver == false)
		{
			if(team == "Team1")
			{
				IncreaseScore();
			}
		}
		
		if(other.gameObject.layer == xa.Team2Goal && xa.gameOver == false)
		{
			if(team == "Team2")
			{
				IncreaseScore();	
			}
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.layer == xa.Team1Goal && xa.gameOver == false)
		{
			if(team == "Team1")
			{
				scoringPoints = false;
			}
		}
		
		if(other.gameObject.layer == xa.Team2Goal && xa.gameOver == false)
		{
			if(team == "Team2")
			{
				scoringPoints = false;	
			}
		}
	}
	
	void IncreaseScore()
	{
		xa.scoreManager.IncreaseScore(team);
		scoringPoints = true;
	}
	
	void DecreaseScore()
	{
		if(xa.letsPlayKeepaway == false)
		{
			xa.scoreManager.DecreaseScore(team);
		}
	}
}
