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
		
		Attacking attacking = this.gameObject.GetComponent<Attacking>();
		
		if (attacking != null && attacking.Attack is AerialAttack){
			
			this.LandingLag = ((AerialAttack)attacking.Attack).LandingLag;
			
			// TODO : Check if a LCancel was performed
			
		}
		
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
		
		this.LandingLag -= Time.deltaTime;
		
		if(this.LandingLag <= 0){
			
			this.fighter.State = this.gameObject.AddComponent<Standing>();
						
			Object.Destroy(this);
			
			
		}
		
	}
	
}
