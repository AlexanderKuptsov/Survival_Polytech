using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour {

	public Image healthI;
	public Image hungerI;
	public Image sanityI;
	public Image expI;

	private float currentHp;
	private float maximumHp;
	private float sanity;
	private float exp;
	private float hunger;
	private float maxValue;
    

	// Use this for initialization
	void Start () {

        maxValue = CharacterStats.Instance.maxValue;
		maximumHp = CharacterStats.Instance.maxHealth;
		currentHp = CharacterStats.Instance.currentHealth;
		sanity = CharacterStats.Instance.sanity;
		exp = CharacterStats.Instance.EXP;

		Setup();
	}

	// Update is called once per frame
	void Update () {
		if (currentHp != CharacterStats.Instance.currentHealth) {
			currentHp = CharacterStats.Instance.currentHealth;
            healthI.transform.localScale = new Vector2(Mathf.Min((currentHp / maximumHp), 1), (float)healthI.transform.localScale.y);
		}

		if (hunger != CharacterStats.Instance.hunger) {
			hunger = CharacterStats.Instance.hunger;
            hungerI.transform.localScale = new Vector2(Mathf.Min((hunger / maxValue), 1), (float)hungerI.transform.localScale.y);
		}

		if (sanity != CharacterStats.Instance.sanity) {

            sanityI.transform.localScale = new Vector2(Mathf.Min((sanity / maxValue), 1), (float)sanityI.transform.localScale.y);
		}

		if (exp != CharacterStats.Instance.EXP) {
			exp = CharacterStats.Instance.EXP;
            expI.transform.localScale = new Vector2(Mathf.Min((exp / CharacterStats.Instance.ExpToLvlUp), 1), (float)expI.transform.localScale.y);
		}
	}

	void Setup() {
        healthI.transform.localScale = new Vector2(Mathf.Min((currentHp / maximumHp), 1), (float)healthI.transform.localScale.y);
        hungerI.transform.localScale = new Vector2(Mathf.Min((hunger / maxValue), 1), (float)hungerI.transform.localScale.y);
        sanityI.transform.localScale = new Vector2(Mathf.Min((sanity / maxValue), 1), (float)sanityI.transform.localScale.y);
        expI.transform.localScale = new Vector2(Mathf.Min((exp / CharacterStats.Instance.ExpToLvlUp), 1), (float)expI.transform.localScale.y);
    }
}