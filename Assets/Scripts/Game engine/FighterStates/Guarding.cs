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
		
		// No input if you are in Guard Lag
		if(this.GuardLag <= 0){
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
		
		// Reduce Guard lag if there is
		if(this.GuardLag > 0){
			
			this.GuardLag -= Time.fixedDeltaTime;
			
			if(this.GuardLag <= 0){
				this.GuardLag = 0;
			}
			
		}
		
	}
	
	public void GuardBreak(){
		
		Debug.Log("Guarding GuardBreak");
		this.fighter.GuardLife = this.fighter.GuardInitLife/2;
		
	}
	
	public void GuardHit (Attack attack){
		
				
		// Reduce the guard
		this.fighter.GuardLife -= attack.Damage;
		
		this.GuardLag += attack.Damage/60f;
		Debug.Log("Guarding GuardHit - GuardLag "+this.GuardLag);
		
		// If you are not facing the attacker
		if( (this.fighter.isFacingLeft && attack.Owner.gameObject.transform.position.x > this.fighter.gameObject.transform.position.x) ||
			(this.fighter.isFacingRight && attack.Owner.gameObject.transform.position.x < this.fighter.gameObject.transform.position.x)){
			
			// You turn around
			this.fighter.TurnAround();
			
		}
		
		// Take reduced damage		
		this.fighter.CurrentHp -= Mathf.FloorToInt((attack.Damage * this.fighter.GuardLife)/ this.fighter.GuardInitLife);
		
		// If the guard break
		if(this.fighter.GuardLife <=0){
			
			this.GuardBreak();
			
		}
		
		// TODO add guard DI
		
		// Add a momentum to the fighter guarding
		Momentum m = this.fighter.gameObject.AddComponent<Momentum>();
		
		// Set the strength and reduction
		m.strength = attack.BaseKnockBack.strength / 3f;
		m.reduction = this.fighter.Weight;
		
		// the angle depend on which side he is looking (he has already turn around if needed)
		if(this.fighter.isFacingLeft){
			m.angle = 0;
		}
		else {
			m.angle = 180;
		}
				
		
	}
	
}
