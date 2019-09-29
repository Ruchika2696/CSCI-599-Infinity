using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class question : MonoBehaviour
{
    //public Question q;
    public static IDictionary<int, Question> dict = new Dictionary<int, Question>();
    public static int num;
    // Start is called before the first frame update
    void Start()
    {
        dict.Add(1, new Question("5 x 2", new List<string>() {"10" , "12" , "15"}, "10"));
        dict.Add(2, new Question("3 x 4", new List<string>() { "10", "12", "15" }, "12"));
        dict.Add(3, new Question("7 x 6", new List<string>() { "40", "42", "36" }, "42"));
        dict.Add(4, new Question("1 x 0", new List<string>() { "10", "1", "0" }, "0"));
        dict.Add(5, new Question("4 x 8", new List<string>() { "32", "22", "34" }, "32"));
        num = Random.Range(1, 5);
        //num = 1;
        TextMeshProUGUI textmeshPro = GameObject.Find("Canvas/Text1").GetComponent<TextMeshProUGUI>();

        //q = new Question(dict[num].getQuestion, dict[num].get);
        textmeshPro.text = dict[num].getQuestion() + " = ? ";
        //doorScript ds = new doorScript(dict[num]);
        List<string> optionsCopy = dict[num].getOptions();

        int option = Random.Range(0, 3);
        GameObject.Find("door/Canvas/Button").GetComponentInChildren<Text>().text = optionsCopy[option];

        optionsCopy.Remove(optionsCopy[option]);

        option = Random.Range(0, 2);
        GameObject.Find("door1/Canvas/Button").GetComponentInChildren<Text>().text = optionsCopy[option];

        optionsCopy.Remove(optionsCopy[option]);


        

        GameObject.Find("door2/Canvas/Button").GetComponentInChildren<Text>().text = optionsCopy[0];



    }

    // Update is called once per frame
    void Update()
    {
		GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 4);
	}

    public Question getQuestion()
    {
        return dict[num];
    }
}

public class Question
{
    private string quest;
    private List<string> options;
    private string ans;


    public Question(string question, List<string> options, string ans)
    {
        this.quest = question;
        this.options = options;
        this.ans = ans;
    }

    public string getQuestion()
    {
        return this.quest;
    }

    public List<string> getOptions()
    {
        return this.options;
    }

    public string getAns()
    {
        return this.ans;
    }
    
}
