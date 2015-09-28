using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Helper : MonoBehaviour
{
	/// <summary>
	/// Generates the random prefab in specified slot.
	/// </summary>
	/// <param name="slotTransform">Slot transform.</param>
	/// <param name="prefabs">Prefabs list.</param>
	public static void GenerateRandomPrefabInSlot (Transform slotTransform, List<GameObject> prefabs)
	{
		if (prefabs.Count > 0)
		{
			// Get the random prefab from list.
			var rnd = Random.Range (0, prefabs.Count);
			var prefab = prefabs [rnd];

			// And create it. 
			var go = Instantiate<GameObject> (prefab);

			// Apply transfom and put into children.
			go.transform.position 	= slotTransform.position;
			go.transform.rotation	= slotTransform.rotation;
			go.transform.parent 	= slotTransform;
		}
	}
}

/// <summary>
/// Part type. Used for combining different level part types.
/// </summary>
public enum PartType
{
	Tall, WaterLeft, WaterRight
}
