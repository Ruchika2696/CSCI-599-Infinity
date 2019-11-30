using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTutorial : Tutorial
{
	// Start is called before the first frame update
	public override void Start()
	{
		base.Start();
		displayTutorialTimer = 5.0f;
	}

	public override void checkIfHappening()
	{
		if (staticVars.doorTutorialTriggered)
		{
			//displayTutorial = true;
			//TutorialManager.Instance.completedTutorial();
		}
        else if(!staticVars.doorTutorialTriggered)
		{
			//displayTutorial = false;
		}
	}
}
