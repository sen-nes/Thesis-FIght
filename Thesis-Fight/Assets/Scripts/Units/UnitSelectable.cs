using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectable : MonoBehaviour, ISelectable {

    public bool Selected { get; set; }

    private MeshRenderer highlight;
    private UnitStats stats;
    private UnitCombat combat;

    // UI
    private GameObject panel;
    private Text damage;
    private Text attackSpeed;
    private Text armor;
    private Text health;
    private Image healthBar;

    private void Start()
    {
        highlight = transform.Find("Highlight").GetComponent<MeshRenderer>();
        stats = transform.parent.GetComponent<UnitStats>();
        combat = transform.parent.GetComponent<UnitCombat>();
    }

    private void Update()
    {
        if(Selected)
        {
            UpdateUI();
        }
    }

    public void Select()
    {
        Selected = true;
        highlight.enabled = true;
        panel = SelectionManager.instance.GetActivePanel();
        GetPanelFields();
        StartCoroutine("UpdateUI");
    }

    public void Deselect()
    {
        Selected = false;
        highlight.enabled = false;
        panel = null;
        StopCoroutine("UpdateUI");
    }

    private void GetPanelFields()
    {
        damage = panel.transform.Find("Details").Find("Attack Damage").Find("Value").GetComponent<Text>();
        attackSpeed = panel.transform.Find("Details").Find("Attack Speed").Find("Value").GetComponent<Text>();
        armor = panel.transform.Find("Details").Find("Armor").Find("Value").GetComponent<Text>();
        health = panel.transform.Find("Health").Find("Health").Find("Value").GetComponent<Text>();
        healthBar = panel.transform.Find("Health").Find("Health").Find("HealthBar").Find("Bar").GetComponent<Image>();
    }

    // Enumerable?
    public IEnumerator UpdateUI()
    {
        Debug.Log("Update");
        while (true)
        {
            damage.text = stats.AttackDamage.FinalValue.ToString();
            attackSpeed.text = stats.AttackSpeed.FinalValue.ToString();
            armor.text = stats.Armor.FinalValue.ToString();
            health.text = combat.currentHealth + " / " + stats.Health.FinalValue;
            healthBar.fillAmount = combat.currentHealth / stats.Health.FinalValue;
            yield return null;
        }
    }
}
