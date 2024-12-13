using System.Collections.Generic;
using System;
using UnityEngine;

public interface IStateMachine{

  void Start() {}

  void Update() {}

  public void TransitionToState<T>(T key) where T :Enum{}
  void onTriggerEnter(Collider other) {}
  void onTriggerStay(Collider other) {}
  void onTriggerExit(Collider other) {}
}
