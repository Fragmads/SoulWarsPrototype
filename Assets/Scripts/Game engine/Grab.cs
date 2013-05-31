// Grab.cs
// Author : Fragmads
// Package : Game engine
// Last modification date : 31/05/2013
//
// Grab : An attack resulting in a throw if successfull
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grab : Attack {
	
	// Properties
	//
	
	// the throw launched by this grab if successfull
	public Throw Throw;
	
		
	// Method
	//
	
	// You have grabbed your foe
	public new void ApplyAttack(Fighter f){
		
		// The owner goes in throw state
		Throwing throwing = this.Owner.gameObject.AddComponent<Throwing>();
		this.Owner.State = throwing;
		
		GameObject.Destroy(this.Owner.gameObject.GetComponent<Attacking>());
		
		Disabled disabled = f.gameObject.AddComponent<Disabled>();
		f.State = disabled;
		
		
		
		
	}
}
