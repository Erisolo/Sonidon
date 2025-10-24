using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slice: MonoBehaviour {
    private AudioSource head;
    private AudioSource tail;
    private AudioSource casings;

    [SerializeField]
    private float overlapTime;
    private int overLapSampleDuration; //cuantos samples dura el overlap

    
    public AudioClip[] pcmDataHeads, pcmDataTails, pcmDataCasings;
    private int nHeads, nTails, nCasings;


    void Awake(){
        nHeads = pcmDataHeads.Length;
        nTails = pcmDataTails.Length;
        nCasings = pcmDataCasings.Length;
        head = gameObject.AddComponent<AudioSource>();        
        tail = gameObject.AddComponent<AudioSource>();
        casings = gameObject.AddComponent<AudioSource>();        
    }

    private void Start()
    {
        overLapSampleDuration = (int)(overlapTime * AudioSettings.outputSampleRate);

        for (int i = 0; i < nHeads; i++)
        {
            pcmDataHeads[i] = FadeOut(pcmDataHeads[i]);
        }
        for(int i = 0; i < nTails; i++)
            pcmDataTails[i] = FadeIn(pcmDataTails[i]);
    }

    AudioClip FadeIn(AudioClip clip)
    {
        float[] clipData = new float[clip.samples];
        clip.GetData(clipData, 0);

        for (int i = 0; i < overLapSampleDuration; i++)
        {
            clipData[i] = Mathf.Sqrt(i/overLapSampleDuration);
        }
        
        clip.SetData(clipData, 0);
        return clip;
    }
    AudioClip FadeOut(AudioClip clip)
    {
        float[] clipData = new float[clip.samples];
        clip.GetData(clipData, 0);

        int overlapStartSample = clipData.Length - overLapSampleDuration;
        if(overlapStartSample < 0) //evitamos que si la duracion del overlap es mas grande que la duracion del clip explote todo
            overlapStartSample = 0;
        
        for (int i = overlapStartSample; i < clipData.Length; i++)
        {
            clipData[i] = Mathf.Sqrt((overLapSampleDuration - i)/overLapSampleDuration);
        }
        
        clip.SetData(clipData, 0);
        return clip;
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Alpha3)) 
        {
            Shoot();
        }
    }

    void Shoot()
    {
        int h = Random.Range(0, nHeads), t = Random.Range(0, nTails), c = Random.Range(0, nCasings);
        head.clip = pcmDataHeads[h];
        tail.clip = pcmDataTails[t];
        casings.clip = pcmDataCasings[c];

        double headLength = (double)head.clip.samples / head.pitch;
        double tailLength = (double)tail.clip.samples / tail.pitch;
        

        int sRATE = AudioSettings.outputSampleRate;

        head.Play();
        tail.PlayScheduled(AudioSettings.dspTime + headLength / sRATE - overlapTime);
        casings.PlayScheduled(AudioSettings.dspTime + (headLength + tailLength) / sRATE - overlapTime*2);
    }
}
