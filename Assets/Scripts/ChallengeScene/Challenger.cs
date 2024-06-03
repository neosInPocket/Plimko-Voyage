using TMPro;
using UnityEngine;

public class Challenger : MonoBehaviour
{
	public ChallengeViewer challengeViewer;
	public ChallengeCompleter challengeCompleter;
	public ChallengeTutor challengeTutor;
	public ChallengeStarter challengeStarter;
	public Jumper jumper;
	public DangerFloor dangerFloor;

	private void Start()
	{
		challengeViewer.InitializeCompleter(MaxScoreFunc, MaxRewardFunc);

		if (ChallengesHolder.Challenges.intro == 1)
		{
			ChallengesHolder.Challenges.intro = 0;
			ChallengesHolder.Challenges.RewriteAllSaves();

			challengeTutor.ChallengeTutorial();
		}
		else
		{
			AfterGuide();
		}
	}

	public void StartChallenge()
	{
		jumper.Avaliable(true);
		dangerFloor.FloorAvaliable(true);
	}

	public void AfterGuide()
	{
		challengeStarter.StartChallengeStarter();
	}

	public void IncrementScore()
	{
		if (challengeViewer.IncreaseScore())
		{
			jumper.Avaliable(false);
			dangerFloor.FloorAvaliable(false);

			challengeCompleter.CompleteChallenge("YOU WIN!", MaxRewardFunc(ChallengesHolder.Challenges.currentSection).ToString(), "NEXT", true);
			ChallengesHolder.Challenges.stars += MaxRewardFunc(ChallengesHolder.Challenges.currentSection);
			ChallengesHolder.Challenges.currentSection++;

			ChallengesHolder.Challenges.RewriteAllSaves();
		}
	}

	public void CrashComet()
	{
		jumper.Avaliable(false);
		dangerFloor.FloorAvaliable(false);

		challengeCompleter.CompleteChallenge("YOU LOSE", "0", "RETRY", false);
	}

	public int MaxScoreFunc(int x)
	{
		return (int)(10 * Mathf.Log(x + 1) - 3);
	}

	public int MaxRewardFunc(int x)
	{
		return (int)(10 * Mathf.Log(x + 1) + 3);
	}
}
