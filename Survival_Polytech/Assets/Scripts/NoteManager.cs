using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{

    #region Singleton

    public static NoteManager Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Slomalis' zapiski chini daun");
            return;
        }
        Instance = this;
    }

    #endregion

    public GameObject note;
    public string text;

    //string[] allNotes;
    Text noteText;
    Image image;

    void Start()
    {
        noteText = note.GetComponentInChildren<Text>();
        UpdateText();
        image = note.GetComponentInChildren<Image>();
    }
        
    void Update()
    {        
     
    }

    public void OpenNewNote(string newText)
    {
        noteText.text = newText;
        note.SetActive(true);
    }

    public void UpdateNewText(string newText)
    {
        noteText.text = newText;
    }

    public void UpdateText()
    {
        noteText.text = text;
    }

    public void SetImage(Image newImage)
    {
        image = newImage;
    }
}