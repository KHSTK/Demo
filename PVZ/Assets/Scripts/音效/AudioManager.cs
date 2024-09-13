using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance;

	//�������ֺ���Ч��Sound����
	public Sound[] musicSounds, sfxSounds;
	//���ֺ���Ч��AudioSource
	public AudioSource musicSource, sfxSource;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			//�ڳ����л�ʱ�����ٸö���
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	//�������ֵķ���������Ϊ��������
	public void PlayMusic(string name)
	{
		//������Sounds�������ҵ�����ƥ���Sound����
		Sound s = Array.Find(musicSounds, x => x.name == name);
		//����Ҳ�����Ӧ��Sound�����������Ϣ
		if (s == null)
		{
			Debug.Log("û���ҵ�����");
		}
		//��������Դ��clip����Ϊ��ӦSound��clip������
		else
		{
			musicSource.clip = s.clip;
			musicSource.Play();
		}
	}

	//������Ч�ķ���������Ϊ��Ч����
	public void PlaySFX(string name)
	{
		//����ЧSounds�������ҵ�����ƥ���Sound����
		Sound s = Array.Find(sfxSounds, x => x.name == name);
		//����Ҳ�����Ӧ��Sound�����������Ϣ
		if (s == null)
		{
			Debug.Log("û���ҵ���Ч");
		}
		//���򲥷Ŷ�ӦSound��clip
		else
		{
			sfxSource.PlayOneShot(s.clip);
		}
	}
}

