using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PauseOS_Controller : MonoBehaviour
{
    [Header("State")]
    public bool isOpen = true;
    public bool isFullyOpen = false;
    public bool isAiming = false;
    public bool canChangeState = true;

    [Header("References")]
    public CanvasGroup cg;
    public SpriteRenderer ocRend;
    public ParticleSystem backgroundParticles;

    // Global Volumes
    public VolumeProfile volumeProfile;
    private VolumeProfile nativeProfile;

    private Volume globalVolume;

    // Cinemachine
    public CinemachineVirtualCamera vCam;
    public Transform playerT;

    private KeycodeDatabase keycodeDatabase;
    private Dictionary<int, KeyCode> keybinds;

    // Options
    private PauseOS_MenuSelection options;

    // PlayerMovement
    private PlayerMovement playerMovement;

    [Header("Config")]
    public float updateRate = 1.1f;
    private float lastUpdate = 0f;
    public float defaultFixedDeltaTime;

    [Header("Animation")]
    public List<Sprite> openclose;
    public List<Sprite> closeopen;
    public float ocAnimTime;

    void Awake()
    {
        ocRend.sprite = openclose[0];
        cg.alpha = 0f;
        backgroundParticles.Stop();

        globalVolume = GameObject.Find("Enviornmental Effects").GetComponent<Volume>();
        nativeProfile = globalVolume.profile;

        options = GameObject.Find("Menu Selection").GetComponent<PauseOS_MenuSelection>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // Update Fixed Delta Time Along With Time.time
        Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;

        // Instantiate The Database
        if (keycodeDatabase == null)
        {
            keycodeDatabase = GameObject.Find("KeyCode Database").GetComponent<KeycodeDatabase>();
        }
        this.keybinds = keycodeDatabase.GetFullDictionary();

        // Switch
        if (isOpen && Input.GetKey(keybinds[9]) && Time.realtimeSinceStartup > updateRate + lastUpdate && !isAiming && canChangeState)
        {
            isOpen = false;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.osClose, this.transform.position);
            AudioManager.instance.SetMusicTrackByName("osTrack");

            StartCoroutine(OCAnim(true));
            StartCoroutine(ReloadOS(1f));
            lastUpdate = Time.realtimeSinceStartup;
        }
        else if (!isOpen && Input.GetKey(keybinds[9]) && Time.realtimeSinceStartup > updateRate + lastUpdate && !isAiming && canChangeState)
        {
            isOpen = true;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.osOpen, this.transform.position);
            AudioManager.instance.SetMusicTrackByName("defaultTrack");
            /*vCam.enabled = false;
            vCam.transform.SetPositionAndRotation(playerT.position, playerT.rotation);
            vCam.enabled = true;*/

            StartCoroutine(OCAnim(false));
            StartCoroutine(ReloadOS(0f));
            lastUpdate = Time.realtimeSinceStartup;
        }

        //Debug.Log(Time.timeScale.ToString());
    }

    IEnumerator ReloadOS(float currentTime)
    {
        if (isOpen)
        {
            if (Time.timeScale < 0.985)
            {
                //Debug.Log("Current Time: " + currentTime);
                Time.timeScale = currentTime;
                yield return new WaitForSecondsRealtime(ocAnimTime / 100);

                backgroundParticles.Stop();
                backgroundParticles.Clear();
                cg.alpha = 0f;
                globalVolume.profile = nativeProfile;
                options.ResetSelected();
                playerMovement.canMove = true;
                isFullyOpen = false;

                currentTime = currentTime + 0.01f;
                StartCoroutine(ReloadOS(currentTime));
            }
            else
            {
                Time.timeScale = 1f;
                //vCam.enabled = true;
                yield break;
            }
        }
        else if (!isOpen)
        {
            if (Time.timeScale > 0.015)
            {
                //Debug.Log("Current Time: " + currentTime);
                Time.timeScale = currentTime;
                yield return new WaitForSecondsRealtime(ocAnimTime / 100);

                currentTime = currentTime - 0.01f;
                StartCoroutine(ReloadOS(currentTime));
            }
            else
            {
                Time.timeScale = 0f;
                cg.alpha = 1f;
                backgroundParticles.Play();
                globalVolume.profile = volumeProfile;
                options.ResetSelected();
                playerMovement.canMove = false;
                isFullyOpen = true;

                yield break;
            }
        }

        //isOpenUpdated = false;
        yield return new WaitForSecondsRealtime(0.01f);
    }

    public void resume()
    {
        isOpen = true;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.osClose, this.transform.position);

        StartCoroutine(OCAnim(false));
        StartCoroutine(ReloadOS(0f));
        lastUpdate = Time.realtimeSinceStartup;
    }

    IEnumerator OCAnim(bool isOpening)
    {
        if (isOpening)
        {
            foreach (Sprite frame in openclose)
            {
                yield return new WaitForSecondsRealtime(ocAnimTime / openclose.Count);
                ocRend.sprite = frame;
            }
        }
        else
        {
            foreach (Sprite frame in closeopen)
            {
                yield return new WaitForSecondsRealtime(ocAnimTime / closeopen.Count);
                ocRend.sprite = frame;
            }
        }
    }
}
