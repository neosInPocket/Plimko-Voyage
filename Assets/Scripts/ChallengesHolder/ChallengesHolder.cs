using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengesHolder : MonoBehaviour
{
	[Header("Rewrite saves")]
	[SerializeField] private bool rewriteSaves;
	[Header("Defaults")]
	[SerializeField] public int currentSection;
	[SerializeField] public int stars;
	[SerializeField] public int ringUpgrade;
	[SerializeField] public int pendatUpgrade;
	[SerializeField] public int soundsPopup;
	[SerializeField] public int musicPopup;
	[SerializeField] public int intro;
	public static ChallengesHolder Challenges;

	private void Awake()
	{
		if (Challenges)
		{
			Destroy(gameObject);
			return;
		}
		else
		{
			DontDestroyOnLoad(gameObject);
			Challenges = this;
		}

		if (rewriteSaves)
		{
			RewriteAllSaves();
		}
		else
		{
			LoadWrittenSaves();
		}
	}

	public void LoadWrittenSaves()
	{
		currentSection = PlayerPrefs.GetInt("currentSection", currentSection);
		stars = PlayerPrefs.GetInt("stars", stars);
		ringUpgrade = PlayerPrefs.GetInt("ringUpgrade", ringUpgrade);
		pendatUpgrade = PlayerPrefs.GetInt("pendatUpgrade", pendatUpgrade);
		soundsPopup = PlayerPrefs.GetInt("soundsPopup", soundsPopup);
		musicPopup = PlayerPrefs.GetInt("musicPopup", musicPopup);
		intro = PlayerPrefs.GetInt("intro", intro);
	}

	public void RewriteAllSaves()
	{
		PlayerPrefs.SetInt("currentSection", currentSection);
		PlayerPrefs.SetInt("stars", stars);
		PlayerPrefs.SetInt("ringUpgrade", ringUpgrade);
		PlayerPrefs.SetInt("pendatUpgrade", pendatUpgrade);
		PlayerPrefs.SetInt("soundsPopup", soundsPopup);
		PlayerPrefs.SetInt("musicPopup", musicPopup);
		PlayerPrefs.SetInt("intro", intro);
		PlayerPrefs.Save();
	}
}
