using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;

    // Flags
    public bool isAiming;
    public int ammoCount;

    [SerializeField]
    private GameObject currentCursor;

    private Camera cam;

    private void Awake()
    {
        Instance = this;
        cam = Camera.main;
        currentCursor.SetActive(true);
    }

    private void Update()
    {
        Cursor.visible = false;
        EvaluateCursorState();
        var cursorPos = cam.ScreenToWorldPoint(Input.mousePosition);
        cursorPos.z = 0;
        currentCursor.transform.position = cursorPos;
    }

    private void EvaluateCursorState()
    {
    }
}