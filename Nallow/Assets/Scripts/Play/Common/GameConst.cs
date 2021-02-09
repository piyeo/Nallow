using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameCommon
{
    public static class GameConst
    {
        public static Vector3 judgeZoneLeftPosition = GameObject.Find("JudgeZoneLeft").transform.localPosition;
        public static Vector3 judgeZoneRightPosition = GameObject.Find("JudgeZoneRight").transform.localPosition;
    }
}
