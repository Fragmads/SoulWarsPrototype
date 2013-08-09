// Rolling.cs
// Author : Fragmads
// Package : Game engine/FighterStates
// Last modification date : 31/05/2013
//
// Rolling : A state that describe a fighter performing a roll
// 
// State : Uncomplete


using UnityEngine;
using System.Collections;

public class Rolling : AFighterState {
	
	// Properties
	//
	
	public float RollLength = 1f;
	public float RollSpeed = 2f;
	public float RollInvincibilityStart = 0.3f;
	public float RollInvincibilityEnd = 0.8f;
	
	// Side of the roll
	public bool isRight = false;
	public bool isLeft = false;
	
	private float time = 0f;
	
	// Method
	//
	
	// Use this for initialization
	public new void Start () {
		
		base.Start();
		
		this.time = 0;
		
		// Set up the roll
		this.RollLength = this.fighter.RollLength;
		this.RollSpeed = this.fighter.RollSpeed;
		this.RollInvincibilityStart = this.fighter.RollInvincibilityStart;
		this.RollInvincibilityEnd = this.fighter.RollInvincibilityEnd;
		
		// If this is a FRoll
		if( (this.fighter.isFacingLeft && this.isLeft) ||(this.fighter.isFacingRight && this.isRight) ){
			
			// play the forward roll animation
			this.fighter.SetAnimationSpeed("FRoll");
			this.gameObject.animation.Play("FRoll", PlayMode.StopAll);
			
		}
		else{
			
			// play the backward roll animation
			this.fighter.SetAnimationSpeed("BRoll");
			this.gameObject.animation.Play("BRoll", PlayMode.StopAll);
			
		}
		
	}
	
	// Send the name of this state
	public override string getStateName() {
		return "Rolling";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		
		// Nothing to do here
		
	}
	
	public void FixedUpdate(){
		
		this.time += Time.fixedDeltaTime;
		
		if(this.time >= this.RollLength){
			
			// End roll and start guarding
			this.fighter.InvincibilityTime = 0;
			Guarding guarding = this.gameObject.AddComponent<Guarding>();
			this.fighter.State = guarding;
			GameObject.Destroy(this);
			
		}
		
		// Start of the invicibility of the roll
		if(this.time > this.RollInvincibilityStart && this.fighter.InvincibilityTime <=0 && this.time < this.RollInvincibilityEnd){
			
			this.fighter.InvincibilityTime = this.RollInvincibilityEnd - this.time;
			
		}
		
		// End of the invincibility of the roll
		if(this.time > this.RollInvincibilityEnd){
			this.fighter.InvincibilityTime = 0;
		}
		
		
		// Move during the roll
		
		// Right roll
		if(this.isRight){
			
			float fighterWantedPos = this.fighter.gameObject.transform.position.x + this.RollSpeed/60;
			float platformEnd = this.fighter.gameObject.GetComponent<OnGround>().platform.gameObject.transform.position.x + (this.fighter.gameObject.GetComponent<OnGround>().platform.Length/2);
			
			// If the roll has reached the end of the platform
			if(fighterWantedPos < platformEnd){
				// Move the fighter
				this.fighter.gameObject.transform.position = new Vector3(fighterWantedPos, this.fighter.gameObject.transform.position.y, 0);
				
			}
		}
		// Left roll
		else if(this.isLeft){
			
			float fighterWantedPos = this.fighter.gameObject.transform.position.x - this.RollSpeed/60;
			float platformEnd = this.fighter.gameObject.GetComponent<OnGround>().platform.gameObject.transform.position.x - (this.fighter.gameObject.GetComponent<OnGround>().platform.Length/2);
			
			// If the roll has reached the end of the platform
			if(fighterWantedPos > platformEnd){
				// Move the fighter
				this.fighter.gameObject.transform.position = new Vector3(fighterWantedPos, this.fighter.gameObject.transform.position.y, 0);
				
			}
			
		}
	}
	
	
}
