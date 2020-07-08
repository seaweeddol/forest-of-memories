using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
  using System;
  using System.IO;
using Newtonsoft.Json;

public class ToneAnalyzer : MonoBehaviour
{
    // https://json2csharp.com/json-to-csharp
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
    public GameObject inputField;
    public GameObject toneScoreDisplay;
    public GameObject toneSentimentDisplay;

    private ToneResponse GetToneAnalysis() {
        string userMemoryInput = inputField.GetComponent<Text>().text;

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("https://api.us-south.tone-analyzer.watson.cloud.ibm.com/instances/fcea7060-3962-4a9b-b48f-054d1e6909e4/v3/tone?version=2017-09-21&sentences=false&text={0}", userMemoryInput));

        // set up Authorization header
        string username = "apikey";
        string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + API_KEY));
        request.Headers.Add("Authorization", "Basic " + encoded);

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();

        // https://www.newtonsoft.com/json/help/html/DeserializeObject.htm
        ToneResponse toneResponse = JsonConvert.DeserializeObject<ToneResponse>(jsonResponse); 

        // ToneResponse info = JsonUtility.FromJson<ToneResponse>(jsonResponse);
        return toneResponse;
    }

    public void CheckEnterPress() {
        if(Input.GetKeyDown("return")){
            DisplayToneResponse();
        }
    }

    public void DisplayToneResponse() {
        DocumentTone toneAnalysis = GetToneAnalysis().document_tone;
        double score = toneAnalysis.tones[0].score;
        string tone = toneAnalysis.tones[0].tone_name;

        // TODO: clear input field
        
        // if there are multiple responses, get the highest scored one

        // intead of displaying text, the score and tone should be used to create a tree (create a CreateTree script?)
        toneScoreDisplay.GetComponent<Text>().text = $"Score: {score.ToString()}";
        toneSentimentDisplay.GetComponent<Text>().text = $"Sentiment: {tone}";
    }
}
