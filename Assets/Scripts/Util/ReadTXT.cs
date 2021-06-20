using UnityEngine;
using System.Xml;


public class ReadTXT : MonoBehaviour {
    XmlDocument xmlDoc;
    private TextAsset asset;
    private string textasset, tam;
    int[,] list = new int[3, 3];
    int x = 0;
    int y = 0;

	void Start () {
        tam = null;
        asset = Resources.Load<TextAsset>("New");
        textasset = asset.text;
        for (int i = 0; i < textasset.Length; i++)
        {
            //print(textasset[i]);

            if (textasset[i].ToString() != " " && textasset[i].ToString() != "\n"&&textasset[i].ToString()!="]"&&textasset[i].ToString()!="[")
            {
                tam += textasset[i].ToString();
            }
            else
            {
                if (tam != null)
                {
                    //print(tam);
                    loadlist(System.Convert.ToInt32(tam));
                }
                
                tam = null;
            }
            
        }
        //LoadXMLData();
        //print(tam);
	}
    void loadlist(int val)
    {
       // print(x + "_" + y);
        list[x,y] = val;
        
        print(list[x, y]);
        if (x < 2)
        {
            if (y < 2)
            {
                y++;
            }
            else
            {
                x++;
                y = 0;
            }
        }
    }
 
	// Update is called once per frame
	void Update () {
	    
	}
    private void LoadXMLData()
    {
        xmlDoc = new XmlDocument();
        TextAsset taXMl = Resources.Load<TextAsset>("keyCode");
        xmlDoc.LoadXml(taXMl.text);
        //print ("Content: " + taXMl.text);
        foreach (XmlElement nodeCode in xmlDoc.SelectNodes("KEY_CODE/Code"))
        {
            print(nodeCode.GetAttribute("name"));
        }
    }
}
