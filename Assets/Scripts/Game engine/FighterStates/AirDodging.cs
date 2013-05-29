// AirDodging.cs
// Author : Fragmads
// Package : Game engine/FighterStates
// Last modification date : 29/05/2013
//
// AirDodging : A state describing a fighter performing an air dodge
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class AirDodging : AFighterState {

	// Properties
	//
	
	// Direction of the AirDodge
	public float DirectionX = 0;
	public float DirectionY = 0;
	
	// Length of the air dodge in second
	public float length = 1f;
	
	// Start and end of the invicibility time
	public float StartInvincibilityTime = 0.2f;
	public float EndInvincibilityTime = 0.8f;
	
	
	// Method
	//
	
	private float angle;
	private float strength;
	
	private float timeSinceStart = 0f;
	
	public new void Start(){
		
		base.Start();
		
		// Calculate the angle and strength of this air dodge
		Vector2 point = new Vector2(this.DirectionX, this.DirectionY);
		this.angle = Vector2.Angle(Vector2.right, point);
		
		// Vector2.Angle only tell from 0 to 180 angle ...
		if(this.DirectionY < 0){
			
			this.angle = 360 - this.angle;
			
		}
		
		// Cap the strength of the vector to 1
		this.strength = Mathf.Min(Vector2.Distance(Vector2.zero, point), 1f) * this.fighter.AirDodgeStrength;
		
		// Set up the air dodge
		this.length = this.fighter.AirDodgeLength;
		this.EndInvincibilityTime = this.fighter.AirDodgeEnd;
		this.StartInvincibilityTime = this.fighter.AirDodgeStart;
		
		// Clean all previous momentum
		Momentum.Clean(this.fighter);
		
		if(this.fighter.gameObject.GetComponent<XMomentum>() != null){			
			this.fighter.gameObject.GetComponent<XMomentum>().strength = 0;			
		}
		
		Debug.Log("AirDodge.Start - Angle : "+this.angle);
		
	}
	
	// Send the name of this state
	public override string getStateName() {
		return "AirDodging";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		
		// TODO Nothing you can do
		
	}
	
	public void FixedUpdate(){
		
		
		this.timeSinceStart += Time.fixedDeltaTime;
		
		// If this AirDodge End
		if(this.timeSinceStart >= this.length){
			
			// End this AirDodge
			UselessStance uselessStance = this.fighter.gameObject.AddComponent<UselessStance>();
			this.fighter.State = uselessStance;
			
			GameObject.Destroy(this);
			
		}
		
		// Activate the invicibility
		if(this.timeSinceStart < this.StartInvincibilityTime && this.fighter.InvincibilityTime <=0 && this.timeSinceStart < this.EndInvincibilityTime){
			
			this.fighter.InvincibilityTime = this.EndInvincibilityTime - this.timeSinceStart;
			
		}
		
		// Deactivate the invicibility
		if(this.timeSinceStart > this.EndInvincibilityTime){
			this.fighter.InvincibilityTime = 0;
		}
		
		
		// Move the fighter in the direction
		
		
		Vector3 newPos = new Vector3(this.fighter.gameObject.transform.position.x + ((Mathf.Cos(this.angle* Mathf.Deg2Rad)*this.strength)/60) , this.fighter.gameObject.transform.position.y + ((Mathf.Sin(this.angle * Mathf.Deg2Rad)*this.strength)/60),0);
		this.fighter.gameObject.transform.position = newPos;
		
		
	}
	
}
