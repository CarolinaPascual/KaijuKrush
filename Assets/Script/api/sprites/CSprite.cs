using UnityEngine;
using System.Collections;

public class CSprite : CGameObject
{
    private GameObject mSprite;
    private SpriteRenderer mSpriteRenderer;

    // Caching of mSprite.transform.
    private Transform mTransform;

    public CSprite()
    {
        mSprite = new GameObject();
        mSpriteRenderer = mSprite.AddComponent<SpriteRenderer>();

        mTransform = mSprite.transform;
        setRadius(getWidth() / 2);
    }

    override public void update()
    {
        base.update();
    }

    override public void render()
    {
        base.render();

        Vector3 pos = new Vector3(getX(), getY() * -1, 0.0f);
        mTransform.position = pos;
    }

    override public void destroy()
    {
        base.destroy();
        Object.Destroy(mSprite);
    }

    public void setImage(Sprite aSprite)
    {
        mSpriteRenderer.sprite = aSprite;
    }
    public float getWidth()
    {
        return mSpriteRenderer.bounds.size.x;
    }
    public float getHeight()
    {
        return mSpriteRenderer.bounds.size.y;
    }
    public void setSortingLayer(string aLayer)
    {
        mSpriteRenderer.sortingLayerName = aLayer;
    }

    public SpriteRenderer getSpriteRenderer()
    {
        return mSpriteRenderer;
    }

    public void setSpriteRenderer(SpriteRenderer aSprite)
    {
        mSpriteRenderer = aSprite;

    }
    public void setAlpha(float alpha)
    {
        Color color = mSpriteRenderer.material.color;
        mSpriteRenderer.material.color = new Color(color.r, color.g, color.b, alpha);
        
    }
    public float getAlpha()
    {
        Color color = mSpriteRenderer.material.color;
        return color.a;

    }
    public void setVisible(bool aIsVisible)
    {
        mSpriteRenderer.enabled = aIsVisible;
    }
    public void setName(string aName)
    {
        mSprite.name = aName;
    }

    // Resizea pero recibe un porcentaje
    public void setScale (float aScale)
    {
        float scale = aScale / 100;
        mSprite.transform.localScale = new Vector3(scale, scale, scale);        
    }

    
}
