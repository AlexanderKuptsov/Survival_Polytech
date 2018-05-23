using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class QuestSystem : MonoBehaviour
{

    #region Singleton

    public static QuestSystem Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Slomalis' Kvesti chini daun");
            return;
        }
        Instance = this;
    }

    #endregion

    public Text currentQuestText;

    public List<Quest> Quests { get; set; }
    public bool AllCompleted { get; set; }

    [HideInInspector]
    public Quest currentQuest;
	[HideInInspector]
	public Quest coffeeQuest;
	[HideInInspector]
	public Quest foodQuest;
	[HideInInspector]
	public Quest keyQuest;
	[HideInInspector]
    private void Start()
    {

        Quest coffeeQuest = GetComponent<CoffeeIsLife>();
		Quest foodQuest = GetComponent<Eating>();
		Quest keyQuest = GetComponent<KeyQuest>();
        // more quests..
        
        Quests = new List<Quest>
        {          
			foodQuest,
			keyQuest
            // more quests..
        };
		//ActivateQuest(1);
        ActivateQuest(0);
		
		
    }

    public void ActivateQuest(int index)
    {
        currentQuest = Quests[index];

        currentQuest.enabled = true;
        currentQuestText.text = currentQuest.Description;
        
    }

    public void CheckQuests()
    {
        AllCompleted = Quests.All(g => g.Completed);
        if (AllCompleted)
        {
            Done();
        }
    }

    private void Done()
    {
        // Game result
        currentQuestText.text = "Всё пройдено";
        Debug.Log("All quests are done!");
    }
}
