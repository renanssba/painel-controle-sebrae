using System;
using UnityEngine;

public class VsnOperator : VsnArgument{

  string operatorType;

  public VsnOperator(string type){
    operatorType = type;
  }


  public bool EvaluateComparison(VsnArgument first, VsnArgument second){
    if((first.GetType() == typeof(VsnNumber) && second.GetType() == typeof(VsnNumber)) ||
       (first.GetType() == typeof(VsnReference) && second.GetType() == typeof(VsnNumber)) ||
       (first.GetType() == typeof(VsnNumber) && second.GetType() == typeof(VsnReference)) ){
      return CompareFloats(first.GetNumberValue(), second.GetNumberValue());
    }

    if((first.GetType() == typeof(VsnString) && second.GetType() == typeof(VsnString)) ||
       (first.GetType() == typeof(VsnReference) && second.GetType() == typeof(VsnString)) ||
       (first.GetType() == typeof(VsnString) && second.GetType() == typeof(VsnReference)) ){
      return CompareStrings(first.GetStringValue(), second.GetStringValue());
    }

    return CompareVariables(first, second);
  }


  private bool CompareFloats(float op1, float op2){
    Debug.Log("  >Comparing floats: " + op1 + " and " + op2);

    switch(operatorType){
      case "==":
        if(op1 == op2) {
          return true;
        }
        break;
      case "!=":
        if(op1 != op2) {
          return true;
        }
        break;
      case "<=":
        if(op1 <= op2) {
          return true;
        }
        break;
      case ">=":
        if(op1 >= op2) {
          return true;
        }
        break;
      case "<":
        if(op1 < op2) {
          return true;
        }
        break;
      case ">":
        if(op1 > op2) {
          return true;
        }
        break;
    }
    return false;
  }


  private bool CompareStrings(string op1, string op2){
    Debug.Log("Comparing strings");

    switch(operatorType){
      case "==":
        if(op1 == op2) {
          return true;
        }
        break;
      case "!=":
        if(op1 != op2) {
          return true;
        }
        break;
    }
    return false;
  }


  private bool CompareVariables(VsnArgument op1, VsnArgument op2){
    /// TODO: also implement when the two variables are different types
    /// or when they're both strings

    return CompareFloats(op1.GetNumberValue(), op2.GetNumberValue());
  }
}
