using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour {

    public Transform interactTransform ; // для того чтобы описать случай, когда место взаимодействия и объект в разных местах, можно оставить сам объект
    public GameObject detect;    
    public Image description;
    public Text text;
    public string descriptionText = "Не забудь добавить описание";

    public float radius = 3f;
    bool isFocused = false;
    Transform player = null;
    bool isInteracted = false;

    private GameObject defaultDescription;

    private void Awake()
    {
        if (description == null || text == null)
        {
            defaultDescription = GameObject.FindGameObjectWithTag("Description");
            if (description == null)
                description = defaultDescription.GetComponent<Image>();

            if (text == null)
                text = defaultDescription.transform.GetChild(0).GetComponent<Text>();
        }
    }

    public virtual void Interact()
    {

    }

    public virtual void AfterInteract()
    {

    }

    public virtual void ForImageSet()
    {

    } 

    public virtual void ForImageRemove()
    {

    }

    public void OnFocused(Transform playerTransform)
    {
        isFocused = true;
        player = playerTransform;
        isInteracted = false;
    }

    public void OnDefocused()
    {
        isFocused = false;
        player = null;
        isInteracted = false;
    }
    
    void OnMouseEnter()
    {
        if (detect != null)
        {
            try
            {
                detect.GetComponent<MeshRenderer>().enabled = true;
            }catch(MissingComponentException e)
            {
                Debug.Log("No MeshRenderer");
            }
            description.enabled = true;
            SetText();
            ForImageSet();
        }
    }

    private void OnMouseExit()
    {
        if (detect != null)
        {
            try
            {
                detect.GetComponent<MeshRenderer>().enabled = false;
            }
            catch (MissingComponentException e)
            {
                Debug.Log("No MeshRenderer");
            }
            description.enabled = false;
            RemoveText();
            ForImageRemove();
        }
    }

    public virtual void SetText()
    {
        text.text = descriptionText;
        text.enabled = true;
    }

    public void RemoveText()
    {
        text.text = null;
        text.enabled = false;
    }

    private void Update()
    {
        if(isFocused && !isInteracted )
        {
            float distanse = Vector3.Distance(player.transform.position, interactTransform.position);
            if(distanse <= radius)
            {
                Interact();
                isInteracted = true;
            }
        }
      
    }

    public GameObject GetDefaultDescription()
    {
        return defaultDescription;
    }
}
