using UnityEngine;
using System.Collections;

public class Player1 : Character 
{	
	public Character[] players;
	
	// Use this for initialization
	public override void Start () 
	{
		base.Start();
		
		spawnPos = thisTransform.position;
	}
	
	// Update is called once per frame
	public void Update () 
	{
		// these are false unless one of keys is pressed
		isLeft = false;
		isRight = false;
		isJump = false;
		isPass = false;
		
		movingDir = moving.None;
		
		// keyboard input
		if(Input.GetKey(KeyCode.A)) 
		{ 
			isLeft = true; 
			facingDir = facing.Left;
		}
		if (Input.GetKey(KeyCode.D) && isLeft == false) 
		{ 
			isRight = true; 
			facingDir = facing.Right;
		}
		
		if (Input.GetKeyDown(KeyCode.W)) 
		{ 
			isJump = true; 
		}
		
		if(Input.GetKeyDown(KeyCode.S))
		{
			isPass = true;
		}
		
		if(Input.GetKeyDown(KeyCode.R))
		{
			ResetBall();
		}
		
		if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.T))
		{
			//print ("reload level");
			Application.LoadLevel(0);
		}
		
		if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			players[0].HideMe();
			players[1].HideMe();
		}
		
		if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			players[0].ShowMe();
			players[1].HideMe();
		}
		if(Input.GetKeyDown(KeyCode.Alpha4))
		{
			players[0].ShowMe();
			players[1].ShowMe();
		}
		
		UpdateMovement();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Ball"))
		{
			PickUpBall();
		}
	}
	
	public void Respawn()
	{
		if(alive == true)
		{
			thisTransform.position = spawnPos;
			hasBall = false;
			rayDistUp = 0.375f;
		}
	}
}
