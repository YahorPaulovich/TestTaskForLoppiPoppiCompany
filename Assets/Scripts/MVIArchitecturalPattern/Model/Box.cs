using UnityEngine;

public class Box : MonoBehaviour
{
    public BoxData BoxData;
    private SpriteRenderer _spriteRenderer;

    //private void Awake()
    //{
    //    _spriteRenderer = GetComponent<SpriteRenderer>();

    //    Index = transform.GetSiblingIndex();
    //    Mark = Mark.None;
    //    IsMarked = false;
    //}

    //public void SetAsMarked(Sprite sprite, Mark mark, Color color)
    //{
    //    IsMarked = true;
    //    Mark = mark;

    //    _spriteRenderer.color = color;
    //    _spriteRenderer.sprite = sprite;

    //    GetComponent<CircleCollider2D>().enabled = false;
    //}
}
