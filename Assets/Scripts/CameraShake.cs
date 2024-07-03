using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    public Transform cameraTransform;
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.2f;
    public float dampingSpeed = 1.0f;
    public Vector3 shakeOffset = Vector3.zero; // Offset for the shake effect
    public int shakeCount = 1; // Number of shakes

    private Vector3 initialPosition;
    public bool isShaking = false;

    void Awake()
    {
        // Ensure only one instance of the CameraShake exists
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
        initialPosition = cameraTransform.localPosition;
    }

    void Update()
    {
        if (isShaking)
        {
            StartCoroutine(Shake());
            isShaking = false;
        }
    }

    public void TriggerShake(int numberOfShakes)
    {
        shakeCount = numberOfShakes;
        isShaking = true;
    }

    private IEnumerator Shake()
    {
        int currentShake = 0;
        float elapsed = 0.0f;

        while (currentShake < shakeCount)
        {
            while (elapsed < shakeDuration)
            {
                float x = Random.Range(-1f, 1f) * shakeMagnitude + shakeOffset.x;
                float y = Random.Range(-1f, 1f) * shakeMagnitude + shakeOffset.y;

                cameraTransform.localPosition = new Vector3(x, y, initialPosition.z);

                elapsed += Time.deltaTime;
                yield return null;
            }

            elapsed = 0.0f;
            currentShake++;
        }

        cameraTransform.localPosition = initialPosition;
    }
}
