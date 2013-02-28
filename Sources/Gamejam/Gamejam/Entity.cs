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

    public bool ent_bCanCollide = false;
    public Entity ent_entCollider;

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

    public void Destroy()
    {
      Gamejam.gam_aEntities.Remove(this);

      // set iterator back because it won't be valid anymore
      Gamejam.gam_iEntityIterator--;
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

      for (int i = 0; i < Gamejam.gam_aEntities.Count(); i++) {
        Entity ent = Gamejam.gam_aEntities[i];

        if (ent == this) {
          continue;
        }

        if (ent_bCanCollide) {
          if (ent.GetCollision().Intersects(GetCollision())) {
            if (ent.ent_entCollider != this) {
              ent.ent_entCollider = this;
              OnCollisionEnter(ent);
            } else {
              OnCollision(ent);
            }
          } else {
            if (ent.ent_entCollider == this) {
              ent.ent_entCollider = null;
              OnCollisionLeave(ent);
            }
          }
        }
      }
    }

    public virtual void Render()
    {

    }

    public virtual void OnCollisionEnter(Entity entOther)
    {

    }

    public virtual void OnCollision(Entity entOther)
    {

    }

    public virtual void OnCollisionLeave(Entity entOther)
    {

    }
  }
}
