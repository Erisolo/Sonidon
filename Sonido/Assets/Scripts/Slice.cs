using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchedEvent: MonoBehaviour {
    private AudioSource head;
    private AudioSource tail;
    private AudioSource casings;

    [SerializeField]
    private float overlapTime;

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
        for (int i = 0; i< pcmDataHeads.Length; i++) 
        {
        }
        for (int i = 0; i< pcmDataHeads.Length; i++) 
        {
        }
        for (int i = 0; i< pcmDataHeads.Length; i++) 
        {
        }
    }

    void FadeIn(AudioClip clip)
    {

    }
    void FadeOut(AudioClip clip)
    {

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
