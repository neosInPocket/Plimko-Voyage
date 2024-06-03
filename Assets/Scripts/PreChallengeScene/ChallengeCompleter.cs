using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChallengeCompleter : MonoBehaviour
{
	public TMP_Text ticketText;
	public TMP_Text starsText;
	public TMP_Text controlButtonText;
	public Image ticketImage;
	public Sprite ticketGold;
	public Sprite ticketBlue;

	public void CompleteChallenge(string ticketText, string starsText, string controlButtonText, bool goldTicket)
	{
		this.ticketText.text = ticketText;
		this.starsText.text = starsText;
		this.controlButtonText.text = controlButtonText;
		gameObject.SetActive(true);

		if (goldTicket)
		{
			ticketImage.sprite = ticketGold;
		}
		else
		{
			ticketImage.sprite = ticketBlue;
		}
	}

	public void ControlPreChallengeScene()
	{
		SceneManager.LoadScene("PreChallengeScene");
	}

	public void ControlChallengeScene()
	{
		SceneManager.LoadScene("ChallengeScene");
	}
}
