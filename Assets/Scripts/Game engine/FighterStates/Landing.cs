// Landing.cs
// Author : Fragmads
// Package : Game engine/FighterStates
// Last modification date : 03/09/2012
//
// Landing : A state that describe a fighter landing
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class Landing : AFighterState {
	
	public Platform platform;
	
	public float LandingLag = 0f;
	
	// Method
	//
	
	new void Start(){
		
		base.Start();
		
		// Check if the fighter landing was attacking
		Attacking attacking = this.gameObject.GetComponent<Attacking>();
		
		if (attacking != null && attacking.Attack is AerialAttack){
			
			this.LandingLag = ((AerialAttack)attacking.Attack).LandingLag;
			
			// Stop the aerial attack
			attacking.StopAttacking();
			
			GameObject.Destroy(attacking);
			// TODO : Check if a LCancel was performed
			
			
			
		}
		
		this.gameObject.GetComponent<XMomentum>().strength = 0;
		
		
		// play the landing animation
		this.gameObject.animation.Play("landing", PlayMode.StopAll);
		
		
	}
	
	// Send the name of this state
	public override string getStateName() {
		return "Landing";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		// Nothing to do
			
	}	
	
	
	void FixedUpdate(){
		
		// Calculate landing lag time
		this.LandingLag -= Time.deltaTime;
		
		// If the lag time is over
		if(this.LandingLag <= 0){
			// Fighter is Standing
			this.fighter.State = this.gameObject.AddComponent<Standing>();
						
			Object.Destroy(this);			
			
		}
		
		
		// When landing, a fighter Horizontal speed tend to quickly reduce to 0
		if(this.fighter.SpeedX != 0){
			this.fighter.SpeedX = this.fighter.SpeedX/(this.fighter.GroundFriction);
			
			// If the speed is low enough, consider it's 0
			if(Mathf.Abs(this.fighter.SpeedX) < 1){
				this.fighter.SpeedX = 0;
			} 
			
		}
		
	}
	
}
