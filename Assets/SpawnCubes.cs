using UnityEngine;
using System.Collections;

public class SpawnCubes : MonoBehaviour {

	public int number;
	public float spread;
	public GameObject cube;
	private GameObject[] cubeArray;
	public float vectorSize = 0.5f;
	public float multiplier = 6;
	public Camera camera;
	public float ampDecay = 0.1f;
	public float ampCap = 5.0f;

	// Use this for initialization
	void Start () {
		cubeArray = new GameObject[number];
		Vector3 pos = transform.position;
		pos.x -= (spread-1)/2;
		for (int i = 0; i < number; i++){

			GameObject newCube = Instantiate(cube, pos, cube.transform.rotation) as GameObject;
			cubeArray[i] = newCube;
			newCube.transform.parent = transform;
			pos.x += (spread/number);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Visualizer v = camera.GetComponent("Visualizer") as Visualizer;
		int samples = v.samples;
		int length = cubeArray.Length;
		float[] numbers = new float[length];
		int count = 0;
		for(int i = 1; i <= length; i++){
			count += i;
		}
		for(int i = 1; i <= length; i++){
			numbers[i-1] = (i*samples)/count;
		}


		int counter = 0;
		for(int i = 0; i < length; i++){


			float numberOfSamples = samples/length;
			float amplitude = 0;

			float numberSamplesf = numbers[i];
			int numberSamples = (int)numberSamplesf;
			for (int j = 0; j < numberSamples; j++){
				float arrayIndexf = counter + j;
				int arrayIndex = (int)arrayIndexf;
				if(arrayIndex < samples){
					amplitude += Visualizer.data[arrayIndex];
				}
			}

			counter += numberSamples;
			//Debug.Log (amplitude*multiplier);
			float currAmp = cubeArray[i].transform.localScale.y;
			float finalAmp = amplitude*(multiplier+(i+1));
			if(finalAmp > ampCap){
				finalAmp = ampCap;
			}
			if(finalAmp <= currAmp){
				float value = 0;
				if(currAmp - finalAmp > ampDecay){
					value = currAmp - ampDecay;
				}
				else{
					value = finalAmp;
				}
				cubeArray[i].transform.localScale = new Vector3(vectorSize,value,vectorSize);
			}
			else{
				cubeArray[i].transform.localScale = new Vector3(vectorSize,finalAmp,vectorSize);
			}
		}
	}
}
