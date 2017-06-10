using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuilderSelectable : MonoBehaviour {

    //public bool Selected { get; set; }

    //private MeshRenderer highlight;
    //private GameObject panel;
    //private GameObject detailsPanel;

    //// UI
    //private Text buildingName;
    //private Text buildingPrice;
    //private Text buildingHealth;
    //private Text buildingArmor;
    //private Text unitName;
    //private Text unitHealth;
    //private Text unitArmor;
    //private Text unitAttackDamage;
    //private Text unitAttackSpeed;
    //private Text unitRange;
    //private Text unitSpawnRate;

    //private void Start()
    //{
    //    highlight = transform.Find("Highlight").GetComponent<MeshRenderer>();
    //}

    //public void Select()
    //{
    //    Selected = true;
    //    highlight.enabled = true;
    //    panel = SelectionManager.instance.GetActivePanel();
    //}

    //public void Deselect()
    //{
    //    Selected = false;
    //    highlight.enabled = false;
    //    panel = null;
    //}

    //private void GetPanelFields()
    //{
    //    Transform detailsPanel = panel.transform.Find("Details");
    //    this.detailsPanel = detailsPanel.gameObject;

    //    buildingName = detailsPanel.Find("Name").GetComponent<Text>();
    //    buildingPrice = detailsPanel.Find("Price").GetComponent<Text>();
    //    buildingHealth = detailsPanel.Find("Building Details").Find("Health").Find("Value").GetComponent<Text>();
    //    buildingArmor = detailsPanel.Find("Building Details").Find("Armor").Find("Value").GetComponent<Text>();
    //    unitName = detailsPanel.Find("Unit Details").Find("Name").GetComponent<Text>();
    //    unitHealth = detailsPanel.Find("Unit Details").Find("Health").Find("Value").GetComponent<Text>();
    //    unitArmor = detailsPanel.Find("Unit Details").Find("Armor").Find("Value").GetComponent<Text>();
    //    unitAttackDamage = detailsPanel.Find("Unit Details").Find("Attack Damage").Find("Value").GetComponent<Text>();
    //    unitAttackSpeed = detailsPanel.Find("Unit Details").Find("Attack Speed").Find("Value").GetComponent<Text>();
    //    unitRange = detailsPanel.Find("Unit Details").Find("Range").Find("Value").GetComponent<Text>();
    //    unitSpawnRate = detailsPanel.Find("Unit Details").Find("Spawn Rate").GetComponent<Text>();
    //}

    //public void ShowDetails(int abilityID)
    //{
    //    Debug.Log("Show details for " + abilityID);
    //    GetPanelFields();
    //    var building = transform.parent.GetComponent<BuilderController>().GetBuilding(abilityID);
    //    if (building != null)
    //    {
    //        UpdateDetails(building);
    //        detailsPanel.SetActive(true);
    //    }
    //}

    //public void HideDetails()
    //{
    //    Debug.Log("Hide details");
    //    detailsPanel.SetActive(false);
    //}
    
    //public void UpdateDetails(GameObject buildingObject)
    //{
    //    //BuildingController building = buildingObject.GetComponent<BuildingController>();
    //    //BuildingStats stats = buildingObject.GetComponent<BuildingStats>();
    //    //BuldingDetails buildingDetails = building.GetComponent<BuldingDetails>();
    //    //UnitDetails unitDetails = building.unit.GetComponent<UnitDetails>();
    //    //UnitStats unitStats = building.unit.GetComponent<UnitStats>();

    //    //buildingName.text = buildingDetails.buildingName;
    //    //buildingPrice.text = buildingDetails.buildingPrice.ToString() + "g";

    //    //buildingHealth.text = stats.health.ToString();
    //    //buildingArmor.text = stats.armor.ToString();

    //    //unitName.text = unitDetails.name;
    //    //unitHealth.text = unitStats.health.ToString();
    //    //unitArmor.text = unitStats.armor.ToString();
    //    //unitAttackDamage.text = unitStats.attackDamage.ToString();
    //    //unitAttackSpeed.text = unitStats.attackSpeed.ToString();
    //    //unitRange.text = unitStats.range.ToString();

    //    unitSpawnRate.text = unitDetails.unitSpawnTime.ToString() + "s";
    //}

    //public IEnumerator UpdateUI()
    //{
    //    yield return null;
    //}
}
