using UnityEngine;
using System.Collections;

public class Player3 : Character 
{	
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
		//isScoring = false;
		
		movingDir = moving.None;
		
		// keyboard input
		if(Input.GetKey(KeyCode.V)) 
		{ 
			isLeft = true; 
			facingDir = facing.Left;
		}
		if (Input.GetKey(KeyCode.N) && isLeft == false) 
		{ 
			isRight = true; 
			facingDir = facing.Right;
		}
		
		if (Input.GetKeyDown(KeyCode.G)) 
		{ 
			isJump = true; 
		}
		
		if(Input.GetKeyDown(KeyCode.B))
		{
			isPass = true;
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
