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
	[SerializeField] public float shrinkSpeed;
	public FractionType fractionSelected { get; private set; } = FractionType.Middle;

	private void Start()
	{

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

		}
		else
		{
			fractionSelected = fractionType;
			transform.DOMoveX(jumperNextPosition.x, selectTime);
		}

	}

	public void StartBounce()
	{
		Touch.onFingerUp += EndBounce;
	}

	public void EndBounce(Finger finger)
	{
		Touch.onFingerUp -= EndBounce;
		jumperRigid.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
	}

	public IEnumerator ShrinkBall()
	{
		float shrinkForce = 0;
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

		RaycastHit2D[] raycasthit = Physics2D.RaycastAll(transform.position, Vector3.forward);
		var JumpZone = raycasthit.FirstOrDefault(x => x.collider.GetComponent<JumpZone>() != null);

		if (JumpZone.collider != null)
		{
			Debug.Log("zone is not null");
		}
		else
		{
			Debug.Log("zone is null");
		}
	}

	public void OnDisable()
	{
		Touch.onFingerDown -= SelectFraction;
		Touch.onFingerUp -= EndBounce;
	}
}
