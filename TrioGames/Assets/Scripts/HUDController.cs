using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] TMP_Text speedometer;
    [SerializeField] TMP_Text countdown;
    [SerializeField] TMP_Text timeCounter;

    [SerializeField] GameObject playerCar;
    CarController[] allCars;

    [SerializeField] int waitingTime = 3;

    private float timeStart = 0f;
    bool timerActive = false;
    private float currentCarSpeed;

    void Start()
    {
        timeCounter.text = timeStart.ToString("F2");
        countdown.text = waitingTime.ToString();
        allCars = FindObjectsOfType<CarController>();

        StartCoroutine(WaitForStart(waitingTime));
    }

    void Update()
    {
        if (timerActive)
        {
            timeStart += Time.deltaTime;
            timeCounter.text = timeStart.ToString("F2");
        }
    }

    private void FixedUpdate()
    {
        currentCarSpeed = playerCar.GetComponent<CarController>().GetVelocityVersusUp();
        speedometer.text = System.String.Format("{0:N0} mps", currentCarSpeed);
    }

    private IEnumerator WaitForStart(int waitTime)
    {
        //playerCar.GetComponent<CarController>().enabled = false;
        for (int i = 0; i < allCars.Length; i++)
        {
            allCars[i].enabled = false;
        }
        while (waitTime != 0)
        {
            yield return new WaitForSeconds(1);
            waitTime--;
            countdown.text = waitTime.ToString();
        }
        Destroy(countdown);
        //playerCar.GetComponent<CarController>().enabled = true;
        for (int i = 0; i < allCars.Length; i++)
        {
            allCars[i].enabled = true;
        }
        timerActive = true;
    }
}