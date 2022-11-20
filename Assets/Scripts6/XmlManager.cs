using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class XmlManager : MonoBehaviour
{
    private static XmlDocument CreateXmlDocument(string path)
    {
        XmlDocument xml = new XmlDocument();
        XmlDeclaration xmldecl = xml.CreateXmlDeclaration("1.0", "UTF-8", "");//设置xml文件编码格式为UTF-8
        XmlElement root = xml.CreateElement("root");//创建根节点
        xml.AppendChild(root);
        xml.Save(path);//保存xml到路径位置
        return xml;
    }

    public static XmlDocument GetXmlDocument(string filename)
    {
        
        string localPath = UnityEngine.Application.streamingAssetsPath + "/" + filename;
        
        if (File.Exists(localPath))
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(localPath);
            return xml;
        }
        else
        {
            return CreateXmlDocument(localPath);
        }
    }

    public static void AddUser(string filename, List<KeyValuePair<string,string>> keyValuePairs, XmlDocument xml = null)
    {
        string localPath = UnityEngine.Application.streamingAssetsPath + "/" + filename;
        if (xml == null)
        {
            xml = new XmlDocument();
            xml.Load(localPath);
        }

        XmlNode root = xml.SelectSingleNode("root");

        XmlNodeList xmlNodeList = root.ChildNodes;
        

        //判断是否已经注册
        foreach(XmlElement xmlElement in xmlNodeList)
        {
            if (xmlElement.GetAttribute(keyValuePairs[0].Key) == keyValuePairs[0].Value)
                return;
        }


        XmlElement user = xml.CreateElement("user");
        foreach(KeyValuePair<string,string> keyValuePair in keyValuePairs)
        {
            user.SetAttribute(keyValuePair.Key, keyValuePair.Value);
        }

        root.AppendChild(user);
        xml.Save(localPath);
    }

    public static void AddTest(string filename, List<KeyValuePair<string, string>> TestAtrrs, List<List<KeyValuePair<string, string>>> ItemLists,XmlDocument xml = null)
    {
        string localPath = UnityEngine.Application.streamingAssetsPath + "/" + filename;
        if(xml == null)
        {
            xml = new XmlDocument();
            xml.Load(localPath);
        }

        XmlNode root = xml.SelectSingleNode("root");
        XmlNodeList rootChilds = root.ChildNodes;
        foreach(XmlElement user in rootChilds)
        {
            XmlElement test = xml.CreateElement("test");
            foreach(KeyValuePair<string,string> TestAtrr in TestAtrrs)
            {
                test.SetAttribute(TestAtrr.Key, TestAtrr.Value);
            }


            foreach(List<KeyValuePair<string, string>> ItemList in ItemLists)
            {
                XmlElement item = xml.CreateElement("item");
                foreach(KeyValuePair<string,string> Item in ItemList)
                {
                    item.SetAttribute(Item.Key, Item.Value);
                }

                test.AppendChild(item);
            }

            user.AppendChild(test);
        }

        xml.Save(localPath);
    }
}
