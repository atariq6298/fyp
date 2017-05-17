using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public interface TimedInputHandler : IEventSystemHandler {

	void HandleTimedInput();
}
