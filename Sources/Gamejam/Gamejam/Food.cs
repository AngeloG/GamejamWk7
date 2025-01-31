﻿using System;
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

    public StabState stabState;
    public Entity stuckTo;
    public Vector2 stuckOffset;

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
      if (Gamejam.rnd.Next(0, 2) == 0)
      {
        ent_vVelocity.X *= -1;
      }
      ent_vVelocity.Y = 1;
    }

    public override void Update()
    {
      

      if (ent_vPosition.Y > Gamejam.gam_fScreenHeight + ent_vSize.Y / 2f) {
        Gamejam.GetPlayer().Hurt();
        Destroy();
      }

      switch (stabState)
      {
        case StabState.NONE:
          if (ent_vPosition.X > Gamejam.gam_fScreenWidth - RIGHT_WALL
            || ent_vPosition.X < LEFT_WALL)
            {
              ToggleDirection();
            }
          if (ent_vPosition.Y > Gamejam.gam_fScreenHeight + ent_vSize.Y / 2f)
          {
            Gamejam.GetPlayer().Hurt();
            Destroy();
          }
          break;
        case StabState.HIT:
          ent_vVelocity = Vector2.Zero;

          if (stuckTo.ent_vPosition.Y - this.ent_vPosition.Y < -30
            + ((Player)stuckTo).stuckFoodCount * 30)
          {
            this.stabState = StabState.STUCK;
            this.stuckOffset = this.ent_vPosition - stuckTo.ent_vPosition;
          }

          break;
        case StabState.STUCK:
          ent_vVelocity = Vector2.Zero;
          ent_vPosition = stuckTo.ent_vPosition + stuckOffset;

          break;
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
