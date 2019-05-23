using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;
using System.Reflection;

[System.Serializable]
public class VsnWaypoint{
	public string label;
	public int commandNumber;

	public VsnWaypoint(string argLabel, int argCommandNumber){
    label = argLabel;
    commandNumber = argCommandNumber;
	}
}
