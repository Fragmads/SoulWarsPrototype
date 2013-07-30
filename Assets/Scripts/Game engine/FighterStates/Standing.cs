// Standing.cs
// Author : Fragmads
// Package : Game engine/FighterStates
// Last modification date : 03/09/2012
//
// Standing : A state describing a fighter standing still
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class Standing : AFighterState {
	
	
	// Method
	//
	
	
	public new void Start () {
		
		base.Start();
		
		// Stop the speed when you are walking
		XMomentum groundMomentum = this.gameObject.GetComponent<XMomentum>();
		
		if(groundMomentum != null){
			groundMomentum.strength = 0;
		}
		
		
		// Start the standing animation
		this.gameObject.animation.Play("standing", PlayMode.StopAll);
		
		
		
	}
	
	
	// Send the name of this state
	public override string getStateName() {
		return "Standing";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		// TODO run, jump, guard, attack, walking, Dashing
		// TODO reorganize the if else if chain, to a more logical order
				
		// Walking
		if (!input.LeftStickDash && input.LeftStickX != 0){
			
			Walking walking = this.gameObject.AddComponent<Walking>();
			this.fighter.State = walking;
			Object.Destroy(this);
			
		}
		// Dashing
		else if(input.LeftStickDash && input.LeftStickX != 0){
			
			// Make the fighter face the good direction
			if( (this.fighter.isFacingLeft && input.LeftStickX > 0) || (this.fighter.isFacingRight && input.LeftStickX < 0)){
				
				this.fighter.TurnAround();
				
			}
			
			Dashing dashing = this.gameObject.AddComponent<Dashing>();
			this.fighter.State = dashing;
			Object.Destroy(this);
			
		}
				
		// Jumping
		else if(input.CommandJump){
			
			Jumping jumping = this.gameObject.AddComponent<Jumping>();
			this.fighter.State = jumping;
			Object.Destroy(this);
			
		}
		
		// Guarding
		else if(input.CommandGuard){
			
			Guarding guarding = this.gameObject.AddComponent<Guarding>();
			this.fighter.State = guarding;
			Object.Destroy(this);
			
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
					Object.Destroy(this);
					
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
			foreach(Move ga in this.fighter.GroundMoveSet){
				
				if(ga is GroundAttack && !ga.isSpecial && ga.orientation == orientation && ((GroundAttack)ga).AttackLevel == AttLevel){
					
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
	
	public void FixedUpdate(){
		
		// When standing, a fighter Horizontal speed tend to quickly reduce to 0
		if(this.fighter.SpeedX != 0){
			this.fighter.SpeedX = this.fighter.SpeedX/(this.fighter.GroundFriction);
			
			// If the speed is low enough, consider it's 0
			if(Mathf.Abs(this.fighter.SpeedX) < 1){
				this.fighter.SpeedX = 0;
			} 
			
		}
		
	}
	
}
