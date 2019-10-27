using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class question : MonoBehaviour
{
    //public Question q;
    
    public static int num;

    public static IDictionary<int, Material> color = new Dictionary<int, Material>();
    public static IDictionary<int, Material> copyColor;
    
    public System.Random rand = new System.Random();
    // Start is called before the first frame update
    void Start()
    {   
        
        num = Random.Range(1, 5);
        //num = 1;
        Material yellowMat = Resources.Load("centerDoor", typeof(Material)) as Material;
        Material greenMat = Resources.Load("rightDoor", typeof(Material)) as Material;
        Material redMat = Resources.Load("leftDoor", typeof(Material)) as Material;
		if(color.Count()!=3){
			color.Add(1, redMat);
        color.Add(2, yellowMat);
        color.Add(3, greenMat);
		}
        

        copyColor = new Dictionary<int, Material>(color);
        //int option = rand.Next(1, copyColor.Count);
        //        C =  colorCopy.ElementAt(option).Value; 
        var cubeRenderer = GameObject.Find("door").GetComponent<Renderer>();
        var option = copyColor.ToList()[rand.Next(copyColor.Count)];
        cubeRenderer.material= option.Value;
        copyColor.Remove(option.Key);
        //var k = dict.Keys.ToList()[rand.Next(dict.Count)];
        option = copyColor.ToList()[rand.Next(copyColor.Count)];
        //option = rand.Next(0, copyColor.Count);
        //        C =  colorCopy.ElementAt(option).Value; 
        var cubeRenderer2 = GameObject.Find("door1").GetComponent<Renderer>();
        cubeRenderer2.material= option.Value;
        copyColor.Remove(option.Key);

        //option = rand.Next(0, copyColor.Count);
        //        C =  colorCopy.ElementAt(option).Value; 
        option = copyColor.ToList()[rand.Next(copyColor.Count)];
        var cubeRenderer3 = GameObject.Find("door2").GetComponent<Renderer>();
        cubeRenderer3.material= option.Value;
        copyColor.Remove(option.Key);   

    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 4);
        }
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
