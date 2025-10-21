using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityAmbientController : MonoBehaviour
{
    //sliders para controlar intensidad
    [SerializeField] [Range(0f, 1.0f)] private float Itraffic = 0.5f;
    [SerializeField] [Range(0f, 1.0f)] private float Ichatter = 0.5f;

    //sonidos de ambiente
    [SerializeField] private AudioSource traffic_pad;
    [SerializeField] private AudioSource chatter_pad;
    //Difusores trafico
    [SerializeField] private Difusor passing_;
    [SerializeField] private Difusor train_;
    [SerializeField] private Difusor horn_;
    [SerializeField] private Difusor siren_;
    //difusores conversaciones
    [SerializeField] private Difusor chatter_;
    
    //igual esta feo asi pero para conservar los valores iniciales los guardamos y al modificar la frequencia utilizamos estos con los valores de intensidad
    private (float, float) passingFreqFactor;
    private (float, float) trainFreqFactor;
    private (float, float) hornFreqFactor;
    private (float, float) sirenFreqFactor;
    private (float, float) chatterFreqFactor;

    void Start()
    {
        //nos guardamos las frequencias iniciales para hacer los calculos y no perderlas
        passingFreqFactor = (passing_.minTime, passing_.maxTime);
        trainFreqFactor = (train_.minTime, train_.maxTime);
        hornFreqFactor = (horn_.minTime, horn_.maxTime);
        sirenFreqFactor = (siren_.minTime, siren_.maxTime);
        chatterFreqFactor = (chatter_.minTime, chatter_.maxTime);
    }
    
    // Update is called once per frame
    void Update()
    {
        //controlamos sonidos de ambiente
        traffic_pad.volume = Itraffic;
        chatter_pad.volume = Ichatter;
        
        //actualizamos primero en base al estado "normal" de cada difusor y luego ya aplicamos restricciones
        
        //el volumen lo incrementamos en base a la intensidad (ya que no se especifica cuanto debe incrementarse)
        passing_.minVol = Itraffic;
        passing_.maxVol = Itraffic;
        
        train_.minVol = Itraffic;
        train_.maxVol = Itraffic;
        
        chatter_.minVol = Ichatter;
        chatter_.maxVol = Ichatter;
        
        //la frecuencia se incrementa con un menor tiempo maximo y minimo
        passing_.minTime = passingFreqFactor.Item1 - Itraffic * 10;
        passing_.maxTime = passingFreqFactor.Item2 - Itraffic * 10;
        
        train_.minTime = trainFreqFactor.Item1 - Itraffic * 10;
        train_.maxTime = trainFreqFactor.Item2 - Itraffic * 10;
        
        horn_.minTime = hornFreqFactor.Item1 - Itraffic * 10;
        horn_.maxTime = hornFreqFactor.Item2 - Itraffic * 10;
        
        siren_.minTime = sirenFreqFactor.Item1 - Itraffic * 10;
        siren_.maxTime = sirenFreqFactor.Item2 - Itraffic * 10;
        
        chatter_.minTime = chatterFreqFactor.Item1 - Itraffic * 10;
        chatter_.maxTime = chatterFreqFactor.Item2 - Itraffic * 10;
        
        //restricciones de cada difusor
        if (Itraffic < 0.2f)
        { 
            //se quita el volumen
            passing_.maxVol = 0;
            train_.maxVol = 0;
        }

        if (Itraffic < 0.5f)
        {
            //se quita el volumen
            horn_.maxVol = 0;
            siren_.maxVol = 0;
        }

        if (Ichatter < 0.5f)
        {
            chatter_.maxVol = 0;
        }
        
        
    }
}
