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
	
	// Platform the fighter is on
	public Platform platform;
	
	// Method
	//
	
	public new void Start ()
	{
		base.Start();
		
		// Set initial dash speed
		if (fighter.isFacingLeft) {
			
			fighter.SpeedX = -fighter.DashInitialSpeed;
			
		} else if (fighter.isFacingRight) {
			
			fighter.SpeedX = fighter.DashInitialSpeed;
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
			state.platform = this.platform;
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
		if(input.Jump){
			
			Jumping jumping = this.gameObject.AddComponent<Jumping>();
			this.fighter.State = jumping;
			Object.Destroy(this);
			
		}
		
	}	
	
}
