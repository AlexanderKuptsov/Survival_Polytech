using System.Collections;
using UnityEngine;

public class FloatingTextController : MonoBehaviour {

    private static FloatingText popupText;
    private static GameObject canvas;

   /* private void Awake()
    {
        Initialize();
    } */

    public static void Initialize()
    {
        canvas = GameObject.Find("Canvas");
        if (!popupText)
            popupText = Resources.Load<FloatingText>("Prefabs/PopupDmgTextParent");        
    }

    public static void CreateFloatingText(string text,Transform location)
    {
        FloatingText instance = Instantiate(popupText);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        screenPosition.x += Random.Range(-0.95f, 0.95f);
        screenPosition.y += Random.Range(-0.95f, 0.95f);

        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.transform.rotation = canvas.transform.rotation;
        instance.SetText(text);
    }

    private static void SetTextScale()
    {
        ///////////
    }
}
