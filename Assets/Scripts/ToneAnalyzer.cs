using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
  using System;
  using System.IO;
using Newtonsoft.Json;
using System.Linq;

public class ToneAnalyzer : MonoBehaviour
{
    // https://json2csharp.com/json-to-csharp
    // set up classes for JSON response from API
    public class Tone    {
        public double score { get; set; } 
        public string tone_id { get; set; } 
        public string tone_name { get; set; } 
    }

    public class DocumentTone    {
        public List<Tone> tones { get; set; } 
    }

    public class ToneResponse    {
        public DocumentTone document_tone { get; set; } 
    }

    public string API_KEY;
    public GameObject memoryUI;
    public InputField inputField;
    public SpawnTree spawnTree;
    string userMemoryInput;

    private ToneResponse GetToneAnalysis() {
        userMemoryInput = inputField.text;

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("https://api.us-south.tone-analyzer.watson.cloud.ibm.com/instances/fcea7060-3962-4a9b-b48f-054d1e6909e4/v3/tone?version=2017-09-21&sentences=false&text={0}", userMemoryInput));

        // set up Authorization header
        string username = "apikey";
        string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + API_KEY));
        request.Headers.Add("Authorization", "Basic " + encoded);

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        Debug.Log(jsonResponse);

        // https://www.newtonsoft.com/json/help/html/DeserializeObject.htm
        ToneResponse toneResponse = JsonConvert.DeserializeObject<ToneResponse>(jsonResponse); 

        // ToneResponse info = JsonUtility.FromJson<ToneResponse>(jsonResponse);
        return toneResponse;
    }

    // if user presses enter while in the input field, call DisplayToneResponse()
    public void CheckEnterPress() {
        if(Input.GetKeyDown("return")){
            DisplayToneResponse();
        }
    }

    public void DisplayToneResponse() {
        DocumentTone toneAnalysis = GetToneAnalysis().document_tone;
        double score;
        string strongestTone;
        List<string> allTones = new List<string>(); 

        // TODO: send back full list of tones

        // determine strongest tone
        if (toneAnalysis.tones.Count == 0) {
            // no overt tones detected - neutral
            allTones.Add("Neutral");
            score = 0;
            strongestTone = "Neutral";
        } else if (toneAnalysis.tones.Count == 1) {
            // one tone detected
            allTones.Add("Neutral");
            score = toneAnalysis.tones[0].score;
            strongestTone = toneAnalysis.tones[0].tone_name;
        } else {
            // multiple tones detected, grab highest scoring one
            foreach(Tone tone in toneAnalysis.tones) {
                allTones.Add(tone.tone_name);
            }
            Tone highestScore = toneAnalysis.tones.OrderByDescending(p => p.score).FirstOrDefault();
            score = highestScore.score;
            strongestTone = highestScore.tone_name;
        }

        inputField.text = "";
        memoryUI.SetActive(false);
        spawnTree.CreateTree(strongestTone, score, userMemoryInput, allTones);
    }
}
