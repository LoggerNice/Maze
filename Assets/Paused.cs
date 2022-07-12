using UnityEngine;

public class Paused : MonoBehaviour
{
    [SerializeField] GameObject pause;
    [SerializeField] AudioSource music;

    void Start()
    {
        pause.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            music.Pause();
            pause.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void PauseOff()
    {
        music.Play();
        pause.SetActive(false);
        Time.timeScale = 1;
    }

    public void Exite()
    {
        music.Stop();
        Application.Quit();
    }
}
