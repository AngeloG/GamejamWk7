using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gamejam
{
  public class Entity
  {
    public string ent_strClassName;

    public Vector2 ent_vPosition;
    public Vector2 ent_vVelocity;
    public Vector2 ent_vSize;

    public Texture2D ent_texTexture;

    public Entity()
    {
      ent_vPosition = new Vector2(0, 0);
      ent_vVelocity = new Vector2(0, 0);
    }

    public void Initialize()
    {
      ent_strClassName = GetType().Name;
      Gamejam.gam_aEntities.Add(this);
    }

    public void SetTexture(string strFilename)
    {
      ent_texTexture = CContent.GetTexture(strFilename);
      ent_vSize = new Vector2(ent_texTexture.Width, ent_texTexture.Height);
    }

    public Rectangle GetCollision()
    {
      Vector2 vPos = Gamejam.ScaleVector(ent_vPosition);
      Vector2 vSize = Gamejam.ScaleVector(ent_vSize);

      return new Rectangle(
        (int)(vPos.X - vSize.X / 2), // x
        (int)(vPos.Y - vSize.Y / 2), // y
        (int)vSize.X,  // w
        (int)vSize.Y); // h
    }

    public virtual void Update()
    {
      ent_vPosition += ent_vVelocity;
    }

    public virtual void Render()
    {

    }
  }
}
