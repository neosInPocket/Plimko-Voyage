using UnityEngine;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using Finger = UnityEngine.InputSystem.EnhancedTouch.Finger;
using System.Collections;
using System.Linq;
using DG.Tweening;

public class Jumper : MonoBehaviour
{
	[SerializeField] public float selectTime;
	[SerializeField] public Rigidbody2D jumperRigid;
	[SerializeField] public float jumpForce;
	[SerializeField] public SpriteRenderer spriteRenderer;
	[SerializeField] public new CircleCollider2D collider;
	[SerializeField] public float maxShrink;
	[SerializeField] public float[] shrinkSpeeds;
	[SerializeField] public GameObject pass;
	[SerializeField] public GameObject explode;
	[SerializeField] public ZonesApplicator zonesApplicator;
	[SerializeField] public DangerFloor dangerFloor;
	[SerializeField] public Challenger challenger;
	public FractionType fractionSelected { get; private set; } = FractionType.Middle;
	private float shrinkForce;
	private float shrinkSpeed;

	private void Start()
	{
		shrinkSpeed = shrinkSpeeds[ChallengesHolder.Challenges.ringUpgrade];
	}


	public void Avaliable(bool value)
	{
		if (value)
		{
			Touch.onFingerDown += SelectFraction;
		}
		else
		{
			Touch.onFingerDown -= SelectFraction;
			Touch.onFingerUp -= EndBounce;
		}
	}

	public void SelectFraction(Finger finger)
	{
		FractionType fractionType = FractionType.Left;
		float screenNormalized = finger.screenPosition.x / Screen.width;
		Vector2 jumperNextPosition = new Vector2();
		jumperNextPosition.y = transform.position.y;

		if (screenNormalized < 1f / 3f && screenNormalized >= 0)
		{
			fractionType = FractionType.Left;
			jumperNextPosition.x = EnhancedTouchSupportRouter.ScreenPositionFromNormalized(1f / 6f);
		}

		if (screenNormalized >= 1f / 3f && screenNormalized < 2f / 3f)
		{
			fractionType = FractionType.Middle;
			jumperNextPosition.x = EnhancedTouchSupportRouter.ScreenPositionFromNormalized(0.5f);
		}

		if (screenNormalized >= 2f / 3f && screenNormalized <= 1f)
		{
			fractionType = FractionType.Right;
			jumperNextPosition.x = EnhancedTouchSupportRouter.ScreenPositionFromNormalized(5f / 6f);
		}

		if (fractionSelected == fractionType)
		{
			jumperRigid.constraints = RigidbodyConstraints2D.FreezeAll;
			collider.enabled = false;
			StartCoroutine(ShrinkBall());
			Touch.onFingerDown -= SelectFraction;
			Touch.onFingerUp += EndBounce;
		}
		else
		{
			fractionSelected = fractionType;
			transform.DOMoveX(jumperNextPosition.x, selectTime);
		}
	}

	public void EndBounce(Finger finger)
	{
		StopAllCoroutines();
		Touch.onFingerUp -= EndBounce;
		transform.localScale = Vector3.one;
		collider.enabled = true;
		jumperRigid.constraints = RigidbodyConstraints2D.None;
		jumperRigid.AddForce(shrinkForce * jumpForce * Vector2.up, ForceMode2D.Impulse);
		StartCoroutine(WaitForZeroSpeed());
	}

	public IEnumerator ShrinkBall()
	{
		shrinkForce = 0;
		Vector3 scale = Vector3.one;

		while (shrinkForce < maxShrink)
		{
			shrinkForce += shrinkSpeed * Time.deltaTime;
			scale.y = 1 - shrinkForce;
			transform.localScale = scale;
			yield return null;
		}
	}

	public IEnumerator WaitForZeroSpeed()
	{
		while (jumperRigid.velocity.y > 0)
		{
			yield return null;
		}

		RaycastHit2D[] raycasthit = Physics2D.RaycastAll(transform.position + new Vector3(0, spriteRenderer.size.y / 2, 0), Vector3.forward);
		var JumpZone = raycasthit.FirstOrDefault(x => x.collider.name == "Zone");

		if (JumpZone.collider != null)
		{
			JumpZone zone = JumpZone.collider.transform.parent.parent.GetComponent<JumpZone>();
			zone.PassZone(this);
			StopCoroutine(Pass());
			StartCoroutine(Pass());
			Touch.onFingerDown += SelectFraction;
			zonesApplicator.SpawnNext();
			dangerFloor.SetFloorNextLayer();
			challenger.IncrementScore();
		}
		else
		{
			StartCoroutine(Explode());
			Avaliable(false);
			challenger.CrashComet();
		}
	}

	public IEnumerator Pass()
	{
		var decoy = Instantiate(pass, transform.position, Quaternion.identity, transform);
		yield return new WaitForSeconds(1f);
		Destroy(decoy.gameObject);
	}

	public void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<DangerFloor>(out DangerFloor dangerFloor))
		{
			challenger.CrashComet();
			StartCoroutine(Explode());
		}
	}

	public IEnumerator Explode()
	{
		spriteRenderer.enabled = false;
		jumperRigid.constraints = RigidbodyConstraints2D.FreezeAll;
		collider.enabled = false;
		explode.SetActive(true);
		yield return new WaitForSeconds(1f);
		explode.SetActive(false);
	}

	public void OnDisable()
	{
		Touch.onFingerDown -= SelectFraction;
		Touch.onFingerUp -= EndBounce;
		StopAllCoroutines();
	}
}
