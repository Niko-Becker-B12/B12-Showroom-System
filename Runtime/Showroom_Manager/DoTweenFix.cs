using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoTweenFix : MonoBehaviour
{


    void Start()
    {

        StartCoroutine ("ActiveDeactive_DoTween");

    }

    IEnumerator ActiveDeactive_DoTween () 
    {

        GameObject DoTween = GameObject.Find ("[DOTween]");

        if (DoTween == null)
            yield return null;

        DoTween.SetActive (false);

        yield return new WaitForSeconds (0.1f);

        DoTween.SetActive (true);

    }
}