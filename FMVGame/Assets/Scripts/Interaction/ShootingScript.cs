using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    [SerializeField]
    private InteractionManager interactionManager;

    [SerializeField]
    private AudioClip sfx;

    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            interactionManager.SendInteraction(new InteractionInfo
            {
                interactionType = InteractionInfo.InteractionType.Click,
                mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition)
            });
            SFXOneshotPlayer.Instance.PlaySFXOneshot(sfx);
        }
    }
}