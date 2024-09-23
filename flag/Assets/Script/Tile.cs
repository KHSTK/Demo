using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;//瓦片
    [SerializeField] private Sprite [] sprites;//瓦片集
    [SerializeField] public bool canWalk;
    public LayerMask ObLayerMask;
    public Color highlightColor;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        int randomNum = Random.Range(0, sprites.Length);//0到瓦片集的长度，获取下标
        spriteRenderer.sprite = sprites[randomNum];

        CheckObstacle();
    }
    private void OnMouseEnter()
    {
        Debug.Log("MouseEnter");
        transform.localScale += Vector3.one * 0.05f;
        spriteRenderer.sortingOrder = 25; 
    }
    private void OnMouseExit()
    {
        Debug.Log("MouseExit");
        transform.localScale -= Vector3.one * 0.05f;
        spriteRenderer.sortingOrder = 0;
    }
    private void CheckObstacle()
    {
       Collider2D collider2D= Physics2D.OverlapCircle(transform.position, spriteRenderer.bounds.extents.x,ObLayerMask);
        if (collider2D != null)
        {
            canWalk = false;
        }
        else 
        {
            canWalk = true;
        }
    }
    public void HighlightTile()
    {
        if (canWalk)
        {
            spriteRenderer.color = highlightColor;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }
    public void ResetTile()
    {
        spriteRenderer.color = Color.white;
    }
    private void OnMouseDown()
    {
        if(canWalk&&GameManager.instance.selectedUnit != null&&GameManager.instance.selectedUnit.hasMove==false)
        {
            GameManager.instance.selectedUnit.Move(this.transform);
        }
    }
}
