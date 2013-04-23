// GravityEntity.cs
// Author : Fragmads
// Package : Game engine
// Last modification date : 03/09/2012
//
// GravityEntity : an entity affected by gravity. It always has at least one momentum applied for gravity
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class GravityEntity : AEntity {
	
	public float Weight = 1.0f;
		
	public void Start(){
		
		this.AddGravityMomentum();
		
	}
	
	public new void FixedUpdate(){
		
		base.FixedUpdate();
		
		// If the entity has no momentum attached
		if(this.gameObject.GetComponent<GravityMomentum>() == null){
			
			// Be sure that it don't ignore gravity
			this.AddGravityMomentum();
			
		}
		
	}
	
	
	private void AddGravityMomentum(){
		
		Momentum gravity = this.gameObject.AddComponent<GravityMomentum>();
		gravity.angle = 270;
		gravity.reduction = 0;
		gravity.strength = this.Weight * Stage.Gravity;
		
	}
	
}
