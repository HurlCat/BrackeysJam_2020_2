﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = System.Random;

public class VHSLibrary : MonoBehaviour
{
    public static VHSLibrary singleton;
    [SerializeField] private ParticleSystem _particleSystem;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _rustle;
    
    public Sprite[] Action_MovieCovers;
    public Sprite[] Horror_MovieCovers;
    public Sprite[] Mystery_MovieCovers;
    public Sprite[] Comedy_MovieCovers;
    public Sprite[] Docu_MovieCovers;
    public Sprite[] Romance_MovieCovers;

    [SerializeField]
    private int[] tapesInStock = new int[6];

    void Awake()
    {
        if(singleton != null)
            Destroy(this);
        else
            singleton = this;

        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        for (int i = 0; i < tapesInStock.Length; i++)
            tapesInStock[i] = GameController.singleton.settings.GetRandVHSBinStock();

        InvokeRepeating("Restock", 0f, GameController.singleton.settings.VHSBinRefillRateInSeconds);
    }

    private void Restock()
    {
        for (int i = 0; i < tapesInStock.Length; i++)
            tapesInStock[i] += UnityEngine.Random.Range(0, 3);
    }
    
    public List<int> GenresInStock() // returns a list of indexes of genres in stock
    {
        List<int> inStock = new List<int>();

        for(int i = 0; i < tapesInStock.Length; i++)
            if(tapesInStock[i] > 0)
                inStock.Add(i);
        
        return inStock;
    }
    
    public VHSTape GetTapeFromGenre(int genreToGet)
    {
        bool rewindProbability = UnityEngine.Random.Range(0, 100) > GameController.singleton.settings.rewindProbability
            ? true
            : false;

        tapesInStock[(int)genreToGet]--;
        _particleSystem.Play();
        Util.PlayAudio(_audioSource, _rustle);
        return new VHSTape((Genre)genreToGet, rewindProbability);
    }

    public Sprite GetRandomArtwork(Genre genre)
    {
        switch (genre)
        {
            case Genre.Action:
                if(Action_MovieCovers.Length != 0)
                    return Action_MovieCovers[UnityEngine.Random.Range(0, Action_MovieCovers.Length)];
                break;
            case Genre.Comedy:
                if(Comedy_MovieCovers.Length != 0)
                    return Comedy_MovieCovers[UnityEngine.Random.Range(0, Comedy_MovieCovers.Length)];
                break;
            case Genre.Documentary:
                if(Docu_MovieCovers.Length != 0)
                    return Docu_MovieCovers[UnityEngine.Random.Range(0, Docu_MovieCovers.Length)];
                break;
            case Genre.Horror:
                if(Horror_MovieCovers.Length != 0)
                    return Horror_MovieCovers[UnityEngine.Random.Range(0, Horror_MovieCovers.Length)];
                break;
            case Genre.Mystery:
                if(Mystery_MovieCovers.Length != 0)
                    return Mystery_MovieCovers[UnityEngine.Random.Range(0, Mystery_MovieCovers.Length)];
                break;
            case Genre.Romance:
                if(Romance_MovieCovers.Length != 0)
                    return Romance_MovieCovers[UnityEngine.Random.Range(0, Romance_MovieCovers.Length)];
                break;
        }
        return null;
    }
    
    
}
