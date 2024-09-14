using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	public Slider _musicSlider, _sfxSlider;

	//�л����־���״̬�ķ���
	public void ToggleMusic()
	{
		AudioManager.Instance.ToggleMusic();
	}

	//�л���Ч����״̬�ķ���
	public void ToggleSFX()
	{
		AudioManager.Instance.ToggleSFX();
	}

	//�������������ķ���
	public void MusicVolume()
	{
		AudioManager.Instance.MusicVolume(_musicSlider.value);
	}

	//������Ч�����ķ���
	public void SFXVolume()
	{
		AudioManager.Instance.SFXVolume(_sfxSlider.value);
	}
}

