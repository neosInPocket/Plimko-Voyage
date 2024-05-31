using UnityEngine;

public class AudioModel : MonoBehaviour
{
	public static AudioModel Model;

	private void Awake()
	{
		if (Model)
		{
			Destroy(gameObject);
			return;
		}
		else
		{
			DontDestroyOnLoad(gameObject);
			Model = this;
		}
	}

	private void Start()
	{
		AudioSource source = GetComponent<AudioSource>();
		var toggleValue = ChallengesHolder.Challenges.musicPopup = ChallengesHolder.Challenges.musicPopup == 1 ? 1 : 0;
		source.volume = toggleValue;
	}

	public void SetModel()
	{
		AudioSource source = GetComponent<AudioSource>();
		var toggleValue = ChallengesHolder.Challenges.musicPopup = ChallengesHolder.Challenges.musicPopup == 1 ? 0 : 1;
		source.volume = toggleValue;
		ChallengesHolder.Challenges.musicPopup = toggleValue;
		ChallengesHolder.Challenges.RewriteAllSaves();
	}

	public void SetEffectsModel()
	{
		ChallengesHolder.Challenges.soundsPopup = ChallengesHolder.Challenges.soundsPopup == 1 ? 0 : 1;
		ChallengesHolder.Challenges.RewriteAllSaves();
	}
}
