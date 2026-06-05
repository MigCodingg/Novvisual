using UnityEngine;
using UnityEngine.InputSystem;

public class ClickSFX : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;
    [Range(0f, 1f)]
    [SerializeField] private float clickVolume = 0.3f;

    private void Update()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            AudioManager.Instance.PlaySFX(clickSound, clickVolume);
        }
    }
}