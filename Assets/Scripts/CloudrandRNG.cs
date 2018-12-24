using System.Collections;
using System;
using System.Security.Cryptography;
using System.IO;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CloudrandRNG : MonoBehaviour
{

    public RawImage webcamImage;
    public TextMeshProUGUI outputText;

    private int minumum;
    private int maximum;

    private WebCamTexture webCam;
    //Called the first fram before Update
    void Start()
    {
        webCam = new WebCamTexture();
        webCam.Play();

        webcamImage.texture = webCam;
    }

    //Called every single frame after Start
    void Update()
    {
        webcamImage.rectTransform.localEulerAngles = new Vector3(0,0,-webCam.videoRotationAngle);
    }

    //Called when the "Generate" button is clicked
    public void Generate()
    {
        outputText.SetText($"Output: {TrulyRandomNumber()}");
    }
    //Generates a Truly Random Number from an Image
    private int TrulyRandomNumber()
    {
        using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
        {
            Texture2D texture = new Texture2D(webcamImage.texture.width, webcamImage.texture.height, TextureFormat.ARGB32, false);
            texture.SetPixels(webCam.GetPixels());
            texture.Apply();

            byte[] tokenData = texture.GetRawTextureData();
            rng.GetBytes(tokenData);

            var token = BitConverter.ToUInt32(tokenData, 0);
            return (int)(minumum + (maximum - minumum) * (token / (double)uint.MaxValue));
        }
    }
    //Called when the "Minimum Value" input text value is changed
    public void SetMinimumValue(string min)
    {
        minumum = Int32.Parse(min);
    }
    //Called when the "Maximum Value" input text value is changed
    public void SetMaximumValue(string max)
    {
        maximum = Int32.Parse(max);
    }
}
