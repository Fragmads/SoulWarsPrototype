// Attacking.cs
// Author : Fragmads
// Package : Game engine/FighterStates
// Last modification date : 03/09/2012
//
// Attacking : A state describing a fighter attacking
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class Attacking : AFighterState {

	// Properties
	//
	
	public Move Attack;
	
	// The number of attack (Only available for ground attack, possible value : 0, 1, 2, 3)
	public int AttackLevel = 0;
		
	
	// Method
	//
	
	public new void Start(){
		
		base.Start();
		
		
		// Play the animation
		this.gameObject.animation.Play(this.Attack.AnimationName, PlayMode.StopAll);
		
		// Instantiate a game object that will represent the attack
		GameObject go = (GameObject) GameObject.Instantiate(this.Attack.Prefab);
		
		// This temp object is now the attack
		this.Attack = go.GetComponent<Move>();
		this.Attack.Owner = this.fighter;
		
	}
	
	// Send the name of this state
	public override string getStateName() {
		return "Attacking";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		
		// TODO go to next attack, Air control
				
		// If this attack is not a special and is performed on the ground
		if(!this.Attack.isSpecial && this.Attack is GroundAttack){
			
			
			// If the player want to repeat/spam this level of attack
			if(this.Attack.isEnded && input.Attack && this.AttackLevel < 3){
				
				// Destroy the dummy for the past attack
				Object.Destroy(this.Attack.gameObject);
				
				// Launch an attack
				this.LaunchAttack(input);			
				
			}
			
			// If the player want to go to the next level of attack
			if(input.CommandAttack){
				
				this.AttackLevel++;
				
				// Cap the attack level at 3
				if(this.AttackLevel <4){
				
					// Destroy the dummy for the past attack
					this.Attack.EndMove();
					GameObject.Destroy(this.Attack.gameObject);
					
					// Launch an attack
					this.LaunchAttack(input);
				}
				else{
					this.AttackLevel = 3;
				}
				
			}
						 
		}
		
		if(this.Attack.isEnded && this.Attack != null){
			
			this.Attack.EndMove();
			// Destroy the dummy for the attack			
			Object.Destroy(this.Attack.gameObject);
			
			if(this.gameObject.GetComponent<OnGround>() != null){
				
				// TODO stand
				this.fighter.State = this.gameObject.AddComponent<Standing>();
				Object.Destroy(this);
				
			}
			
			if(this.gameObject.GetComponent<Airborne>() != null){
				
				// TODO airborne				
				this.fighter.State = this.gameObject.GetComponent<Airborne>();
				Object.Destroy(this);				
				
			}
			
		}
		
		// If the fighter is airborne
		if(this.gameObject.GetComponent<Airborne>() != null){
			// Check the air control of the fighter	
			this.gameObject.GetComponent<Airborne>().AirControl(input);
			
		}
		
	}	
	
	private void LaunchAttack(InputCommand input){
		
		// Define the orientation of this attack
		Move.Orientation orientation;
		
		if(input.RightStickY > 0.8){
			orientation = Move.Orientation.Up;
		}
		else if(input.RightStickY < -0.8){
			orientation = Move.Orientation.Down;
		}
		else if(input.RightStickX > 0.8 || input.RightStickX < -0.8 ){
			orientation = Move.Orientation.Forward;
		}
		else{
			orientation = Move.Orientation.Neutral;
		}
	
		// Find the right move in the moveset
		foreach(GroundAttack ga in this.fighter.GroundMoveSet){
			
			if(!ga.isSpecial && ga.orientation == orientation && ga.AttackLevel == this.AttackLevel){
				
				
				
				// Instantiate a game object that will represent the attack
				GameObject go = (GameObject) GameObject.Instantiate(ga.Prefab);
				
				// This temp object is now the attack
				this.Attack = go.GetComponent<Move>();
				this.Attack.Owner = this.fighter;
				
				// Play the animation
				this.gameObject.animation.Play(this.Attack.AnimationName, PlayMode.StopAll);
				
				break;
				
			}
			
		}
		
		
	}
	
	
}
