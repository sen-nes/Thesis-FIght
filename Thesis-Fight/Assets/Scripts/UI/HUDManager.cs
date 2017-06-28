using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    public GameObject topPanel;
    public GameObject builderPanel;
    public GameObject buildingPanel;
    public GameObject unitPanel;
    public GameObject castlePanel;

    private GameObject activePanel;
    private Spawner spawner;

    private void Start()
    {
        InvokeRepeating("KeepTrackOfTime", 0f, 1f);
    }

    public void KeepTrackOfTime()
    {
        Text currentTime = topPanel.transform.Find("Time").GetComponentInChildren<Text>();
        string minutes = ((int)(Time.time / 60)).ToString().PadLeft(2, '0');
        string seconds = ((int)(Time.time % 60)).ToString().PadLeft(2, '0');
        currentTime.text = minutes + " : " + seconds;
    }

    public void UpdateHUD(GameObject selectedObject)
    {
        StopAllCoroutines();
        spawner = null;

        if (selectedObject != null)
        {
            switch (selectedObject.tag)
            {
                case "Builder":
                    builderPanel.SetActive(true);
                    activePanel = builderPanel;
                    UpdateBuilderHUD(selectedObject);
                    break;
                case "Building":
                    buildingPanel.SetActive(true);
                    activePanel = buildingPanel;
                    spawner = selectedObject.GetComponent<Spawner>();
                    StartCoroutine(UpdateBuildingHUD(selectedObject));
                    break;
                case "Unit":
                    unitPanel.SetActive(true);
                    activePanel = unitPanel;
                    StartCoroutine(UpdateUnitHUD(selectedObject));
                    break;
                case "Castle":
                    castlePanel.SetActive(true);
                    activePanel = castlePanel;
                    StartCoroutine(UpdateCastleHUD(selectedObject));
                    break;
                default:
                    if (activePanel != null)
                    {
                        activePanel.SetActive(false);
                        activePanel = null;
                    }
                    break;
            }
        }
        else
        {
            if (activePanel != null) {
                activePanel.SetActive(false);
                activePanel = null;
            }
        }
    }

    private IEnumerator UpdateCastleHUD(GameObject obj)
    {
        Attackable attackable = obj.GetComponent<Attackable>();
        StructureStats stats = obj.GetComponent<StructureStats>();

        Text name = activePanel.transform.Find("Name").GetComponentInChildren<Text>();
        Text armor = activePanel.transform.Find("Stats").Find("Armor").Find("Value").GetComponent<Text>();
        Text armorType = activePanel.transform.Find("Stats").Find("Armor").Find("Type").GetComponent<Text>();
        Text health = activePanel.transform.Find("Stats").Find("Healthbar").Find("Value").GetComponent<Text>();
        Image healthbar = activePanel.transform.Find("Stats").Find("Healthbar").Find("Bar").GetComponent<Image>();
        
        if (attackable.teamID == Teams.TEAM_EAST)
        {
            name.text = "Castle East";
        }
        else
        {
            name.text = "Castle West";
        }

        armorType.text = "(" + stats.ArmorType.ToString().ToLower() + ")";

        while (true)
        {
            health.text = attackable.CurrentHealth + " / " + stats.Health.FinalValue;
            healthbar.fillAmount = attackable.HealthPercentage;
            armor.text = stats.Armor.FinalValue.ToString();

            yield return null;
        }
     }

    private void UpdateBuilderHUD(GameObject obj)
    {
        GameObject abilities = activePanel.transform.Find("Builder Abilities").gameObject;
        GameObject nameField = activePanel.transform.Find("Name").gameObject;

        if (obj != GameStartManager.HumanBuilder)
        {
            Text name = activePanel.transform.Find("Name").GetComponentInChildren<Text>();
            name.text = "Builder (BOT)";

            abilities.SetActive(false);
            nameField.SetActive(true);
        }
        else
        {
            nameField.SetActive(false);
            abilities.SetActive(true);
        }
    }

    private IEnumerator UpdateBuildingHUD(GameObject obj)
    {
        GameObject prodPanel = activePanel.transform.Find("Production").gameObject;
        Attackable attackable = obj.GetComponent<Attackable>();
        StructureStats stats = obj.GetComponent<StructureStats>();

        Text name = activePanel.transform.Find("Name").GetComponentInChildren<Text>();
        Text armor = activePanel.transform.Find("Stats").Find("Armor").Find("Value").GetComponent<Text>();
        Text armorType = activePanel.transform.Find("Stats").Find("Armor").Find("Type").GetComponent<Text>();
        Text health = activePanel.transform.Find("Stats").Find("Healthbar").Find("Value").GetComponent<Text>();
        Image healthbar = activePanel.transform.Find("Stats").Find("Healthbar").Find("Bar").GetComponent<Image>();
        Image progressBar = activePanel.transform.Find("Production").Find("Progress Bar").Find("Bar").GetComponent<Image>();

        if (obj.GetComponent<BuildingController>().playerID != GameStartManager.HumanBuilderID)
        {
            prodPanel.SetActive(false);
        }
        else
        {
            prodPanel.SetActive(true);
        }

        name.text = obj.GetComponent<BuildingController>().building.name;
        armorType.text = "(" + stats.ArmorType.ToString().ToLower() + ")";

        while (true)
        {
            health.text = attackable.CurrentHealth + " / " + stats.Health.FinalValue;
            healthbar.fillAmount = attackable.HealthPercentage;
            armor.text = stats.Armor.FinalValue.ToString();

            progressBar.fillAmount = spawner.Progress;

            yield return null;
        }
    }

    private IEnumerator UpdateUnitHUD(GameObject obj)
    {
        Attackable attackable = obj.GetComponent<Attackable>();
        FighterStats stats = obj.GetComponent<FighterStats>();

        Text name = activePanel.transform.Find("Name").GetComponentInChildren<Text>();
        Text armor = activePanel.transform.Find("Center Panel").Find("Armor").Find("Value").GetComponent<Text>();
        Text armorType = activePanel.transform.Find("Center Panel").Find("Armor").Find("Type").GetComponent<Text>();
        Text health = activePanel.transform.Find("Center Panel").Find("Healthbar").Find("Value").GetComponent<Text>();
        Image healthbar = activePanel.transform.Find("Center Panel").Find("Healthbar").Find("Bar").GetComponent<Image>();

        Text attackDamage = activePanel.transform.Find("Left Panel").Find("Attack Damage").Find("Value").GetComponent<Text>();
        Text attackDamageType = activePanel.transform.Find("Left Panel").Find("Attack Damage").Find("Type").GetComponent<Text>();
        Text attackSpeed = activePanel.transform.Find("Left Panel").Find("Attack Speed").Find("Value").GetComponent<Text>();
        Text critChance = activePanel.transform.Find("Left Panel").Find("Critical Chance").Find("Value").GetComponent<Text>();
        Text range = activePanel.transform.Find("Left Panel").Find("Range").Find("Value").GetComponent<Text>();

        // Armor/Damage type

        name.text = obj.GetComponent<UnitController>().unit.name;

        attackDamageType.text = "(" + stats.DamageType.ToString().ToLower() + ")";
        armorType.text = "(" + stats.ArmorType.ToString().ToLower() + ")";

        while (true)
        {
            health.text = attackable.CurrentHealth + " / " + stats.Health.FinalValue;
            healthbar.fillAmount = attackable.HealthPercentage;
            armor.text = stats.Armor.FinalValue.ToString();

            attackDamage.text = stats.AttackDamage.FinalValue.ToString();
            attackSpeed.text = (1f / stats.AttackSpeed.FinalValue).ToString();
            critChance.text = (stats.CriticalChance.FinalValue * 100) + " %";
            range.text = stats.Range.FinalValue.ToString();

            yield return null;
        }
    }

    public void StartProduction()
    {
        activePanel.transform.Find("Production").Find("Start").gameObject.SetActive(false);
        activePanel.transform.Find("Production").Find("Stop").gameObject.SetActive(true);
        if (spawner != null)
        {
            spawner.StartSpawning();
        }
    }

    public void StopProduction()
    {
        activePanel.transform.Find("Production").Find("Start").gameObject.SetActive(true);
        activePanel.transform.Find("Production").Find("Stop").gameObject.SetActive(false);
        if (spawner != null)
        {
            spawner.StopSpawning();
        }
    }

    public void ToggleAbilityDetails(int index)
    {
        GameObject details = activePanel.transform.Find("Details").gameObject;

        if (!details.activeSelf)
        {
            GameObject builder = GameStartManager.HumanBuilder;
            Building building = builder.GetComponent<Build>().buildings[index].GetComponent<BuildingController>().building;
            Unit unit = builder.GetComponent<Build>().buildings[index].GetComponent<Spawner>().unit.GetComponent<UnitController>().unit;

            Transform buildingDetails = details.transform.Find("Building Details");
            Transform unitDetails = details.transform.Find("Unit Details");

            Text buildingName = details.transform.Find("Name").GetComponentInChildren<Text>();
            Text buildingPrice = details.transform.Find("Price").GetComponentInChildren<Text>();
            Text buildingHealth = buildingDetails.Find("Health").Find("Value").GetComponent<Text>();
            Text buildingArmor = buildingDetails.Find("Armor").Find("Value").GetComponent<Text>();

            Text unitName = unitDetails.Find("Name").GetComponent<Text>();
            Text unitSpawnRate = unitDetails.Find("Spawn Rate").GetComponent<Text>();
            Text unitHealth = unitDetails.Find("Health").Find("Value").GetComponent<Text>();
            Text unitArmor = unitDetails.Find("Armor").Find("Value").GetComponent<Text>();
            Text unitDamage = unitDetails.Find("Attack Damage").Find("Value").GetComponent<Text>();
            Text unitAttackSpeed = unitDetails.Find("Attack Speed").Find("Value").GetComponent<Text>();
            Text unitRange = unitDetails.Find("Range").Find("Value").GetComponent<Text>();

            buildingName.text = building.name;
            buildingPrice.text = building.cost + "g";
            buildingHealth.text = building.health.ToString();
            buildingArmor.text = building.armor.ToString();

            unitName.text = unit.name;
            unitSpawnRate.text = (building.cost / 20 + 15) + "s";
            unitHealth.text = unit.health.ToString();
            unitArmor.text = unit.armor.ToString();
            unitDamage.text = unit.attackDamage.ToString();
            unitAttackSpeed.text = unit.attackSpeed.ToString();
            unitRange.text = unit.range.ToString();

            details.SetActive(true);
        }
        else
        {
            details.SetActive(false);
        }
    }
}
