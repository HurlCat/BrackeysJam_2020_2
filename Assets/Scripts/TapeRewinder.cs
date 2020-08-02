﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeRewinder : MonoBehaviour
{
    private VHSTape _currentTape;
    private bool _rewinding = false;
    private bool _tapeInside = false;

    [SerializeField] private float _rewindTime = 10f;
    private float _rewindTimer;

    private void Update()
    {
        if (!_rewinding) // if we aren't rewinding
            return;

        _rewindTimer += Time.deltaTime;
        if (_rewindTimer > _rewindTime) // when done rewinding
        {
            _rewindTimer = 0f;

            _rewinding = false; // allow tape to be grabbed
            Debug.Log("DING!");
        }
    }

    public void InsertTape(VHSTape tape)
    {
        tape.rewound = true;
        _currentTape = tape;
        
        _rewinding = true;
        _tapeInside = true;
    }

    public VHSTape GiveTapeToPlayer()
    {
        _tapeInside = false;
        _rewinding = false;
        
        return _currentTape;
    }

    public bool IsRewinding() => _rewinding;
    public bool IsFull() => _tapeInside;
}
