using SFML.Audio;

public class AudioClip
{
    public bool playing { get; private set; }

    private Sound sound;
    private SoundBuffer soundBuffer;
    
    public Action<SoundBuffer> OnPlayed;
    public Action<SoundBuffer> OnStopped;
    public Action<SoundBuffer> OnPaused;

    public AudioClip(SoundBuffer soundBuffer)
    {
        this.soundBuffer = soundBuffer;
        OnStopped += (_) => playing = false;
        OnPaused += (_) => playing = false;
        OnPlayed += (_) => playing = true;
    }
    public void Play()
    {
        sound = new(soundBuffer);
        sound.Play();
        
        OnPlayed?.Invoke(soundBuffer);
    }
    public void Update()
    {
        switch (sound.Status)
        {
            case SoundStatus.Stopped:
                Stop();
                break;
            case SoundStatus.Paused:
                OnPaused?.Invoke(soundBuffer);
                break;
        }
    }
    public void Stop()
    {
        sound.Stop();
        
        OnStopped?.Invoke(soundBuffer);
    }
}