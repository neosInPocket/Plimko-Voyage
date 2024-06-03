using Cinemachine;
using UnityEngine;

[ExecuteInEditMode]
[SaveDuringPlay]
public class Virtual2DExtension : CinemachineExtension
{
	protected override void PostPipelineStageCallback(
		CinemachineVirtualCameraBase virtual2D,
		CinemachineCore.Stage stageState, ref CameraState condition, float time)
	{
		if (stageState == CinemachineCore.Stage.Body)
		{
			var pos = condition.RawPosition;
			pos.x = 0;
			condition.RawPosition = pos;
		}
	}
}
