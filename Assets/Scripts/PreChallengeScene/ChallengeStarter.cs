using UnityEngine;

public class ChallengeStarter : MonoBehaviour
{
	[SerializeField] public Challenger challenger;

	public void StartChallengeStarter()
	{
		gameObject.SetActive(true);
	}

	public void InitializeChallengeStart()
	{
		challenger.StartChallenge();
		gameObject.SetActive(false);
	}
}
