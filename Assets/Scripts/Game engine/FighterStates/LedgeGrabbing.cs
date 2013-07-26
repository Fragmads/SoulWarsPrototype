// LedgeGrabbing.cs
// Author : Fragmads
// Package : Game engine/FighterStates
// Last modification date : 03/09/2012
//
// LedgeGrabbing : A states describing a fighter hang to a ledge
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class LedgeGrabbing : AFighterState {
	
	public Edge edge;
	
	// Method
	//
	
	private int firstFrame = 0;
	
	public new void Start(){
		
		base.Start();
				
		// Be sure that the fighter is not on airborne or onGround state
		if(this.fighter.gameObject.GetComponent<Airborne>() != null){
			GameObject.Destroy(this.fighter.gameObject.GetComponent<Airborne>());
		}
		
		if(this.fighter.gameObject.GetComponent<OnGround>() != null){
			GameObject.Destroy(this.fighter.gameObject.GetComponent<OnGround>());
		}
		
		if(this.fighter.gameObject.GetComponent<UselessStance>() != null){
			GameObject.Destroy(this.fighter.gameObject.GetComponent<UselessStance>());			
		}
			
		if(this.fighter.gameObject.GetComponent<AirDodging>() != null){
			GameObject.Destroy(this.fighter.gameObject.GetComponent<AirDodging>());			
		}
		
		
		// Prevent the fighter from moving once he is grabbing the ledge
		Momentum.Clean(this.fighter);
		if(this.fighter.gameObject.GetComponent<XMomentum>() != null){
			this.fighter.gameObject.GetComponent<XMomentum>().strength = 0;
		}
		
		// play the ledgeGrab animation
		this.gameObject.animation.Play("ledgeGrab", PlayMode.StopAll);
		
	}
	
	// Send the name of this state
	public override string getStateName() {
		return "LedgeGrabbing";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		
		// No input is processed on the first frames you grab a ledge
		if(this.firstFrame < 0){
			this.firstFrame ++;
		}
		else{
		
			// TODO dropping the ledge, jumping from it, rolling in, standing, rising attacks
			
			// If ledge drop
			if(input.LeftStickY < -0.5f){
				
				this.GoAirborne();
				
				Vector3 newPos = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - (this.fighter.MaxEdgeGrabHeight +0.01f), 0);
				this.gameObject.transform.position = newPos;
				
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
			if(input.LeftStickY > 0.5){
				
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
		
	}	
	
	public void FixedUpdate(){
		
		// Prevent the fighter from falling under the platform
		//this.gameObject.transform.position = new Vector3 (this.gameObject.transform.position.x, this.edge.gameObject.transform.position.y, 0 );
		this.fighter.SpeedY = 0;
		
		// Place the fighter at the ledge position		
		if(this.edge.isLeft){
			
			Vector3 newPos = new Vector3(this.edge.gameObject.transform.position.x -0.1f, this.edge.gameObject.transform.position.y, 0);
			this.gameObject.transform.position = newPos;
		}
		else if (this.edge.isRight) {
			
			Vector3 newPos = new Vector3(this.edge.gameObject.transform.position.x +0.1f, this.edge.gameObject.transform.position.y, 0);
			this.gameObject.transform.position = newPos;
			
		}
		
	}
	
	
	// Make the fighter airborne
	public void GoAirborne(){
		
		// The fighter is airborne, he is not on ground anymore
		Airborne airborne = this.gameObject.AddComponent<Airborne>();
		this.fighter.State = airborne;
		Object.Destroy(this);
		
		
	}
	
}
