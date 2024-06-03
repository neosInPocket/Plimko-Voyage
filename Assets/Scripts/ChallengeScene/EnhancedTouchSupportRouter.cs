using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class EnhancedTouchSupportRouter : MonoBehaviour
{
	public static Vector2 Aspect { get; private set; }

	public void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();

		Aspect = new Vector2(Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize);
	}

	public static float ScreenPositionFromNormalized(float normalized)
	{
		float result = 2 * Aspect.x * normalized - Aspect.x;
		return result;
	}
}
