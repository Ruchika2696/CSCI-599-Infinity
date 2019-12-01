using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
	public int order;

    [TextArea(3,10)]
	public string explanation;
	public float displayTutorialTimer;

	public virtual void Start()
	{
		TutorialManager.Instance.tutorials.Add(this);
	}

	public virtual void checkIfHappening() { }
}
