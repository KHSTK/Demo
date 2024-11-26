using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarController : MonoBehaviour
{
    private CharacterBase currentCharacter;
    [Header("Elements")]
    public Transform healthBarTransForm;
    private UIDocument healthBarDocument;
    private ProgressBar healthBar;
    private VisualElement defenseElement, buffRonudElement;
    private Label defenseLabel, buffRoundLabel;
    [Header("Buff素材")]
    public List<Sprite> buffSpriteList;

    private Enemy enemy;
    private VisualElement intentElement;
    private Label intentLabel;
    public VisualTreeAsset intentTemplate;

    private void Awake()
    {
        currentCharacter = GetComponent<CharacterBase>();
        enemy = GetComponent<Enemy>();

    }
    private void OnEnable()
    {
        Debug.Log("血条产生坐标：" + healthBarTransForm.position);
        InitHealthBar();
        UpdataHealthBar();
    }
    private void Update()
    {
        UpdataHealthBar();
    }
    private void MoveToWorldPos(VisualElement element, Vector3 worldPos, Vector3 size)
    {
        Debug.Log("血条产生坐标：" + worldPos);
        Rect rect = RuntimePanelUtils.CameraTransformWorldToPanelRect(element.panel, worldPos, size, Camera.main);
        element.transform.position = rect.position;
    }

    [ContextMenu("Get UI Position")]
    public void InitHealthBar()
    {
        healthBarDocument = GetComponent<UIDocument>();
        healthBar = healthBarDocument.rootVisualElement.Q<ProgressBar>("HealthBar");
        healthBar.highValue = currentCharacter.MaxHp;
        MoveToWorldPos(healthBar, healthBarTransForm.position, Vector2.zero);

        defenseElement = healthBar.Q<VisualElement>("Defense");
        defenseLabel = defenseElement.Q<Label>("DefenseAmount");

        buffRonudElement = healthBar.Q<VisualElement>("Buff");
        buffRoundLabel = buffRonudElement.Q<Label>("BuffRoundAmount");

        intentElement = healthBar.Q<VisualElement>("Intent");
        intentLabel = healthBar.Q<Label>("IntentAmount");

        //初始不可见
        defenseElement.style.display = DisplayStyle.None;
        buffRonudElement.style.display = DisplayStyle.None;
        intentElement.style.display = DisplayStyle.None;
        defenseLabel.text = "0";
        buffRoundLabel.text = "0";
        intentLabel.text = "0";



    }
    [ContextMenu("Update HealthBar")]
    public void UpdataHealthBar()
    {
        if (currentCharacter.isDead)
        {
            healthBar.style.display = DisplayStyle.None;
            return;
        }
        if (healthBar != null)
        {
            healthBar.title = $"{currentCharacter.CurrentHp}/{currentCharacter.MaxHp}";
            healthBar.value = currentCharacter.CurrentHp;
            healthBar.RemoveFromClassList("highHealth");
            healthBar.RemoveFromClassList("mediumHealth");
            healthBar.RemoveFromClassList("lowHealth");
            var percent = (float)currentCharacter.CurrentHp / (float)currentCharacter.MaxHp;
            if (percent < 0.3f)
            {
                healthBar.AddToClassList("lowHealth");
            }
            else if (percent < 0.6f)
            {
                healthBar.AddToClassList("mediumHealth");
            }
            else
            {
                healthBar.AddToClassList("highHealth");
            }

        }
        //护甲更新
        defenseElement.style.display = currentCharacter.defense.currentValue > 0 ? DisplayStyle.Flex : DisplayStyle.None;
        defenseLabel.text = currentCharacter.defense.currentValue.ToString();
        //buff更新
        buffRonudElement.style.display = currentCharacter.buffRound.currentValue > 0 ? DisplayStyle.Flex : DisplayStyle.None;
        buffRoundLabel.text = currentCharacter.buffRound.currentValue.ToString();
        buffRonudElement.style.backgroundImage = currentCharacter.baseStrong > 1 ? new StyleBackground(buffSpriteList[0]) : new StyleBackground(buffSpriteList[1]);
    }

    [ContextMenu("Update intentBar")]
    public void UpdateIntentBar()
    {
        intentElement.style.display = DisplayStyle.Flex;
        intentElement.style.backgroundImage = new StyleBackground(enemy.currentAction.initentIcon);
        intentLabel.text = enemy.currentAction.effect.value.ToString();
    }
    /// <summary>
    /// 敌人回合结束时隐藏
    /// </summary>
    public void HideIntentBar()
    {
        intentElement.style.display = DisplayStyle.None;
    }

}
