// HurtBox.cs
// Author : Fragmads
// Package : Physic engine
// Last modification date : 03/09/2012
//
// HurtBox : A body that can be hit by attack
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class HurtBox : MonoBehaviour {
	
	// Owner of this hurtbox
	public Fighter Owner;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnTriggerEnter(Collider other){
		
		// If this hitboxes touched an HurtBox that is'nt one of the owner
		if(other.gameObject.GetComponent<HurtBox>() && !other.gameObject.GetComponent<HurtBox>().Owner.Equals(this.Owner)){
			
		}
		
	}
	
}
