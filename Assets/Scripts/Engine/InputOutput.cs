using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using UnityEngine;

public class InputOutput : MonoBehaviour { 
    // Use this for initialization
    public string DEFAULTSAVEPATH = "./SAVES/";
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    /**
     * Generates a save file with save data at a specific location on the hard disk
     * @param saveName The name of the save file
     * @param savePath The path to the save file location
     * @param saveContent The content of the save file. Content should be in XML
     * @param currentTime The current system time
     */
    public void WriteToSaveFile(string saveName, string savePath, string saveContent, string currentTime) {
        string output = "<?xml version='1.0'?><SaveName id=\"" + saveName + "\" time=\"" + currentTime + "\">" + saveContent + "</SaveName>";
        string path = savePath +  saveName + ".save";
        File.Create(path).Dispose();
        DirectoryInfo info = new DirectoryInfo(path);
        path = info.FullName;
        Debug.Log(info.FullName);
        Debug.Log(info.Exists);
        using (TextWriter textWriter = new StreamWriter(path))
        {
            textWriter.WriteLine(output);
            textWriter.Close();
        }
        Debug.Log(info.Exists);
        //System.IO.File.WriteAllText(path, output);
    }

    /**
     * Writes content to a new save file
     * @param content The content to save to the save file
     */
    public void WriteToSave(string content) {
        string currentTime = System.DateTime.Now.ToString("hh.mm.ss");
        WriteToSaveFile(currentTime, DEFAULTSAVEPATH, content, currentTime);
    }

    /**
     * Writes content to a file
     * @param fileName The name of the file
     * @param filePath The path to the file
     * @param content The content to write to the file
     */
    void WriteToFile(string fileName, string filePath, string content) {
        System.IO.File.WriteAllText(filePath + fileName, content);
    }


    /**
     * Reads an XML formatted save file
     * @param saveFileName The name of the file to read from the save location
     */
    public void ReadFromSaveFile(string saveFileName) {
        string path = DEFAULTSAVEPATH + saveFileName;
        XmlDocument document = new XmlDocument();
        document.Load(path);

        XmlNodeList saveName = document.GetElementsByTagName("SaveName");
        XmlNodeList player = document.GetElementsByTagName("Player");
        XmlNodeList level = document.GetElementsByTagName("Level");

        if(saveName.Count > 0) {
            Debug.Log("Inner Text: " + saveName[0].InnerText);
            Debug.Log("Inner XML: " + saveName[0].InnerXml);
            Debug.Log("Attributes: " + saveName[0].Attributes);
        }
        if (player.Count > 0) {
            Debug.Log("Inner Text: " + player[0].InnerText);
            Debug.Log("Inner XML: " + player[0].InnerXml);
            Debug.Log("Attributes: " + player[0].Attributes);
        }
        if (level.Count > 0) {
            Debug.Log("Inner Text: " + level[0].InnerText);
            Debug.Log("Inner XML: " + level[0].InnerXml);
            Debug.Log("Attributes: " + level[0].Attributes);
        }
    }
}