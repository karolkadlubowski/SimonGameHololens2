using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightOnTouch : MonoBehaviour, IMixedRealityPointerHandler
{
    private Renderer rend;
    private Color originalColor;
    public int cubeIndex = 0; // Set this appropriately for each cube in the Inspector
    public AudioClip cubeSound; // Reference to the sound clip for this cube

    private MemoryGame gameManager;
    private AudioSource audioSource;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        gameManager = FindObjectOfType<MemoryGame>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = cubeSound;
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        rend.material.color = Color.white;
        gameManager.CubeTouched(cubeIndex);

        // Play the sound when the cube is clicked
        //audioSource.Play();
        audioSource.PlayOneShot(cubeSound);
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        rend.material.color = originalColor;
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData) {
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData) {
    }

    private void OnMouseDown()
    {
        audioSource.PlayOneShot(cubeSound);
        gameManager.CubeTouched(cubeIndex);
    }
}