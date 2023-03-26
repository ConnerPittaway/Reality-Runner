using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCredits : MonoBehaviour
{
    //Poixone
    public void OpenPoixoneYouTube()
    {
        Application.OpenURL("https://www.youtube.com/@poixone");
    }

    public void OpenPoixoneSoundcloud()
    {
        Application.OpenURL("https://soundcloud.com/poixone");
    }

    public void OpenPoixoneSpotify()
    {
        Application.OpenURL("https://open.spotify.com/artist/1vXltCW0Wqb52h9dV8KoPV?autoplay=true");
    }

    //SVRGE
    public void OpenSVRGEYouTube()
    {
        Application.OpenURL("https://www.youtube.com/channel/UCjRkBjbbkXF4jQf9DJkKk7Q");
    }

    public void OpenSVRGESoundcloud()
    {
        Application.OpenURL("https://soundcloud.com/svrge_official");
    }

    public void OpenSVRGESpotify()
    {
        Application.OpenURL("https://open.spotify.com/artist/4Qv9cmrt7qbn04iMQN7o5W");
    }

    void OnEnable()
    {
        EventManager.OnUIElementOpened();
    }
}
