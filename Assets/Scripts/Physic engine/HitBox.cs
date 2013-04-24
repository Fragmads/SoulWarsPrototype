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
		if(other.gameObject.GetComponent<HurtBox>() && !other.gameObject.GetComponent<HurtBox>().Owner.Equals(this.Owner)){
			// TODO hit the target
		}
		
	}
	
	public void OnTriggerStay(Collider other){
		
		// If this hitboxes touched an HurtBox that isn't one of the owner
		if(other.gameObject.GetComponent<HurtBox>() && !other.gameObject.GetComponent<HurtBox>().Owner.Equals(this.Owner)){
			// TODO Hit the target
		}
		
	}
	
	
}
