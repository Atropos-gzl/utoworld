using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : Singleton<GlobalSettings>
{
    private float _MapCellStandardLength = 1f;
    public float MapCellStandardLength { get { return _MapCellStandardLength; } }


    private float _MaxForwardSpeedOfCameraMove = 10f;
    public float MaxForwardSpeedOfCameraMove { get { return _MaxForwardSpeedOfCameraMove; } }

    private float _CameraRotateTime = 0.15f;
    public float CameraRotateTime { get { return _CameraRotateTime; } }


    // 鼠标移动时相机速度
    private float[] _MaxSpeedOfCameraMove = new float[]{ 10f, 7f, 5f };
    public float[] MaxSpeedOfCameraMove { get { return _MaxSpeedOfCameraMove; } }
    // 鼠标判定移动范围占屏幕比例
    private float[] _moveFrameRangeRate = new float[] { 0.05f, 0.07f, 0.10f };
    public float[] moveFrameRangeRate { get { return _moveFrameRangeRate; } }
    
    // 定向移动时相机最大速度
    private float _MaxSpeedOfCameraDirectedMove = 40f;
    public float MaxSpeedOfCameraDirectedMove { get { return _MaxSpeedOfCameraDirectedMove; } }

}
