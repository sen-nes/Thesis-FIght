using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectable : MonoBehaviour {

    //public bool Selected { get; set; }

    //private MeshRenderer highlight;
    //private UnitStats stats;
    //private UnitCombat combat;

    //// UI
    //private GameObject panel;
    //private Text damage;
    //private Text attackSpeed;
    //private Text range;
    //private Text armor;
    //private Text health;
    //private Image healthBar;
    

    //// Details, display playerID somewhere

    //private void awake()
    //{
    //    highlight = transform.Find("Highlight").GetComponent<MeshRenderer>();
    //    stats = transform.parent.GetComponent<UnitStats>();
    //    combat = transform.parent.GetComponent<UnitCombat>();
    //}

    //public void Select()
    //{
    //    Selected = true;
    //    highlight.enabled = true;
    //    panel = SelectionManager.instance.GetActivePanel();
    //    GetPanelFields();
    //    StartCoroutine("UpdateUI");
    //}

    //public void Deselect()
    //{
    //    Selected = false;
    //    highlight.enabled = false;
    //    panel = null;
    //    StopCoroutine("UpdateUI");
    //}

    //private void GetPanelFields()
    //{
    //    damage = panel.transform.Find("Left Panel").Find("Attack Damage").Find("Value").GetComponent<Text>();
    //    attackSpeed = panel.transform.Find("Left Panel").Find("Attack Speed").Find("Value").GetComponent<Text>();
    //    range = panel.transform.Find("Left Panel").Find("Range").Find("Value").GetComponent<Text>();
    //    armor = panel.transform.Find("Center Panel").Find("Armor").Find("Value").GetComponent<Text>();
    //    health = panel.transform.Find("Center Panel").Find("HealthBar").Find("Value").GetComponent<Text>();
    //    healthBar = panel.transform.Find("Center Panel").Find("HealthBar").Find("Bar").GetComponent<Image>();
    //}

    //// Enumerable?
    //public IEnumerator UpdateUI()
    //{
    //    Debug.Log("Update");
    //    while (true)
    //    {
    //        damage.text = stats.AttackDamage.FinalValue.ToString();
    //        attackSpeed.text = stats.AttackSpeed.FinalValue.ToString();
    //        range.text = stats.Range.FinalValue.ToString();
    //        armor.text = stats.Armor.FinalValue.ToString();
    //        health.text = combat.currentHealth + " / " + stats.Health.FinalValue;
    //        healthBar.fillAmount = combat.currentHealth / stats.Health.FinalValue;
    //        yield return null;
    //    }
    //}
}
