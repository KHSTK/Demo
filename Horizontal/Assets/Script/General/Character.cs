using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour,ISaveable
{
    [Header("事件监听")]
    public VoidEventSO newGameEvent;

    [Header("基本属性")]
    public float maxHealth;
    public float currentHealth;
    public float maxPower;
    public float currentPower;
    public float powerRecoverSpeed;
    [Header("受伤无敌")]
    public float invulnerableDuration;
    public float invulnerableCounter;
    public bool invulnerable;

    public UnityEvent<Character> OnHealthChange;
    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDead;

    private void OnEnable()
    {
        newGameEvent.OnEventRaised += NewGame;
        ISaveable saveable = this;
        saveable.RegisterSaveData();
    }
    private void OnDisable()
    {
        newGameEvent.OnEventRaised -= NewGame;
        ISaveable saveable = this;
        saveable.RegisterSaveData();
    }
    private void NewGame()
    {
        currentHealth = maxHealth;
        currentPower = maxPower;
        OnHealthChange.Invoke(this);

    }
    private void Update()
    {
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0)
            {
                invulnerable = false;
            }
        }
        if (currentPower < maxPower)
        {
            currentPower += Time.deltaTime * powerRecoverSpeed;
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            if (currentHealth > 0)
            {
                currentHealth = 0;
                OnHealthChange.Invoke(this);
                OnDead?.Invoke();
            }
        }
    }
    //受伤掉血
    public void TakeDamage(Attack attacker)
    {
        //是否无敌时间
        if (invulnerable) return;
        Debug.Log(attacker.damage);
        if (currentHealth <= attacker.damage)
        {
            currentHealth = 0;
            Debug.Log("die");

            //触发死亡
            OnDead.Invoke();
            OnHealthChange.Invoke(this);
            return;
        }
        currentHealth -=attacker.damage;
        //无敌时间
        TriggerInvulnerable();
        //执行受伤
        OnTakeDamage?.Invoke(attacker.transform);
        OnHealthChange.Invoke(this);
    }
    public void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
    public void OnSlide(int cost)
    {
        currentPower -= cost;
        OnHealthChange.Invoke(this);
    }
    public DataDefinition GetDataID()
    {
        return GetComponent<DataDefinition>();
    }
    public void GetSaveData(Data data)
    {
        //字典中是否已经存在
        if (data.characterPosDict.ContainsKey(GetDataID().ID))
        {
            //更新坐标
            data.characterPosDict[GetDataID().ID] = new SerializeVector3( transform.position);
            data.floatSaveData[GetDataID().ID + "health"] = this.maxHealth;
            data.floatSaveData[GetDataID().ID + "power"] = this.maxPower;

        }
        else
        {
            //保存坐标
            data.characterPosDict.Add(GetDataID().ID, new SerializeVector3 (transform.position));
            //保存血量能量
            data.floatSaveData.Add(GetDataID().ID + "health", this.maxHealth);
            data.floatSaveData.Add(GetDataID().ID + "power", this.maxPower);

        }
    }

    public void LoadData(Data data)
    {
        if (data.characterPosDict.ContainsKey(GetDataID().ID))
        {
            transform.position = data.characterPosDict[GetDataID().ID].ToVecort3();
            this.currentHealth = data.floatSaveData[GetDataID().ID + "health"];
            this.currentPower = data.floatSaveData[GetDataID().ID + "power"];
            OnHealthChange.Invoke(this);

        }
    }


}
