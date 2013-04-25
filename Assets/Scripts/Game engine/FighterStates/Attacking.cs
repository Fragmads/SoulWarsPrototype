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
		
		// TODO start the attack
		
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
			
			// If the player want to go to the next level of attack
			if(input.CommandAttack){
				
			}
			
			// If the player want to repeat/spam this level of attack
			if(this.Attack.isEnded && input.Attack){
				
				if(input.Attack){
					
					// TODO redo the attack
					
				}
				
			} 
		}
		
		if(this.Attack.isEnded){
			
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
		
	}	
	
	
}
