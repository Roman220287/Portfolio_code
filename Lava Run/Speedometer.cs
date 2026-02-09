using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Speedometer : MonoBehaviour
{
    public Car car;

    private float _maxSpeedAngle = -20;
    private float _zeroSpeedAngle = 210;

    private Transform _needleTransform;
    private Transform _speedlabelTemplateTransform;

    private float _speedMax;
    private float _speed;
    void Start()
    {
        _needleTransform = transform.Find("needle");
        _speedlabelTemplateTransform = transform.Find("speedlabelTemplate");
        _speedlabelTemplateTransform.gameObject.SetActive(false);

        _speed = 0f;
        _speedMax = car.MaxSpeed;

        CreateSpeedLabels();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        { 
            _speed += car.currentSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _speed -= car.currentSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _speed -= car.currentSpeed * Time.deltaTime;
        }

        if (_speed > _speedMax)
        {
            _speed = _speedMax;
        }

        _needleTransform.eulerAngles = new Vector3(0, 0, GetSpeedRotation());

        _speed = Mathf.Clamp(_speed, 0f, _speedMax);
    }

    private void CreateSpeedLabels()
    {
        int labelAmount = 10;

        float totalAngleSize = _zeroSpeedAngle - _maxSpeedAngle;

        for (int i = 0; i <= labelAmount; i++)
        {
            Transform speedLabelTransform = Instantiate(_speedlabelTemplateTransform, transform);
            float labelSpeedNormalized = (float)i / labelAmount;
            float speedLabelAngle = _zeroSpeedAngle - labelSpeedNormalized * totalAngleSize;
            speedLabelTransform.eulerAngles = new Vector3(0, 0, speedLabelAngle);
            speedLabelTransform.Find("speedText").GetComponent<TMP_Text>().text = Mathf.RoundToInt(labelSpeedNormalized * _speedMax).ToString();
            speedLabelTransform.Find("speedText").eulerAngles = Vector3.zero;
            speedLabelTransform.gameObject.SetActive(true);
        }
    }

    private float GetSpeedRotation()
    {
        float totalAngleSize = _zeroSpeedAngle - _maxSpeedAngle;

        float speedNormalized = _speed / _speedMax;

        return _zeroSpeedAngle - speedNormalized * totalAngleSize;
    }
}
