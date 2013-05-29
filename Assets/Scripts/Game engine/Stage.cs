// Stage.cs
// Author : Fragmads
// Package : Game engine
// Last modification date : 03/09/2012
//
// Stage : A stage is an area where battle occurs. It has several platforms, blastzones, and start points for players
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stage : MonoBehaviour {
	
	// Object grouping the platforms of this stage
	public GameObject Platforms;
	public float StageGravity = 1;
	
	// Object grouping the SpawnPoint of this stage
	public GameObject SpawnPoints;
	
	private static float gravity = 1;
	public static float Gravity  {
		get {return Stage.gravity;}
	}
	
	// Use this for initialization
	void Start () {
	
		// Initialize the platforms of this stage
		Platform.StagePlatforms =new List<Platform>( this.Platforms.GetComponentsInChildren<Platform>());
		
		SpawnPoint.StageSpawnPoint =new List<SpawnPoint>( this.SpawnPoints.GetComponentsInChildren<SpawnPoint>());
		
		Stage.gravity = this.StageGravity;
		
	}
	
}
