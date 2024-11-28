using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using UnityEngine.UI;

public class TourismInfoHandler : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public AudioSource sigiriyaAudioSource;
    public AudioSource nineArchAudioSource;
    public AudioSource ambuluwawaAudioSource;
    public AudioSource kelaniyaAudioSource;       // AudioSource for Kelaniya Raja Maha Viharaya
    public AudioSource sripadaAudioSource;        // AudioSource for Sri Pada
    public AudioSource ruwanweliMahaSeyaAudioSource; // AudioSource for Ruwanweli Maha Seya
    public Button playAudioButton;

    private AudioSource currentAudioSource;
    private string currentImageName;

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnImageChanged;
        playAudioButton.onClick.AddListener(PlayAudio);
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnImageChanged;
        playAudioButton.onClick.RemoveListener(PlayAudio);
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            UpdateImageInfo(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateImageInfo(trackedImage);
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            if (trackedImage.referenceImage.name == currentImageName)
            {
                HideInfoAndStopAudio();
                Debug.Log("Image removed, hiding info panel and stopping audio.");
            }
        }
    }

    private void UpdateImageInfo(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            string imageName = trackedImage.referenceImage.name;
            if (imageName == currentImageName) return;

            StopCurrentAudio();
            infoPanel.SetActive(true);
            currentImageName = imageName;

            switch (imageName)
            {
                case "Sigiriya":
                    infoText.text = "Sigiriya\r\n\r\nLocation: Matale District, Central Province, Sri Lanka\r\nType: Ancient rock fortress and UNESCO World Heritage Site\r\nHeight: Approximately 200 meters (660 feet) above ground level\r\nPeriod: Built during the reign of King Kasyapa I (477–495 AD)\r\n";
                    currentAudioSource = sigiriyaAudioSource;
                    break;
                case "NineArchBridge":
                    infoText.text = "NineArchBridge\r\n\r\nLocation: Near Ella, Sri Lanka\r\nConstruction Year: Built in 1921\r\nLength: Approximately 300 feet (91 meters)\r\nHeight: About 80 feet (24 meters)";
                    currentAudioSource = nineArchAudioSource;
                    break;
                case "Ambuluwawa":
                    infoText.text = "Ambuluwawa\r\n\r\nLocation: Near Gampola, Central Province, Sri Lanka\r\nHeight: 48 meters tall\r\nMountain Height: Rises to 1,067 meters above sea level\r\nOpening Hours: Daily from 8:30 AM to 5:00 PM\r\nEntrance Fee: 2,000 LKR for foreign adults\r\n";
                    currentAudioSource = ambuluwawaAudioSource;
                    break;
                case "KelaniyaRajaMahaViharaya":
                    infoText.text = "KelaniyaRajaMahaViharaya\r\n\r\nLocation: Kelaniya, near Colombo, Sri Lanka\r\nType: Buddhist temple (Viharaya)\r\nHistorical Significance: One of the oldest and most important Buddhist temples in Sri Lanka";
                    currentAudioSource = kelaniyaAudioSource;
                    break;
                case "SriPada":
                    infoText.text = "SriPada\r\n\r\nLocation: Central Province, Sri Lanka\r\nHeight: 2,243 meters (7,359 feet)\r\nSignificance: Sacred mountain for Buddhists, Hindus, Muslims, and Christians";
                    currentAudioSource = sripadaAudioSource;
                    break;
                case "RuwanweliMahaSeya":
                    infoText.text = "RuwanweliMahaSeya\r\n\r\nLocation: Anuradhapura, Sri Lanka\r\nType: Stupa (Buddhist shrine)\r\nBuilt By: King Dutugemunu\r\nConstruction Date: Approximately 140 B.C.\r\nHeight: 55 meters (180 feet)\r\nDiameter: 36 meters (118 feet) at the base\r\nMaterial: Made of brick and clay, covered with white plaster\r\n";
                    currentAudioSource = ruwanweliMahaSeyaAudioSource;
                    break;
            }

            Debug.Log($"{imageName} detected and text updated.");
        }
        else
        {
            if (trackedImage.referenceImage.name == currentImageName)
            {
                HideInfoAndStopAudio();
                Debug.Log("Tracking lost for " + currentImageName);
            }
        }
    }

    private void PlayAudio()
    {
        if (currentAudioSource != null && !currentAudioSource.isPlaying)
        {
            currentAudioSource.Play();
            Debug.Log("Playing audio for " + currentImageName);
        }
    }

    private void StopCurrentAudio()
    {
        if (currentAudioSource != null && currentAudioSource.isPlaying)
        {
            currentAudioSource.Stop();
            Debug.Log("Stopping audio for " + currentImageName);
        }
    }

    private void HideInfoAndStopAudio()
    {
        infoPanel.SetActive(false);
        StopCurrentAudio();
        currentImageName = null;
    }
}
