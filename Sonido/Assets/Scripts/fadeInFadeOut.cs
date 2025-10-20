using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class fadeInFadeOut : MonoBehaviour
{
    [SerializeField] private List<AudioSource> sources = new List<AudioSource>();
    private bool fading = false; //no queremos crossfade si ya lo estamos haciendo
    [SerializeField][Range(0.1f, 6.0f)]
    private float lap = 0.5f;
    private int actSource, newSource; //indices
    
    IEnumerator Crossfade()
    {
        sources[newSource].volume = 0;
        sources[newSource].Play();
        for (float t = 0 ; t < lap; t += Time.deltaTime)
        {
            sources[newSource].volume = Mathf.Sqrt(t/lap);
            sources[actSource].volume = Mathf.Sqrt((lap-t)/lap);
            yield return null;
        }
        fading = false;
        sources[actSource].Stop();
        actSource = newSource;
    }
    // Start is called before the first frame update
    void Start()
    {
        //iniciamos en la primera cancion
        actSource = 0;
        sources[0].Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!fading && Input.GetKeyDown(KeyCode.N))
        {
            fading = true;

            int seed = (int)Time.time;
            System.Random rd = new System.Random(seed);

            do //hasta que se elija una distinta a la actual
            {
                newSource = rd.Next(0, sources.Count);
            } while (actSource == newSource);
            
            //corrutiian
            StartCoroutine(Crossfade());
        }
    }
}
