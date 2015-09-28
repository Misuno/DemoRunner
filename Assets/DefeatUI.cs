using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DefeatUI : MonoBehaviour {

	public Text highScoreText;
	public Text coinsText;

	void Start()
	{
		highScoreText.gameObject.SetActive(Stats.Instance.UpdateAndCheckBest());
		coinsText.text = Stats.Instance.coins.ToString();
	}

	public void Restart()
	{
		Application.LoadLevel("Runner");
	}
}
