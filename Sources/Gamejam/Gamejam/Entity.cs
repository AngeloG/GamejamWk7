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
      ent_texTexture = Content.GetTexture(strFilename);
      ent_vSize = new Vector2(ent_texTexture.Width, ent_texTexture.Height);
    }

    public Rectangle GetCollision()
    {
      return new Rectangle(
        (int)ent_vPosition.X, (int)ent_vPosition.Y,
        (int)ent_vSize.X, (int)ent_vSize.Y);
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
