using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
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
    private VisualElement intentBar;
    private Enemy enemy;
    public VisualTreeAsset intentTemplate;
    private void OnEnable()
    {
        currentCharacter = GetComponent<CharacterBase>();
        enemy = GetComponent<Enemy>();
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

        intentBar = healthBar.Q<VisualElement>("IntentBar");

        //初始不可见
        defenseElement.style.display = DisplayStyle.None;
        buffRonudElement.style.display = DisplayStyle.None;
        defenseLabel.text = "0";
        buffRoundLabel.text = "0";



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
    public void UpdateIntentBar(EnemyAction currentAction)
    {
        var intent = intentTemplate.Instantiate();
        var intentElement = intent.Q<VisualElement>("Intent");
        var intentLabel = intent.Q<Label>("IntentAmount");
        intentBar.Add(intent);
        intentBar.style.display = DisplayStyle.Flex;
        intentLabel.text = currentAction.effect.value.ToString();
        intentElement.style.backgroundImage = new StyleBackground(currentAction.initentIcon);
    }
    /// <summary>
    /// 敌人回合结束时隐藏
    /// </summary>
    public void HideIntentBar()
    {
        intentBar.Clear();
        // 遍历intentBar的所有子物体
        // foreach (var child in intentBar.Children())
        // {
        //     intentBar.Remove(child);
        // }
        intentBar.style.display = DisplayStyle.None;
    }

}
