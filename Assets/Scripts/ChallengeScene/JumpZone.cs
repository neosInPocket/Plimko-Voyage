using UnityEngine;

public class JumpZone : MonoBehaviour
{
	[SerializeField] public SpriteRenderer divider;
	[SerializeField] public ParticleSystem particlesLeft;
	[SerializeField] public ParticleSystem zoneLeft;
	[SerializeField] public ParticleSystem particlesMiddle;
	[SerializeField] public ParticleSystem zoneMiddle;
	[SerializeField] public ParticleSystem particlesRight;
	[SerializeField] public ParticleSystem zoneRight;
	[SerializeField] public Color selectColor;
	[SerializeField] public Color selectColorParticles;
	[SerializeField] public Collider2D breakCollider;
	[SerializeField] public float[] zoneHeights;

	public FractionType fractionType;

	private void Start()
	{
		PrepareZone();
		breakCollider.enabled = false;
	}

	public void PrepareZone()
	{
		float zoneHeight = zoneHeights[ChallengesHolder.Challenges.pendatUpgrade];

		var screenSize = EnhancedTouchSupportRouter.Aspect;
		divider.size = new Vector2(2 * screenSize.x, divider.size.y);

		zoneLeft.transform.parent.transform.localPosition = new Vector2(2 * screenSize.x * 1f / 6f - screenSize.x, zoneLeft.transform.parent.transform.localPosition.y);
		zoneLeft.transform.localScale = new Vector3(2f * screenSize.x / 3f, zoneHeight, zoneLeft.transform.localScale.z);
		var leftShape = particlesLeft.shape;
		leftShape.radius = 2f * screenSize.x / 6f;

		zoneMiddle.transform.parent.transform.localPosition = new Vector2(2 * screenSize.x * 3f / 6f - screenSize.x, zoneMiddle.transform.parent.transform.localPosition.y);
		zoneMiddle.transform.localScale = new Vector3(2f * screenSize.x / 3f, zoneHeight, zoneMiddle.transform.localScale.z);
		var middleShape = particlesMiddle.shape;
		middleShape.radius = 2f * screenSize.x / 6f;

		zoneRight.transform.parent.transform.localPosition = new Vector2(2 * screenSize.x * 5f / 6f - screenSize.x, zoneRight.transform.parent.transform.localPosition.y);
		zoneRight.transform.localScale = new Vector3(2f * screenSize.x / 3f, zoneHeight, zoneRight.transform.localScale.z);
		var rightShape = particlesRight.shape;
		rightShape.radius = 2f * screenSize.x / 6f;

		SelectRandomZone();
	}

	public void SelectRandomZone()
	{
		fractionType = FractionType.Left;

		if (Random.Range(0, 1f) < 0.5f)
		{
			fractionType = FractionType.Right;
		}

		if (Random.Range(0, 1f) < 0.5f)
		{
			fractionType = FractionType.Middle;
		}

		switch (fractionType)
		{
			case FractionType.Left:
				{
					var zoneMain = zoneLeft.main;
					var particlesMain = particlesLeft.main;
					zoneMain.startColor = new ParticleSystem.MinMaxGradient(selectColor);
					particlesMain.startColor = new ParticleSystem.MinMaxGradient(selectColor);
					break;
				}

			case FractionType.Right:
				{
					var zoneMain = zoneRight.main;
					var particlesMain = particlesRight.main;
					zoneMain.startColor = new ParticleSystem.MinMaxGradient(selectColor);
					particlesMain.startColor = new ParticleSystem.MinMaxGradient(selectColor);
					break;
				}

			case FractionType.Middle:
				{
					var zoneMain = zoneMiddle.main;
					var particlesMain = particlesMiddle.main;
					zoneMain.startColor = new ParticleSystem.MinMaxGradient(selectColor);
					particlesMain.startColor = new ParticleSystem.MinMaxGradient(selectColor);
					break;
				}
		}
	}

	public void PassZone(Jumper jumper)
	{
		breakCollider.enabled = true;
		var jumperPosition = jumper.transform.position;

		jumperPosition.y = divider.transform.position.y * divider.size.y / 2 + jumper.spriteRenderer.size.y / 2;
	}
}
