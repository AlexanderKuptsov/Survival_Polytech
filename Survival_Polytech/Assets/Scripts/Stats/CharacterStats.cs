using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{

    #region Singleton

    public static CharacterStats Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Slomalis' stati chini daun");
            return;
        }
        Instance = this;
    }

    #endregion
   
       
    [Range(0, 200)]
    public int maxHealth = 100;
    public float currentHealth { get; private set; }
    public int lvl;
    public int lvlUpPoints;
    public float ExpToLvlUp;
    public float EXP;
    public float money;

    [Range(1, 300)]
    public float maxValue = 100f;
    [Range(0, 300)]
    public float hunger;
    public float constOfHunger;
    public float hungerDyingConst;
    [Range(0, 300)]
    public float sleepiness;
    public float constOfSleepiness;
    [Range(0, 300)]
    public int sanity;

    public int intelligence;
    public int agility;
    public int authority;

    private bool alive = true;
    private bool sleeping = false;
    private bool starvation = false;

    private bool isStopped = false;

    public GameObject agilityButton;
    public GameObject authorityButton;
    public GameObject intelligenceButton;
    public GameObject panel;

    private Button button1;
    private Button button2;
    private Button button3;

    public float[] GetAllStats()
    {
        float[] stats = new float[12];
        stats[0] = currentHealth;
        stats[1] = lvl;
        stats[2] = lvlUpPoints;
        stats[3] = ExpToLvlUp;
        stats[4] = EXP;
        stats[5] = money;
        stats[6] = hunger;
        stats[7] = sleepiness;
        stats[8] = sanity;
        stats[9] = intelligence;
        stats[10] = agility;
        stats[11] = authority;
        return stats;
    }

    public void SetAllStats(float[] newStats)
    {
        currentHealth = newStats[0];
        lvl = (int)newStats[1];
        lvlUpPoints = (int)newStats[2];
        ExpToLvlUp = newStats[3];
        EXP = newStats[4];
        money = newStats[5];
        hunger = newStats[6];
        sleepiness = newStats[7];
        sanity = (int)newStats[8];
        intelligence = (int)newStats[9];
        agility = (int)newStats[10];
        authority = (int)newStats[11];
    }

    public bool[] GetAllPlayerStates()
    {
        bool[] states = new bool[3];
        states[0] = alive;
        states[1] = sleeping;
        states[2] = starvation;

        return states;
    }

    public void SetAllPlayerStates(bool[] newStates)
    {
        alive = newStates[0];
        sleeping = newStates[1];
        starvation = newStates[2];
    }

    private void Start()
    {
        isStopped = false;
        alive = true;
        currentHealth = maxHealth;

        button1 = agilityButton.GetComponent<Button>();
        button2 = authorityButton.GetComponent<Button>();
        button3 = intelligenceButton.GetComponent<Button>();
        button1.onClick.AddListener(delegate { AgilityAdd(); });
        button2.onClick.AddListener(delegate { AuthorityAdd(); });
        button3.onClick.AddListener(delegate { IntelligenceAdd(); });

        EventHandler.OnTimeScaleChanged += CheckTimeScale;
    }

    void Update()
    {
        if (panel.activeSelf)
        {
            if (lvlUpPoints > 0)
            {
                agilityButton.SetActive(true);
                authorityButton.SetActive(true);
                intelligenceButton.SetActive(true);
            }
            else
            {
                agilityButton.SetActive(false);
                authorityButton.SetActive(false);
                intelligenceButton.SetActive(false);
            }
        }

        if (alive && !isStopped)
        {
            FallingAsleep(constOfSleepiness);

            Starvation(constOfHunger);

            if (starvation)
                TakeDamage(hungerDyingConst);

            if (currentHealth <= 0 && alive)
                Die();

            if (EXP > ExpToLvlUp)
                LvlUp();
        }
    }

    public void CheckTimeScale(bool stop)
    {
        isStopped = stop;
        Debug.Log("Time Scale Changed" + isStopped);
    }

    private void AgilityAdd()
    {
        lvlUpPoints -= 1;
        agility += 5;
    }

    private void AuthorityAdd()
    {
        lvlUpPoints -= 1;
        authority += 5;
    }

    private void IntelligenceAdd()
    {
        lvlUpPoints -= 1;
        intelligence += 5;
    }

    public void LvlUp()
    {
        lvl++;
		EXP -= ExpToLvlUp;
        ExpToLvlUp += Mathf.Floor(ExpToLvlUp / 2.5f); //
        lvlUpPoints += 5;
    }

    public void TakeDamage(float damage)
    {
        // придумать что-то получше
        // damage *= (maxValue - sanity.GetValue()) / 100; 
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;
        //Debug.Log(transform.name + " takes " + damage + " damage.");        
    }

    public void HealthChange(float amount)
    {    
        currentHealth += amount;

        if (currentHealth < 0)
            currentHealth = 0;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public virtual void Die()
    {
        alive = false;
        // some cruel way of death
        Debug.Log(transform.name + " died.");
    }

    public void FallingAsleep(float amount)
    {
        sleepiness += amount;
		if (sleepiness >= maxValue)
        {
			sleepiness = maxValue;
            if (!sleeping)
                FallAsleep();
        }
        else
            if (sleeping) sleeping = false;
    }

    public void FallAsleep()
    {
        // some cruel way of falling asleep
        Debug.Log(transform.name + " is sleeping.");
        sleeping = true;
    }

    public void Starvation(float amount)
    {
        hunger -= amount;
		if (hunger <= 0)
        {
			hunger = 0;
            if (!starvation)
                HungerDyingState();
		}
		else
			if (starvation) starvation = false;

		if (hunger >= maxValue) {
			hunger = maxValue;
		} 
           
    }

    public void HungerDyingState()
    {
        // some cruel way of starvation
        Debug.Log(transform.name + " starvation");
        starvation = true;
    }

    public bool ChangeMoney(float amount)
    {
        money += amount;
        if (money < 0)
        {
            money -= amount;
            return false;
        }
        return true;
    }

    public void EarnEXP(int amount)
    {
        EXP += amount;
        if (EXP > ExpToLvlUp)
            LvlUp();
    }    
}