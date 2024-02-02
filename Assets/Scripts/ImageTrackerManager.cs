using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;

public class ImageTrackerManager : MonoBehaviour
{
    [SerializeField] ARTrackedImageManager m_TrackedImageManager;
    [SerializeField] private TextMeshProUGUI debugText;

    void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;

    void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

    private void Start()
    {
        ListAllImages();
    }

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            debugText.text = "Added";
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            debugText.text = "Updated";
        }

        foreach (var removedImage in eventArgs.removed)
        {
            debugText.text = "Removed";
        }
    }

    void ListAllImages()
    {
        Debug.Log(
            $"There are {m_TrackedImageManager.trackables.count} images being tracked.");

        foreach (var trackedImage in m_TrackedImageManager.trackables)
        {
            Debug.Log($"Image: {trackedImage.referenceImage.name} is at " +
                      $"{trackedImage.transform.position}");
        }
    }

}