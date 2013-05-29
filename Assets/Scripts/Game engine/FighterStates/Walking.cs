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
	//
	
	public new void Start(){
		
		base.Start ();
		
		// play the walking animation
		this.gameObject.animation.Play("walking", PlayMode.StopAll);
		
	}
	
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
		if(!input.LeftStickDash){
			float wantedSpeed = input.LeftStickX * fighter.WalkSpeed;
			
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
				GameObject.Destroy(this);
				
			}
		}
		// Dash
		else {
			
			Dashing dashing = this.gameObject.AddComponent<Dashing>();
			this.fighter.State = dashing;
			GameObject.Destroy(this);
		}
		
		// Jumping
		if(input.CommandJump){
			
			Jumping jumping = this.gameObject.AddComponent<Jumping>();
			this.fighter.State = jumping;
			GameObject.Destroy(this);
			
		}
		
		// Guarding
		else if(input.CommandGuard){
			
			Guarding guarding = this.gameObject.AddComponent<Guarding>();
			this.fighter.State = guarding;
			GameObject.Destroy(this);
			
			XMomentum xMom = this.gameObject.GetComponent<XMomentum>();
			xMom.strength = 0;
			
		}
		
		// Special Move
		else if(input.CommandSpecial){
			
			// Define the orientation of this special move
			Move.Orientation orientation;
			
			if(input.LeftStickY > 0.8){
				orientation = Move.Orientation.Up;
			}
			else if(input.LeftStickY < -0.8){
				orientation = Move.Orientation.Down;
			}
			else if(input.LeftStickX > 0.8 || input.LeftStickX < -0.8 ){
				orientation = Move.Orientation.Forward;
			}
			else{
				orientation = Move.Orientation.Neutral;
			}
			
			// Find the right move in the moveset
			foreach(GroundAttack ga in this.fighter.GroundMoveSet){
				
				if(ga.isSpecial && ga.orientation == orientation){
					
					// Start the special
					Attacking attacking = this.gameObject.AddComponent<Attacking>();
					this.fighter.State = attacking;
					attacking.Attack = ga;
					GameObject.Destroy(this);
					
					break;
					
				}
				
			}			
			
		}
		
		// Attack move
		else if(input.CommandAttack){
			
			// Define the orientation of this attack
			Move.Orientation orientation;
			
			if(input.LeftStickY > 0.8){
				orientation = Move.Orientation.Up;
			}
			else if(input.LeftStickY < -0.8){
				orientation = Move.Orientation.Down;
			}
			else if(input.LeftStickX > 0.8 || input.LeftStickX < -0.8 ){
				orientation = Move.Orientation.Forward;
			}
			else{
				orientation = Move.Orientation.Neutral;
			}
			
			// Find the right move in the moveset
			foreach(GroundAttack ga in this.fighter.GroundMoveSet){
				
				if(!ga.isSpecial && ga.orientation == orientation && ga.AttackLevel == 1){
					
					// Start the attack
					Attacking attacking = this.gameObject.AddComponent<Attacking>();
					this.fighter.State = attacking;
					attacking.Attack = ga;
					attacking.AttackLevel = 1;
					GameObject.Destroy(this);
					
					break;
					
				}
				
			}
			
		}
		
		
	}
	
	
}
