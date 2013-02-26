using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Gamejam
{
  public class Entity
  {
    public Vector2 ent_vPosition;
    public Vector2 ent_vVelocity;

    public Entity()
    {
      ent_vPosition = new Vector2(0, 0);
      ent_vVelocity = new Vector2(0, 0);
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
