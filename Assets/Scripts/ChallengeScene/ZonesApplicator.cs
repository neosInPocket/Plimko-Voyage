using UnityEngine;

public class ZonesApplicator : MonoBehaviour
{
	[SerializeField] public SpriteRenderer startBoxPlatform;
	[Range(0, 1f)]
	[SerializeField] public float startBoxFill;
	[SerializeField] public Jumper jumper;
	[SerializeField] public JumpZone jumpZone;
	[SerializeField] public Vector2 jumpZonesDistances;
	[Range(0, 1f)]
	[SerializeField] public float startZonePosition;
	[HideInInspector] public JumpZone lastZone;

	private void Start()
	{
		Vector2 screenSize = EnhancedTouchSupportRouter.Aspect;
		float screenFill = 2 * screenSize.y * startBoxFill - screenSize.y;
		startBoxPlatform.size = new Vector2(screenSize.x * 2, 15);
		startBoxPlatform.transform.position = new Vector2(0, -screenSize.y - startBoxPlatform.size.y / 2 + screenFill);
		jumper.transform.position = new Vector2(0, -screenSize.y + screenFill);

		var offset = 2 * screenSize.y * startZonePosition - screenSize.y;
		var firstZonePosition = new Vector2(0, -screenSize.y + offset);
		lastZone = Instantiate(jumpZone, firstZonePosition, Quaternion.identity, transform);
	}

	public void SpawnNext()
	{
		Vector2 screenSize = EnhancedTouchSupportRouter.Aspect;
		var firstZonePosition = new Vector2();
		firstZonePosition.x = 0;
		firstZonePosition.y = lastZone.transform.position.y + 2 * Random.Range(jumpZonesDistances.x, jumpZonesDistances.y) * screenSize.y;
		lastZone = Instantiate(jumpZone, firstZonePosition, Quaternion.identity, transform);
	}
}
