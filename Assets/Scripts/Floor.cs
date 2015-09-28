using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Floor : MonoBehaviour {

	// For stuff generation.
	public Transform 		slots;
	public List<GameObject> obstaclePrefabs;
	public List<GameObject> collectablePrefabs;

	// We need to know what is free now.
	private List<Transform> freeSlots = new List<Transform>();

	void Awake ()
	{
		// Some initialisation work.
		freeSlots = new List<Transform>(); 
		slots.GetComponentsInChildren<Transform>(freeSlots);
		freeSlots.Remove(slots.transform);
	}

	void Start () 
	{
		GenerateObstacles();
		GenerateCollectables();
	}

	/// <summary>
	/// Picks the random free slot.
	/// </summary>
	/// <returns>The random free slot.</returns>
	Transform PickRandomFreeSlot ()
	{
		// All slots could be occupied.
		if (freeSlots.Count > 0)
		{
			var random = Random.Range (0, freeSlots.Count);
			var slot = freeSlots [random];
			freeSlots.RemoveAt (random);
			return slot;
		}
		else
		{
			throw new KeyNotFoundException();
		}
	}


	/// <summary>
	/// Generates the obstacles.
	/// </summary>
	/// <param name="obstacleNumber">Obstacle number.</param>
	public void GenerateObstacles(int obstacleNumber = -1) 
	{
		Transform slot;

		// We may not have a free slot. 
		// If it is so, then do nothing.
		try 
		{
			slot = PickRandomFreeSlot ();
		}
		catch (KeyNotFoundException e)
		{
			return;
		}

		// Use the difficulty, Luke!
		float rnd = Random.value; 
		if (rnd < RunnerLevelLogic.Instance.DifficultyLevel)
		{
			return;
		}
		
		Helper.GenerateRandomPrefabInSlot (slot, obstaclePrefabs);
	}

	/// <summary>
	/// Generates the collectables.
	/// </summary>
	public void GenerateCollectables() 
	{
		// We may not have a free slot. 
		// If it is so, then do nothing.
		Transform slot;
		try 
		{
			slot = PickRandomFreeSlot ();
		}
		catch (KeyNotFoundException e)
		{
			return;
		}

		// Don't you get coins all the time.
		float rnd = Random.value; 
		if (rnd < 0.65f)
		{
			return;
		}
		
		Helper.GenerateRandomPrefabInSlot (slot, collectablePrefabs);
	}
}
