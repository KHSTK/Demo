using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	public Slider _musicSlider, _sfxSlider;

	//切换音乐静音状态的方法
	public void ToggleMusic()
	{
		AudioManager.Instance.ToggleMusic();
	}

	//切换音效静音状态的方法
	public void ToggleSFX()
	{
		AudioManager.Instance.ToggleSFX();
	}

	//设置音乐音量的方法
	public void MusicVolume()
	{
		AudioManager.Instance.MusicVolume(_musicSlider.value);
	}

	//设置音效音量的方法
	public void SFXVolume()
	{
		AudioManager.Instance.SFXVolume(_sfxSlider.value);
	}
}

