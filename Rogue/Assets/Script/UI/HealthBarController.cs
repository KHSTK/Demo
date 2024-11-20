using System.Data;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarController : MonoBehaviour
{
    private CharacterBase currentCharacter;
    [Header("Elements")]
    public Transform healthBarTransForm;
    private UIDocument healthBarDocument;
    private ProgressBar healthBar;
    private void Awake()
    {
        currentCharacter = GetComponent<CharacterBase>();
    }
    private void Start()
    {
        InitHealthBar();
        UpdataHealthBar();
    }
    private void Update()
    {
        UpdataHealthBar();
    }
    private void MoveToWorldPos(VisualElement element, Vector3 worldPos, Vector3 size)
    {
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
    }
}
