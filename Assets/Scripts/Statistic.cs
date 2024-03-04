using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Statistic : MonoBehaviour
{
    [SerializeField] private TMP_Text engineForceText;
    [SerializeField] private TMP_Text heightText;
    [SerializeField] private TMP_Text speedText;

    private Rigidbody rb;
    private HelicopterController controller;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<HelicopterController>();
    }
    private void Update()
    {
        engineForceText.text = $"RPM : {controller.currentRPM:F0}";
        heightText.text = $"Height : {transform.position.y:F2} m";
        speedText.text = $"Speed : {rb.velocity * 3.6f:F1} km/h";
    }
}
