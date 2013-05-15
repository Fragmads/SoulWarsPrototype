// HitBox.cs
// Author : Fragmads
// Package : Physic engine
// Creation date : 24/04/2014
//
// HitBox : A collider that may be part of an attack
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class HitBox : MonoBehaviour {
	
	// Properties
	//
	
	// The name of the bone this HitBox is attached to
	public string BoneName;
	
	// Owner of this hitbox
	public Fighter Owner;
	
	// The Attack using this hitbox
	public Attack attack;
	
	public int Damage;
	
	public Momentum KnockBack;
	
	// Method
	//
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnTriggerEnter(Collider other){
		
		// If this hitboxes touched an HurtBox that isn't one of the owner
		if(this.enabled && other.gameObject.GetComponent<HurtBox>()!= null && !other.gameObject.GetComponent<HurtBox>().Owner.Equals(this.Owner)){
			
			this.attack.ApplyAttack(other.gameObject.GetComponent<HurtBox>().Owner);		
				
		}
		
	}
	
	public void OnTriggerStay(Collider other){
		
		// If this hitboxes touched an HurtBox that isn't one of the owner
		if(other.gameObject.GetComponent<HurtBox>() && !other.gameObject.GetComponent<HurtBox>().Owner.Equals(this.Owner)){
			// TODO Hit the target
		}
		
	}
	
	
}
