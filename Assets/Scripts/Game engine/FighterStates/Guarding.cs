// Guarding.cs
// Author : Fragmads
// Package : Game engine/FighterStates
// Last modification date : 03/09/2012
//
// Move : A state describing a fighter who have raised his guard
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class Guarding : AFighterState {

	// Method
	//
	
	public float GuardLag = 0;
	
	
	public new void Start(){
		
		base.Start();
		
		// Play the guard animation
		this.gameObject.animation.Play("guard", PlayMode.StopAll);	
		
	}
	
	// Send the name of this state
	public override string getStateName() {
		return "Guarding";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		// TODO can grab, jump, dodge, or go back to standing
		
		// Guard to stand
		if(!input.Guard){
			
			// He stand without guard
			this.fighter.State = this.gameObject.AddComponent<Standing>();
			
			Object.Destroy(this);
			
		}
		
		if(input.CommandJump){
			
			Jumping jumping = this.gameObject.AddComponent<Jumping>();
			this.fighter.State = jumping;
			Object.Destroy(this);
			
		}
		
	}	
	
	
	public void FixedUpdate(){
		
		// When guarding, a fighter Horizontal speed tend to quickly reduce to 0
		if(this.fighter.SpeedX != 0){
			this.fighter.SpeedX = this.fighter.SpeedX/(this.fighter.GroundFriction);
			
			// If the speed is low enough, consider it's 0
			if(Mathf.Abs(this.fighter.SpeedX) < 1){
				this.fighter.SpeedX = 0;
			} 
			
		}
		
	}
	
	public void GuardBreak(){
		
		this.fighter.GuardLife = this.fighter.GuardInitLife/2;
		
	}
	
	public void GuardHit (Attack attack){
		
		// Reduce the guard
		this.fighter.GuardLife -= attack.Damage;
		
		// Take reduced damage		
		this.fighter.CurrentHp -= Mathf.FloorToInt((attack.Damage * this.fighter.GuardLife)/ this.fighter.GuardInitLife);
		
		// If the guard break
		if(this.fighter.GuardLife <=0){
			
			this.GuardBreak();
			
		}
		
		// TODO add guard DI
		
		
	}
	
}
