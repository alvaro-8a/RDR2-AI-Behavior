using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{

	public static CinemachineShake Instance { get; private set; }
	private CinemachineVirtualCamera virtualCamera;
	private float shakeTimer;
	private float shakeTimerTotal;
	private float startingIntensity;

	private void Awake()
	{
		Instance = this;
		virtualCamera = GetComponent<CinemachineVirtualCamera>();
	}

	public void ShakeCamera(float intensity, float time)
	{
		CinemachineBasicMultiChannelPerlin basicMultiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

		basicMultiChannelPerlin.m_AmplitudeGain = intensity;
		startingIntensity = intensity;
		shakeTimer = time;
		shakeTimerTotal = time;
	}

	private void Update()
	{
		if (shakeTimer > 0)
		{
			shakeTimer -= Time.deltaTime;
			CinemachineBasicMultiChannelPerlin basicMultiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
			basicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, (1 - shakeTimer / shakeTimerTotal));
		}
	}
}
