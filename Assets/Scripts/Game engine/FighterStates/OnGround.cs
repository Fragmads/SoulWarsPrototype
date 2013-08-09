// OnGround.cs
// Author : Fragmads
// Package : Physic engine
// Last modification date : 19/04/2013
//
// OnGround : Describe a state where the fighter is on the ground
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnGround : AFighterState {
	
	public Platform platform;
	
	// Use this for initialization
	public new void Start () {
		base.Start();
		
		// The momentum used tu move the fighter when he is on the ground or in the air on the x axis
		//XMomentum gm = this.gameObject.AddComponent<XMomentum>();
		//gm.reduction = 0;
		//gm.strength = 0;
		
		
		// Be sure that you are no longer airborne
		if(this.gameObject.GetComponent<Airborne>() != null){
			
			GameObject.Destroy(this.gameObject.GetComponent<Airborne>());
			
		}
		
		// Be sure that you are no longer edge grabbing
		if(this.gameObject.GetComponent<LedgeGrabbing>() != null){
			
			GameObject.Destroy(this.gameObject.GetComponent<LedgeGrabbing>());
			
		}
		
	}
	
	// Send the name of this state
	public override string getStateName() {
		return "OnGround";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		// Nothing to do
			
	}
	
	public void FixedUpdate(){
		
		// Prevent the fighter from falling under the platform
		this.gameObject.transform.position = new Vector3 (this.gameObject.transform.position.x, this.platform.gameObject.transform.position.y, 0 );
		this.fighter.SpeedY = 0;
		
		// If the fighter is not over the platform anymore		
		if((this.gameObject.transform.position.x > (this.platform.gameObject.transform.position.x + this.platform.Length/2)) || (this.gameObject.transform.position.x < (this.platform.gameObject.transform.position.x - this.platform.Length/2)) ){
			
			this.GoAirborne();
			
		}
		
		// If the fighter is knocked in the air,he go airborne
		/*if(this.gameObject.GetComponent<Knocked>() != null){
			
			this.GoAirborne();
			
		}*/
		
	}
	
	// Make the fighter airborne
	public void GoAirborne(){
		
		// The fighter is airborne, he is not on ground anymore
		Airborne airborne = this.gameObject.AddComponent<Airborne>();
		this.fighter.State = airborne;
		
		// If you are not on a platform anymore, you can't be in those state
		if(this.gameObject.GetComponent<Standing>() != null){
			GameObject.Destroy(this.gameObject.GetComponent<Standing>());
		}
		if(this.gameObject.GetComponent<Walking>() != null){
			GameObject.Destroy(this.gameObject.GetComponent<Walking>());
		}
		if(this.gameObject.GetComponent<Dashing>() != null){
			GameObject.Destroy(this.gameObject.GetComponent<Dashing>());
		}
		if(this.gameObject.GetComponent<Guarding>() != null){
			GameObject.Destroy(this.gameObject.GetComponent<Guarding>());
		}		
		if(this.gameObject.GetComponent<WaveLanding>() != null){
			GameObject.Destroy(this.gameObject.GetComponent<WaveLanding>());
		}
		if(this.gameObject.GetComponent<Rolling>() != null){
			GameObject.Destroy(this.gameObject.GetComponent<Rolling>());
		}
		
		GameObject.Destroy(this);
		
		
	}
	
	// If the fighter want to try dropping from the platform
	public void DropPlatform(){
		
		// If the platform you are on allow this
		if(this.platform.CanDrop){
			
			// Go Airborne
			this.GoAirborne();
			
			// Move the fighter just a little under the platform
			Vector3 newPos = new Vector3 (this.fighter.gameObject.transform.position.x, this.fighter.gameObject.transform.position.y - 0.01f ,0);
			this.fighter.gameObject.transform.position = newPos;
			
		}
		
	}
	
	
}
