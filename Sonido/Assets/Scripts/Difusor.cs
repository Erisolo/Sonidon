using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difusor : MonoBehaviour
{
    [SerializeField]
    private List<AudioSource> sources = new List<AudioSource>();
    [SerializeField]
    private int polifonia;
    [Range(0f, 1f)]
    public float minVol, maxVol;  // volumenes máximo y mínimo establecidos y volumen origintal del source
    [SerializeField] [Range(0f, 1f)]
    private float pitchVar;
    [Range(0f, 30f)]
    public float minTime, maxTime;  // intervalo temporal de lanzamiento
    [Range(0, 50)]
    public int minPan, maxPan;   // 
    [Range(0f, 1.1f)]
    public float spatialBlend;
    public AudioClip[] pcmData;
    public bool enablePlayMode;


    void Start()
    {
        for (int i = 0; i < polifonia; i ++ )
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.playOnAwake = false;
            newSource.loop = false;
            newSource.volume = 0f;
            sources.Add(newSource);
        }
        
        if ( enablePlayMode ) 
            StartSound();
    }



    IEnumerator Waitforit(AudioSource source)
    {
        // tiempo de espera aleatorio en el intervalo [minTime,maxTime]
        float waitTime = Random.Range(minTime, maxTime);
        Debug.Log(waitTime);

        // miramos si hay un clip asignado al source (sirve para la primera vez q se ejecuta)
        if (source.clip == null)
            // waitfor seconds suspende la coroutine durante waitTime
            yield return new WaitForSeconds(waitTime);

        // cuando hay clip se añade la long del clip + el tiempo de espera para esperar entre lanzamientos
        else
            yield return new WaitForSeconds(source.clip.length + waitTime);

        // si esta activado reproducimos sonido
        if (enablePlayMode) PlaySound(source);
    }

    void PlaySound(AudioSource source)
    {
        SetSourceProperties(pcmData[Random.Range(0, pcmData.Length)], source);
        source.Play();
        //Debug.Log("back in it");
        StartCoroutine(Waitforit(source));
    }



    public void SetSourceProperties(AudioClip audioData, AudioSource source)
    {
        source.loop = false;
        //_Speaker01.maxDistance = maxDist - Random.Range(0f, distRand);
        source.spatialBlend = spatialBlend;
        source.clip = audioData;
        source.pitch = Random.Range(1 - pitchVar, 1 + pitchVar);
        source.panStereo = Random.Range(minPan, maxPan);
        source.volume = Random.Range(minVol, maxVol);
    }



    //estos métodos son usados para desde fuera activar o desactivar el sonido
    void StopSound()
    {
        enablePlayMode = false;
    }

    public void StartSound()
    {
        enablePlayMode = true;
        for(int i = 0; i < polifonia; i++)
        {
            StartCoroutine(Waitforit(sources[i]));
        }
    }
}
