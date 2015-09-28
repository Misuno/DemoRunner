using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wall : MonoBehaviour 
{

	// For stuff generation.
	public Transform propSlots;
	public List<GameObject> propPrefabs;

	// Use this for initialization
	void Start () 
	{
		GenerateProps();
	}

	/// <summary>
	/// Generates the properties.
	/// </summary>
	/// <param name="propsNumber">Properties number.</param>
	public void GenerateProps(int propsNumber = -1) 
	{
		var slotsCount = propSlots.childCount;

		// If default value is used, take random number of props.
		if (propsNumber < 0)
		{
			propsNumber = Random.Range(0, slotsCount);
		}

		// Generate random props.
		for (var i = 0; i < propsNumber; ++i)
		{	
			var slot = propSlots.GetChild(i);
			Helper.GenerateRandomPrefabInSlot (slot.transform, propPrefabs);
		}
	}
}
