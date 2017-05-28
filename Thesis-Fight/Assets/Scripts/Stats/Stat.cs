using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{

    private float _finalValue;

    // Properties
    public string StatName { get; set; }
    public string StatDescription { get; set; }
    public float BaseValue { get; set; }
    public float FinalValue
    {
        get
        {
            _finalValue = BaseValue;
            Buffs.ForEach(x => _finalValue += x.Apply(BaseValue));
            Debuffs.ForEach(x => _finalValue += x.Apply(BaseValue));

            return _finalValue;
        }
    }

    public List<IStatModifier> Buffs;
    public List<IStatModifier> Debuffs;

    // Constructors
    public Stat(string _statName, string _statDescription, float _baseValue)
    {
        StatName = _statName;
        StatDescription = _statDescription;
        BaseValue = _baseValue;

        Buffs = new List<IStatModifier>();
        Debuffs = new List<IStatModifier>();
    }

    // Methods
    public void AddBuff(IStatModifier buff)
    {
        Buffs.Add(buff);
    }

    public void RemoveBuff(string name)
    {
        IStatModifier buff = Buffs.Find(x => x.Name.Equals(name));
        Buffs.Remove(buff);
    }

    public void AddDebuff(IStatModifier debuff)
    {
        Debuffs.Add(debuff);
    }

    public void RemoveDebuff(string name)
    {
        IStatModifier debuff = Debuffs.Find(x => x.Name.Equals(name));
        Debuffs.Remove(debuff);
    }
}
