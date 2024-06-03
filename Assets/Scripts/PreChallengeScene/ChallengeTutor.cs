using System.Collections.Generic;
using UnityEngine;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using Finger = UnityEngine.InputSystem.EnhancedTouch.Finger;

public class ChallengeTutor : MonoBehaviour
{
	public List<GameObject> TMP_Texts;
	public Animator mainAnimationController;
	public Challenger challenger;
	public string nextAnimatorAction;
	[HideInInspector] public int currentIndexer;

	public void ChallengeTutorial()
	{
		gameObject.SetActive(true);
		Touch.onFingerDown += ToggleOtherText;
		currentIndexer = 1;
	}

	public void ToggleOtherText(Finger finger)
	{
		DisableAllTexts();
		TMP_Texts[currentIndexer].SetActive(true);
		mainAnimationController.SetTrigger(nextAnimatorAction);
		currentIndexer++;

		if (currentIndexer >= TMP_Texts.Count)
		{
			EndChallengeGuide();
		}
	}

	public void EndChallengeGuide()
	{
		Touch.onFingerDown -= ToggleOtherText;
		challenger.AfterGuide();
		gameObject.SetActive(false);
	}

	public void DisableAllTexts()
	{
		TMP_Texts.ForEach(x => x.SetActive(false));
	}

	public void OnDisable()
	{
		Touch.onFingerDown -= ToggleOtherText;
	}
}
