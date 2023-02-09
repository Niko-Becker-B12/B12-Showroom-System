using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


[System.Serializable]
public class Function
{
    [SerializeField]
    [FoldoutGroup("Function Block")] public UnityEvent functionName;
    [FoldoutGroup("Function Block")] public float functionDelay;
}