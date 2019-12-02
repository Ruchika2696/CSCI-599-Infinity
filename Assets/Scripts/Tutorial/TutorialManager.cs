using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{

	public List<Tutorial> tutorials = new List<Tutorial>();

	public Text explanationText;
	public GameObject text;
	SimpleTimer st;
	public bool displayTutorial;

	private static TutorialManager instance;
	public static TutorialManager Instance
	{
        get
		{
			if (instance == null)
				instance = GameObject.FindObjectOfType<TutorialManager>();

			if (instance == null)
				Debug.Log("No TutorialManager");

			return instance;
		}
	}

	private Tutorial currentTutorial;
		
    // Start is called before the first frame update
    void Start()
    {
		setNextTutorial(0);
		text = GameObject.Find("Exp Text");
    }

    // Update is called once per frame
    void Update()
    {
		if (currentTutorial)
			currentTutorial.checkIfHappening();

        if (displayTutorial)
		    text.gameObject.SetActive(true);
        else
			text.gameObject.SetActive(false);
	}

    public void completedTutorial()
	{
		setNextTutorial(currentTutorial.order + 1);
	}

    public void setNextTutorial(int currentOrder)
	{
        // get the next tutorial
		currentTutorial = getTutorialByOrder(currentOrder);

        if(!currentTutorial)
		{
			// finished all tutorials
			completedAllTutorials();
			return;
		}

		explanationText.text = currentTutorial.explanation;

		if (currentTutorial.displayTutorialTimer > 0.0f)
		{
			st = new SimpleTimer(null, currentTutorial.displayTutorialTimer);
			StartCoroutine(st.DisplayTutorial());
		}
		else
		{
            // not using the timer to control tutorial display
			displayTutorial = true;
		}
	}

    public void completedAllTutorials()
	{
		explanationText.text = "";
	}

    public Tutorial getTutorialByOrder(int order)
	{
        for(int i=0; i<tutorials.Count; i++)
		{
            if(tutorials[i].order == order)
			{
				return tutorials[i];
			}
		}

		return null;
	}
}
