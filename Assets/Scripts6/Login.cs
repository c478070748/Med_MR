using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class Login : MonoBehaviour
{

    public TextMeshProUGUI IdText, NameText;

    public static string CurFileName;

    public void OnButtonClick()
    {
        CurFileName = IdText.text + "_" + NameText.text + ".xml";
        CreateXml();
        gameObject.SetActive(false);
        //Debug.Log(gameObject.activeSelf);
    }

    private void CreateXml()
    {
        string filename = IdText.text + "_" + NameText.text + ".xml";
        //Debug.Log(filename);
        XmlDocument xml = XmlManager.GetXmlDocument(filename);

        List<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>();
        KeyValuePair<string, string> Id = new KeyValuePair<string, string>("Id", IdText.text);
        KeyValuePair<string, string> Name = new KeyValuePair<string, string>("Name", NameText.text);

        keyValuePairs.Add(Id);
        keyValuePairs.Add(Name);



        XmlManager.AddUser(filename, keyValuePairs, xml);
    }

}
