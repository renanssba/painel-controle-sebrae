using System;

[AttributeUsage(AttributeTargets.Class)]
public class CommandAttribute : Attribute{

	public string CommandString {get;set;}

}

