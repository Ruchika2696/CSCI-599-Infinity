using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovementsTutorial : Tutorial
{
	private Transform player_trans;

    // Start is called before the first frame update
    public override void Start()
    {
		base.Start();
		player_trans = GameObject.FindGameObjectWithTag("player").transform;

		displayTutorialTimer = 0.0f;
	}

	// Update is called once per frame
	void Update()
    {
        
    }

	public override void checkIfHappening()
	{
	    if(player_trans.position.z > 15)
		{
			TutorialManager.Instance.completedTutorial();
		}
	}
}
