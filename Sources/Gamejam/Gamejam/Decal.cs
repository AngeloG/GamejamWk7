using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gamejam
{
  public class Decal : Entity
  {
    public double ent_dSeconds;
    public DateTime ent_tmSpawned = DateTime.Now;

    public Decal(string strDecalFilename, double dSeconds, Vector2 vPos)
    {
      SetTexture(strDecalFilename);
      ent_dSeconds = dSeconds;
      ent_vPosition = vPos;
    }

    public override void Update()
    {
      if ((DateTime.Now - ent_tmSpawned).TotalSeconds >= ent_dSeconds) {
        Destroy();
      }

      base.Update();
    }

    public override void Render()
    {
      Gamejam.SpriteBatch.Draw(ent_texTexture, GetCollision(), Color.White);

      base.Render();
    }
  }
}
