// Walking.cs
// Author : Fragmads
// Package : Game engine/FighterStates
// Last modification date : 03/09/2012
//
// Walking : A state that describe a fighter walking
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class Walking : AFighterState {
	
	
	
	// Event
	
	void Update(){
		
		// TODO animation, vitesse d'animation
		
	}
	
	// Method
	//
	
	// Send the name of this state
	public override string getStateName() {
		return "Walking";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		// TODO attacking, guarding, jumping, dashing, standing
		
		XMomentum groundMomentum = this.gameObject.GetComponent<XMomentum>();
		
		// Walk		
		if(!input.RightStickDash){
			float wantedSpeed = input.RightStickX * fighter.WalkSpeed;
			
			// If the fighter has to change is facing
			if((fighter.isFacingLeft && wantedSpeed > 0 ) || (fighter.isFacingRight && wantedSpeed < 0)){
				
				fighter.TurnAround();
				
			}
						
			groundMomentum.strength = wantedSpeed;
			
			// TODO : include hysteresis if needed
			// Set state as standing, and destroy this state
			if(groundMomentum.strength == 0){
				
				Standing state = this.gameObject.AddComponent<Standing>();
				this.fighter.State = state;
				Object.Destroy(this);
				
			}
		}
		// Dash
		else {
			
			Dashing dashing = this.gameObject.AddComponent<Dashing>();
			this.fighter.State = dashing;
			Object.Destroy(this);
		}
		
		// Jumping
		if(input.CommandJump){
			
			Jumping jumping = this.gameObject.AddComponent<Jumping>();
			this.fighter.State = jumping;
			Object.Destroy(this);
			
		}
		
		// Guarding
		else if(input.CommandGuard){
			
			Guarding guarding = this.gameObject.AddComponent<Guarding>();
			this.fighter.State = guarding;
			Object.Destroy(this);
			
			XMomentum xMom = this.gameObject.GetComponent<XMomentum>();
			xMom.strength = 0;
			
		}
		
	}
	
	
}
