using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using RhythmTool;

public class DisplacementControl : MonoBehaviour
{

    [SerializeField] private float displacementAmount;
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private MeshRenderer meshRenderer;

    private Color newColor;
    private Color lerpedColor;

    private float lastDisplacementAmount;

    private void Awake()
    {
    }

    private void Start()
    {
        //MusicPlayerManager.Instance.onOnset += OnOnset;
        //GameManager.Instance.PlayerControls.Gameplay.Jump.performed += OnJump;
    }

    private void OnDestroy()
    {
        //MusicPlayerManager.Instance.onOnset -= OnOnset;
        //GameManager.Instance.PlayerControls.Gameplay.Jump.performed -= OnJump;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
        displacementAmount += 1;
    }

    void Update()
    {
        lerpedColor = Color.Lerp(newColor, Color.white, Mathf.PingPong(Time.time, 1));
        displacementAmount = Mathf.Lerp(displacementAmount, 0, Time.deltaTime);
        meshRenderer.material.SetFloat("_DisplacementAmount", displacementAmount);
        meshRenderer.material.SetColor("_Color", lerpedColor);
    }

    public void OnOnset(Onset onset, Color color)
    {
        //lastDisplacementAmount = displacementAmount;
        displacementAmount = Mathf.Clamp(onset.strength, 0, 0.5f);
        newColor = color;
    }
}
