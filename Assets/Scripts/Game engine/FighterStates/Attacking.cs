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
	
	// The number of the attack (Only available for ground attack, possible value : 0, 1, 2, 3)
	public int AttackLevel = 0;
		
	public bool BufferNextLevelAttack = false;
	
	private int currentFrame = 0;
	
	// Method
	//
	
	public new void Start(){
		
		base.Start();
		
		
		// Play the animation
		this.fighter.SetAnimationSpeed(this.Attack.AnimationName);	
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
					
		this.currentFrame ++;
		
		// If this attack is not a special and is performed on the ground
		if(!this.Attack.isSpecial && this.Attack is GroundAttack){
						
			// If the player want to repeat/spam this level of attack
			if(this.Attack.isEnded && input.Attack && this.AttackLevel < 3){
				
				// Destroy the dummy for the past attack
			//	GameObject.Destroy(this.Attack.gameObject);
				
				// Launch an attack
			//	this.LaunchAttack(input);			
				
			}
			
			// If the player want to go to the next level of attack
			if(input.CommandAttack){
				
				// If the attack is just starting, go right away on the next level 
				if(((GroundAttack)this.Attack).NextAttackBuffering > this.currentFrame ){
				
					Debug.Log("Attacking.readCommand - Skip to next level");
					
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
				else {
					// End the attack before going to the next level
					this.BufferNextLevelAttack = true;
					
					Debug.Log("Attacking.readCommand - Buffer next Attack BufferingFrame "+((GroundAttack)this.Attack).NextAttackBuffering+" current Frame "+ this.currentFrame);
					
				}
			}
						 
		}
		
		if(this.Attack.isEnded && this.Attack != null){
			
			// Destroy the dummy for the attack			
			this.StopAttacking();
			
			if(this.gameObject.GetComponent<OnGround>() != null){
				
				// If the player want to go to the next attack
				if(this.BufferNextLevelAttack && this.AttackLevel <3){
					
					this.AttackLevel++;
					
					// Cap the attack level at 3
					
					// Destroy the dummy for the past attack
					this.Attack.EndMove();
					GameObject.Destroy(this.Attack.gameObject);
					
					// Launch an attack
					this.LaunchAttack(input);
					
					this.BufferNextLevelAttack = false;
					this.currentFrame = 0;
					
				}
				else{
					this.fighter.State = this.gameObject.AddComponent<Standing>();
					GameObject.Destroy(this);
				}
				
			}
			
			if(this.gameObject.GetComponent<Airborne>() != null){
								
				this.fighter.State = this.gameObject.GetComponent<Airborne>();
				GameObject.Destroy(this);				
				
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
	
	
	public void StopAttacking(){
		
		// Destroy the dummy for the past attack
		if(this.Attack != null && !this.Attack.Equals(this.Attack.Prefab)){
			this.Attack.EndMove();
			GameObject.Destroy(this.Attack.gameObject);
		}
		
	}
	
	
}
