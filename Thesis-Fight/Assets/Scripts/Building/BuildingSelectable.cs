using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSelectable : MonoBehaviour {

    //public bool Selected { get; set; }

    //private MeshRenderer highlight;
    //private BuildingStats stats;
    //private BuildingController controller;
    //private BuildingCombat combat;

    //// UI
    //private GameObject panel;
    //private Text armor;
    //private Text health;
    //private Image healthBar;
    //private Image progressBar;

    //private void Start()
    //{
    //    highlight = transform.Find("Highlight").GetComponent<MeshRenderer>();

    //    stats = transform.parent.GetComponent<BuildingStats>();
    //    controller = transform.parent.GetComponent<BuildingController>();
    //    combat = transform.parent.GetComponent<BuildingCombat>();
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
    //    armor = panel.transform.Find("Stats").Find("Armor").Find("Value").GetComponent<Text>();
    //    health = panel.transform.Find("Stats").Find("HealthBar").Find("Value").GetComponent<Text>();
    //    healthBar = panel.transform.Find("Stats").Find("HealthBar").Find("Bar").GetComponent<Image>();
    //    progressBar = panel.transform.Find("Production").Find("Progress Bar").Find("Bar").GetComponent<Image>();
    //}

    //// Enumerable?
    //public IEnumerator UpdateUI()
    //{
    //    while (true)
    //    {
    //        armor.text = stats.Armor.FinalValue.ToString();
    //        health.text = combat.currentHealth + " / " + stats.Health.FinalValue;
    //        healthBar.fillAmount = combat.currentHealth / stats.Health.FinalValue;

    //        yield return null;
    //    }
    //}

    //public void UpdateProgress(float progress)
    //{
    //    progressBar.fillAmount = progress;
    //}
}
