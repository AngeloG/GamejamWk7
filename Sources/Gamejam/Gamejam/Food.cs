using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Gamejam
{
  class Food : Entity
  {
    const float LEFT_WALL = 130;
    const float RIGHT_WALL = 130;
    static float playWidth = Gamejam.gam_fScreenWidth - LEFT_WALL - RIGHT_WALL;

    public enum StabState
    {
      NONE,
      HIT,
      STUCK
    }

    public Food()
    {
      int iRandomFood = Gamejam.rnd.Next(8) + 1;
      SetTexture("Food/Enemy_" + iRandomFood + ".png");
      ent_vPosition.X = (float)Gamejam.rnd.NextDouble() * playWidth + LEFT_WALL;
      ent_vVelocity.X = (float)Gamejam.rnd.NextDouble() * 3.0f + 1.0f;
      ent_vVelocity.Y = 1;
    }

    public override void Update()
    {
      if (ent_vPosition.X > Gamejam.gam_fScreenWidth - RIGHT_WALL
        || ent_vPosition.X < LEFT_WALL)
      {
        ToggleDirection();
      }

      if (ent_vPosition.Y > Gamejam.gam_fScreenHeight + ent_vSize.Y / 2f) {
        Gamejam.GetPlayer().Hurt();
        Destroy();
      }

      base.Update();
    }

    private void ToggleDirection()
    {
      ent_vVelocity.X = -ent_vVelocity.X;
    }

    public override void Render()
    {
      Gamejam.SpriteBatch.Draw(ent_texTexture,
        GetCollision(), Color.White);

      base.Render();
    }
  }
}
