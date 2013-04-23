// Teching.cs
// Author : Fragmads
// Package : Game engine/FighterStates
// Last modification date : 03/09/2012
//
// Teching : A state that describe a fighter teching
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class Teching : AFighterState {
	
	private static float invincibleTimeWallTech = 0.2f;
	
	private static float invincibleTimeTech = 0.3f;
	
	private bool firstCheck = true;
	
	// Platform the fighter is on
	public Platform platform;
	
	// Method
	//
	

	new void Start(){
		
		base.Start();
		
		// if it's a Wall tech
		if(this.GetComponent<Airborne>() != null){
			
			// Set Speed to 0
			this.fighter.SpeedX = 0;
			this.fighter.SpeedY = 0;
			
		}
		
		
	}
	
	// Send the name of this state
	public override string getStateName() {
		return "Teching";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		// TODO walltech jumping, rolling
		
		// Only once during the Tech life time
		if(this.firstCheck){
			
			// If the fighter his in the air, it's a wall tech
			if(this.gameObject.GetComponent<Airborne>()){
				
				this.fighter.InvincibilityTime = Teching.invincibleTimeWallTech;
				
				
			}
			// Else it's a Tech
			else {
				
				this.fighter.InvincibilityTime = Teching.invincibleTimeTech;
				
				//If the player roll to the right
				if(input.RightStickX > 0.8){
					
					// TODO perform a right dodge
										
				}
				// If the player roll to the left
				else if(input.RightStickX < -0.8){
					
					// TODO perform a left dodge
					
				}
				// If he doesn't roll
				else{					
					
					// TODO perform a stand tech
					
				}
				
				
			}
			
			this.firstCheck = false;
			
		}
			
	}	
	
	
	void FixedUpdate(){
		
		// If the tech is over
		if(this.fighter.InvincibilityTime == 0){
			
			// If the fighter is teching on the ground
			if(this.gameObject.GetComponent<Airborne>() == null){
				
				// He stand at the end of the tech
				this.fighter.State = this.gameObject.AddComponent<Standing>();
				
				Object.Destroy(this);
				
			}
			// If he is wall teching
			else{
				// He is still in the air afterward
				this.fighter.State = this.GetComponent<Airborne>();
				
			}
			
		}
		
	}
	
}
