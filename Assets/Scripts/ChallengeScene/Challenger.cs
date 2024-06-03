using UnityEngine;

public class Challenger : MonoBehaviour
{
	public Jumper jumper;

	private void Start()
	{
		jumper.Avaliable(true);
	}
}
