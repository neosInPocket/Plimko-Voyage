using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SectionPreviewer : MonoBehaviour
{
	[SerializeField] private Image sectionProgressFill;
	[SerializeField] private TMP_Text sectionCostText;
	[SerializeField] private int sectionCostPreview;
	[Header("Button")]
	[SerializeField] private Button sectionPurchase;
	[SerializeField] private TMP_Text sectionPurchaseText;
	[Header("Romb level")]
	[SerializeField] private TMP_Text sectionLevelText;
	[SerializeField] private StarsModel starsModel;
	[Header("Upgrade type")]
	[SerializeField] private SectionUpgradeType sectionUpgradeType;
	[SerializeField] private float alpha;
	[SerializeField] private SectionPreviewer sectionPreviewer;
	[SerializeField] private Sprite starredGreenSprite;
	[SerializeField] private Sprite blueSprite;
	[SerializeField] private Image progressHolder;
	private void Start()
	{
		UpdateSectionView();
	}

	public void UpdateSectionView()
	{
		int purchasedUpgrade = sectionUpgradeType == SectionUpgradeType.Ring ? ChallengesHolder.Challenges.ringUpgrade : ChallengesHolder.Challenges.pendatUpgrade;
		sectionProgressFill.fillAmount = (float)purchasedUpgrade / 5f;

		sectionLevelText.text = purchasedUpgrade.ToString();
		sectionCostText.text = sectionCostPreview.ToString();

		if (purchasedUpgrade == 5)
		{
			sectionPurchase.interactable = false;
			var color = Color.green;
			color.a = alpha;
			sectionPurchaseText.color = color;
			sectionPurchaseText.text = "MAX";
			progressHolder.sprite = starredGreenSprite;
			sectionLevelText.enabled = false;
		}
		else
		{
			int currentStars = ChallengesHolder.Challenges.stars;
			int difference = currentStars - sectionCostPreview;

			if (difference < 0)
			{
				sectionPurchase.interactable = false;
				var color = Color.red;
				color.a = alpha;
				sectionPurchaseText.color = color;
				sectionPurchaseText.text = "NO STARS";
			}
			else
			{
				sectionPurchase.interactable = true;
				var color = Color.white;
				color.a = 1;
				sectionPurchaseText.color = color;
				sectionPurchaseText.text = "UPGRADE";
			}

			progressHolder.sprite = blueSprite;
			sectionLevelText.enabled = true;
		}
	}

	public void SpendStars()
	{
		int starsLeft = ChallengesHolder.Challenges.stars - sectionCostPreview;

		if (sectionUpgradeType == SectionUpgradeType.Ring)
		{
			ChallengesHolder.Challenges.ringUpgrade += 1;
		}
		else
		{
			ChallengesHolder.Challenges.pendatUpgrade += 1;
		}

		ChallengesHolder.Challenges.stars = starsLeft;
		ChallengesHolder.Challenges.RewriteAllSaves();
		UpdateSectionView();
		sectionPreviewer.UpdateSectionView();
		starsModel.SetStars();
	}
}

public enum SectionUpgradeType
{
	Ring,
	Pednant
}
