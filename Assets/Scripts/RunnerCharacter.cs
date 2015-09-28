using UnityEngine;
using System.Collections;

public class RunnerCharacter : MonoBehaviour {
	// Singleton.
	public static RunnerCharacter Instance;

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}
	}

	// Nasty things.
	public void TakeDamage()
	{
		Application.LoadLevel("Defeat");
	}

	// Check for falling.
	void Update()
	{
		if (transform.position.y <= 0)
		{
			TakeDamage();
		}
	}
}
