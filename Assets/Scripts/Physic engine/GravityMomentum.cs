// GravityMomentum.cs
// Author : Fragmads
// Package : Physic engine
// Last modification date : 21/04/2013
//
// GravityMomentum : A momentum used to apply the gravity (SubClass used for differenciation in GetComponent)
// 
// State : Complete

using UnityEngine;
using System.Collections;

public class GravityMomentum : Momentum {
	
	public float maxStrength;
	
	public new void FixedUpdate(){
		
		// If the momentum is attached to an entity, that is not Knocked or jumping
		if(this.gameObject.GetComponent<AEntity>() != null && this.gameObject.GetComponent<JumpMomentum>() == null
			&& this.gameObject.GetComponent<KnockBack>() == null) {
		
			// Decrease the strength of the momentum			
			this.strength += this.reduction/60;
			
			// Cap the strength of the momentum
			if(this.strength > this.maxStrength){
				this.strength = this.maxStrength;
			}
			
			// Reclaculate the momentum
			this.RecalculateVector();
							
		}
		// If not, set the strength to 0 so the fall will accelerate
		else {
			this.strength = 0;
		}
		
	}
	
}
