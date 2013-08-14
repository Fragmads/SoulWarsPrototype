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
		this.fighter.SetAnimationSpeed("walking");
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
					
					// Stop this fighter to launch the attack					
					groundMomentum.strength = 0;
					
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
			int AttLevel = 1;
			
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
				
				if(!ga.isSpecial && ga.orientation == orientation && ga.AttackLevel == AttLevel){
					
					
					// Stop this fighter to launch the attack					
					groundMomentum.strength = 0;
					
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
		// If the right stick is used
		else if(input.RightStickX != 0 || input.RightStickY != 0 || input.R3){
			
			// Define the orientation of this attack
			Move.Orientation orientation = Move.Orientation.Neutral;	
			int AttLevel = 3;
			
			bool rightStickUsed = false;
			
			//Check the Right Stick shortcuts
			if(input.R3){				
				orientation = Move.Orientation.Neutral;	
				rightStickUsed = true;
			}
			else if(input.RightStickY > 0.8){				
				orientation = Move.Orientation.Up;	
				rightStickUsed = true;
			}
			else if(input.RightStickY < -0.8){				
				orientation = Move.Orientation.Down;	
				rightStickUsed = true;
			}
			else if( Mathf.Abs(input.RightStickX) > 0.8f ){
				
				// If the fighter is not facing the right direction
				if( (this.fighter.isFacingRight && input.RightStickX < 0) || (this.fighter.isFacingLeft && input.RightStickX > 0)){
					this.fighter.TurnAround();
				}
				
				orientation = Move.Orientation.Forward;
				rightStickUsed = true;
			}
			
			// If you really used the stick
			if(rightStickUsed){
				// Find the right move in the moveset
				foreach(Move ga in this.fighter.GroundMoveSet){
					
					if(ga is GroundAttack && !ga.isSpecial && ga.orientation == orientation && ((GroundAttack)ga).AttackLevel == AttLevel){
						
						// Stop this fighter to launch the attack					
						groundMomentum.strength = 0;
						
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
		
		
		// Dropping from the platform
		else if(input.LeftStickY < -0.8f && input.LeftStickDash && this.fighter.gameObject.GetComponent<OnGround>() != null){
			
			// Try to drop from the platform
			this.fighter.gameObject.GetComponent<OnGround>().DropPlatform();
			
		}
		
		
	}
	
	
}
