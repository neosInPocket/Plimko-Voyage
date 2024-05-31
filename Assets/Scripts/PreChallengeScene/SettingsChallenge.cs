using UnityEngine;
using UnityEngine.UI;

public class SettingsChallenge : MonoBehaviour
{
	public Sprite greenStarred;
	public Sprite defaultSprite;
	public Image soundsModel;
	public Image musicModel;

	public void Start()
	{
		RefreshSettingsModel();
	}

	public void PreviewMusic()
	{
		AudioModel.Model.SetModel();
		RefreshSettingsModel();
	}

	public void PreviewSounds()
	{
		AudioModel.Model.SetEffectsModel();
		RefreshSettingsModel();
	}

	public void RefreshSettingsModel()
	{
		Sprite musicSprite = ChallengesHolder.Challenges.musicPopup == 1 ? greenStarred : defaultSprite;
		Sprite soundsSprite = ChallengesHolder.Challenges.soundsPopup == 1 ? greenStarred : defaultSprite;

		soundsModel.sprite = soundsSprite;
		musicModel.sprite = musicSprite;
	}
}
