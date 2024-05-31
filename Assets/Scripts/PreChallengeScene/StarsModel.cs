using System.ComponentModel;
using TMPro;
using UnityEngine;

public class StarsModel : MonoBehaviour
{
	private void Start()
	{
		SetStars();
	}

	public void SetStars()
	{
		TMP_Text text = GetComponent<TMP_Text>();
		text.text = ChallengesHolder.Challenges.stars.ToString();
	}
}
