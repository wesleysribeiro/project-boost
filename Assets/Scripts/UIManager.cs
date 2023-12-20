using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Collider endingObject;
    [SerializeField] Transform targetObject;
    [SerializeField] TMP_Text distanceEndingText;
    // Start is called before the first frame update
    void Start()
    {
        endingObject = GameObject.FindWithTag(Tags.EndingObject).GetComponent<Collider>();
    }

    float getDistance()
    {
        Vector3 closesPoint = endingObject.ClosestPointOnBounds(targetObject.position);
        return Vector3.Distance(closesPoint, targetObject.position);
    }

    // Update is called once per frame
    void Update()
    {
        distanceEndingText.SetText("Distance: " + getDistance() + " m");
    }
}
