using UnityEngine;
using System.Collections;

public class Audio_fade_manager : MonoBehaviour {

	public bool alarmMusic; //changing this triggers the fadeout
    public float volume = 1f;

	public AudioSource music_sneak;
	public AudioSource music_alarm;
	
	void Start () {
		alarmMusic = false;
		music_alarm.volume = 0f;
        music_sneak.volume = volume;
		music_sneak.Play ();

	}

	void Update () {
		if (alarmMusic) {
			if (!music_alarm.isPlaying) {
				music_alarm.Play();

			}
            music_sneak.volume -= Time.deltaTime*volume;
            music_sneak.volume = Mathf.Clamp(music_sneak.volume,0,volume);
            if (music_sneak.volume == 0f) music_sneak.Stop();

            music_alarm.volume += Time.deltaTime*volume;
            music_alarm.volume = Mathf.Clamp(music_alarm.volume, 0, volume);
            
        }
        else
        {
            if (!music_sneak.isPlaying)
            {
                music_sneak.Play();

            }
            music_alarm.volume -= Time.deltaTime*volume;
            music_alarm.volume = Mathf.Clamp(music_alarm.volume, 0, volume);
            if (music_alarm.volume == 0f) music_alarm.Stop();

            music_sneak.volume += Time.deltaTime*volume;
            music_sneak.volume = Mathf.Clamp(music_sneak.volume, 0, volume);
        }
	}
}
