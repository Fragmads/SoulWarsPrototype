// Dashing.cs
// Author : Fragmads
// Package : Game engine/FighterStates
// Last modification date : 17/04/2013
//
// Dashing : A state that describe a fighter dashing
// 
// State : Uncomplete
using UnityEngine;
using System.Collections;

public class Dashing : AFighterState
{
	
	
	// Method
	//
	
	public new void Start ()
	{
		base.Start();
				
		
		// Set initial dash speed
		XMomentum xMom = this.gameObject.GetComponent<XMomentum>();
		
		if (fighter.isFacingLeft) {
			
			xMom.strength = -fighter.DashInitialSpeed;
			
		} else if (fighter.isFacingRight) {
			
			xMom.strength = fighter.DashInitialSpeed;
		}
		
	}
	
	// Send the name of this state
	public override string getStateName ()
	{
		return "Dashing";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input)
	{
		// TODO , dash attacking
		
		// Dashing
		
		XMomentum groundMomentum = this.gameObject.GetComponent<XMomentum>();
		
		float wantedAcceleration = input.RightStickX * fighter.DashAcceleration;
		
		// If the fighter dash dance
		if (input.RightStickDash && (Mathf.Abs (fighter.DashMaxSpeed) > Mathf.Abs (groundMomentum.strength)) && ((fighter.isFacingLeft && wantedAcceleration > 0) || (fighter.isFacingRight && wantedAcceleration < 0))) {
			
			fighter.TurnAround ();
			
			
			// Set initial dash speed after a direction change
			if (fighter.isFacingLeft) {
		
				groundMomentum.strength = -fighter.DashInitialSpeed;
		
			} else if (fighter.isFacingRight) {
		
				groundMomentum.strength = fighter.DashInitialSpeed;
			}
			
			
		}
		
		
		groundMomentum.strength = groundMomentum.strength + wantedAcceleration/60;
		
		// Cap the speed
		if(fighter.isFacingLeft && groundMomentum.strength < -fighter.DashMaxSpeed){
			
			groundMomentum.strength = -fighter.DashMaxSpeed;
			
		}
		else if (fighter.isFacingRight && groundMomentum.strength> fighter.DashMaxSpeed){
			
			groundMomentum.strength = fighter.DashMaxSpeed;
			
		}
		
		// TODO : include hysteresis if needed, include stop time, and a sliding momentum
		// Set state as standing, and destroy this state
		if (wantedAcceleration == 0) {
			
			Standing state = this.gameObject.AddComponent<Standing> ();
			fighter.State = state;
			Object.Destroy (this);
			
			Momentum momentum = this.gameObject.AddComponent<Momentum>();
			
			// Depend on the way you were running
			if(groundMomentum.strength < 0){
				momentum.angle = 180;
			}
			else {
				momentum.angle = 0;
			}
			
			momentum.strength = Mathf.Abs(groundMomentum.strength);
			// TODO find a friction coefficient
			momentum.reduction = fighter.Weight * 10;
			
			
			
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
			
			
			Momentum momentum = this.gameObject.AddComponent<Momentum>();
			
			// Depend on the way you were running
			if(groundMomentum.strength < 0){
				momentum.angle = 180;
			}
			else {
				momentum.angle = 0;
			}
			
			momentum.strength = Mathf.Abs(groundMomentum.strength);
			// TODO find a friction coefficient
			momentum.reduction = fighter.Weight * 10;
			
			
		}
		
		
	}	
	
}
