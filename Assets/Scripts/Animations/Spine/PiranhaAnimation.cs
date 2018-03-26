using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaAnimation : SpineAnimation {

	public enum States
    {
        Idle,
        Submerge,
        Emerge,
        Spin_Start,
        Spin_Repeatable,
        Spin_End
    }
}
