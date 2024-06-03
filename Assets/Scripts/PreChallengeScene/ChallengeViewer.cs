using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeViewer : MonoBehaviour
{
	public TMP_Text levelTMP;
	public TMP_Text rombCompleterText;
	public Image fillCompleter;
	private float maxScoreValue;
	private float maxRewardValue;
	private float currentScore;

	public void InitializeCompleter(Func<int, int> maxScoreFunc, Func<int, int> maxRewardFunc)
	{
		int currentSection = ChallengesHolder.Challenges.currentSection;
		maxScoreValue = maxScoreFunc(currentSection);
		maxRewardValue = maxRewardFunc(currentSection);

		currentScore = 0;

		levelTMP.text = $"LEVEL {currentSection}";
		rombCompleterText.text = $"0/{maxScoreValue}";
		fillCompleter.fillAmount = 0;
	}

	public bool IncreaseScore()
	{
		bool returnValue;
		currentScore++;

		if (currentScore == maxScoreValue)
		{
			returnValue = true;
		}
		else
		{
			returnValue = false;
		}

		rombCompleterText.text = $"{currentScore}/{maxScoreValue}";
		fillCompleter.fillAmount = (float)currentScore / (float)maxScoreValue;
		return returnValue;
	}
}
