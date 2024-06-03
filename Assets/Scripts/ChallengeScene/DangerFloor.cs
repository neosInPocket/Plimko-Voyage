using UnityEngine;

public class DangerFloor : MonoBehaviour
{
	[SerializeField] private float dangerSpeed;
	[SerializeField] public float floorNextLayerOffset;
	[SerializeField] public float cameraOffset;
	[SerializeField] public Jumper jumper;
	public Vector2 pos;
	private bool dangerEnabled;

	public void Start()
	{
		var screenSize = EnhancedTouchSupportRouter.Aspect;
		pos = transform.position;
		pos.x = 0;
		pos.y = jumper.transform.position.y - cameraOffset + floorNextLayerOffset;
		transform.position = pos;
	}

	public void FloorAvaliable(bool avaliable)
	{
		dangerEnabled = avaliable;
	}

	public void SetFloorNextLayer()
	{
		pos.y = jumper.transform.position.y - cameraOffset + floorNextLayerOffset;
		transform.position = pos;
	}

	private void Update()
	{
		if (!dangerEnabled) return;

		pos.y += dangerSpeed * Time.deltaTime;
		transform.position = pos;
	}
}
