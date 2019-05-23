using System;

public class VsnNumber : VsnArgument{

  protected float value;

	public VsnNumber(float number){
		value = number;
  }

  public override float GetNumberValue(){
    return value;
  }
}
