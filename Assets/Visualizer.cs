using UnityEngine;
using System.Collections;

public class Visualizer : MonoBehaviour {

	public int samples = 64;
	public int channel = 0;
	public FFTWindow window = FFTWindow.Hamming;
	public static float[] data;

	// Use this for initialization
	void Start () {
		data = new float[samples];
	}
	
	// Update is called once per frame
	void Update () {
	
		data = new float[samples];
		AudioListener.GetSpectrumData (data, channel, window);
		//Debug.Log (data[10]);
	}


}
