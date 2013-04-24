// Jumping.cs
// Author : Fragmads
// Package : Game engine/FighterStates
// Last modification date : 03/09/2012
//
// Jumping : A state that describe a fighter on the land, proceding to jump
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class Jumping : AFighterState {

	// Method
	//
	
	private float JumpLag;
	
	private bool ShortHop =  false;
	
	private float RStickX;
	
	public new void Start(){
		
		base.Start();
		
		this.JumpLag = this.fighter.JumpLag;
		
	}
	
	
	// Send the name of this state
	public override string getStateName() {
		return "Jumping";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		// TODO calculate if it's a full jump or a short hop
		
		// If you released the jump button before the end, you will perform a short jump.
		if(!input.Jump){
			
			this.ShortHop = true;
			
		}
		
		this.RStickX = input.RightStickX;
		
		
	}	
	
	public void FixedUpdate(){
		
		this.JumpLag -= Time.fixedDeltaTime;
		
		// If the fighter finished to jump 
		if(this.JumpLag <= 0){
			
			OnGround onGround = this.gameObject.GetComponent<OnGround>();
			// He is now in the air
			if(onGround != null){
				onGround.GoAirborne();				
			}
			
			// If it's a short hop
			if(this.ShortHop){
				
				Momentum m = this.gameObject.AddComponent<JumpMomentum>();
				m.strength = this.fighter.ShortJumpStrength;
				m.reduction = this.fighter.ShortJumpReduction;
				m.angle = 90;				
				
			}
			// If it's a full hop
			else {
				
				Momentum m = this.gameObject.AddComponent<JumpMomentum>();
				m.strength = this.fighter.FullJumpStrength;
				m.reduction = this.fighter.ShortJumpReduction;
				m.angle = 90;
				
			}
			
			// Horizontal Speed
			
			// If it's the forward speed
			if( (this.fighter.isFacingLeft && (this.RStickX > 0)) || (this.fighter.isFacingRight && (this.RStickX < 0))){
				
				// TODO put the XMomentum here
				Momentum m = this.gameObject.GetComponent<XMomentum>();
				m.strength = this.fighter.JumpForwardHorizontalFactor * this.RStickX;
				m.reduction = 0;
				
				
			}
			// If it's the back speed
			else if((this.fighter.isFacingLeft && (this.RStickX < 0)) || (this.fighter.isFacingRight && (this.RStickX > 0))){
				
				// TODO put the XMomentum here				
				Momentum m = this.gameObject.GetComponent<XMomentum>();
				m.strength = this.fighter.JumpBackHorizontalFactor * this.RStickX;
				m.reduction = 0;
				
			}
			
			// End the jump			
			Object.Destroy(this);
			
			
		}
		
	}
	
	
}
