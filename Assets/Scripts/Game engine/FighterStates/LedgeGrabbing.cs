// Move.cs
// Author : Fragmads
// Package : Game engine
// Last modification date : 03/09/2012
//
// Move : A move that can be executed by a fighter
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class LedgeGrabbing : AFighterState {
	
	public Edge edge;
	
	// Method
	//
	
	public new void Start(){
		
		base.Start();
		
		// Pla ce the fighter at the ledge position
		this.gameObject.transform.position = this.edge.gameObject.transform.position;
		
	}
	
	// Send the name of this state
	public override string getStateName() {
		return "LedgeGrabbing";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		// TODO dropping the ledge, jumping from it, rolling in, standing, rising attacks
		
		// If ledge drop
		if(input.RightStickY < -0.5f){
			
			this.GoAirborne();
			
		}
		
		// If ledge jump
		if(input.CommandJump){
			
			// You are now considered to be on the ground
			OnGround onGround = this.gameObject.AddComponent<OnGround>();
			onGround.platform = this.edge.platform;
			
			// Translate the entity so it doesn't fall of the platform right away
			Vector3 newPos;
			if(this.edge.isLeft){
				newPos = new Vector3(this.gameObject.transform.position.x + 0.1f, this.gameObject.transform.position.y, 0);
			}
			else {
				newPos = new Vector3(this.gameObject.transform.position.x - 0.1f, this.gameObject.transform.position.y, 0);
			}
			this.gameObject.transform.position = newPos;
			
			// Start Jumping
			Jumping jumping = this.gameObject.AddComponent<Jumping>();
			this.fighter.State = jumping;
			Object.Destroy(this);
			
		}
		
		// If Standing on ledge
		if(input.RightStickY > 0.5){
			
			// You are now considered to be on the ground
			OnGround onGround = this.gameObject.AddComponent<OnGround>();
			onGround.platform = this.edge.platform;
			
			// Translate the entity so it doesn't fall of the platform right away
			Vector3 newPos;
			if(this.edge.isLeft){
				newPos = new Vector3(this.edge.gameObject.transform.position.x + 0.1f, this.edge.gameObject.transform.position.y, 0);
			}
			else {
				newPos = new Vector3(this.edge.gameObject.transform.position.x - 0.1f, this.edge.gameObject.transform.position.y, 0);
			}
			this.gameObject.transform.position = newPos;
			
			// Standing
			Standing standing = this.gameObject.AddComponent<Standing>();
			this.fighter.State = standing;
			Object.Destroy(this);
			
		}
		
	}	
	
	public void FixedUpdate(){
		
		// Prevent the fighter from falling under the platform
		this.gameObject.transform.position = new Vector3 (this.gameObject.transform.position.x, this.edge.gameObject.transform.position.y, 0 );
		this.fighter.SpeedY = 0;
		
	}
	
	
	// Make the fighter airborne
	public void GoAirborne(){
		
		// The fighter is airborne, he is not on ground anymore
		Airborne airborne = this.gameObject.AddComponent<Airborne>();
		this.fighter.State = airborne;
		Object.Destroy(this);
		
		
	}
	
}
