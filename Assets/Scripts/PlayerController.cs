using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Gyroscope gyro;
    private bool gyroEnabled;
    private SwerveInputSystem _swerveInputSystem;
    [SerializeField] private float swerveSpeed = 3.5f;
    [SerializeField] private float maxSwerveAmount = 1f;

    private float _forwardSpeed = 10f;

    private void Awake()
    {
        _swerveInputSystem = GetComponent<SwerveInputSystem>();
        gyroEnabled = EnableGyroscope();
    }

    private bool EnableGyroscope()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
        }
        return false;
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.IsGamePlaying)
        {
            SwerveMove();
        }
    }

    private void SwerveMove()
    {
        transform.Translate(Vector3.forward * (Time.fixedDeltaTime * _forwardSpeed));

        float swerveAmount = 0;

        if (gyroEnabled)
        {
            swerveAmount = Time.fixedDeltaTime * swerveSpeed * gyro.rotationRateUnbiased.y;
        }

        swerveAmount += Time.fixedDeltaTime * swerveSpeed * _swerveInputSystem.MoveFactorX;

        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);

        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x + swerveAmount, -2.5f, 2.5f);
        transform.position = newPosition;
    }
}