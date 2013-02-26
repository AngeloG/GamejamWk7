using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Gamejam
{
  class Food : Entity
  {
    public Food()
    {
      SetTexture("TestFood.png");
    }

    public enum Direction
    {
      LEFT,
      RIGHT
    }

    public Direction curDirection = Direction.RIGHT;
    public override void Update()
    {
      if (ent_vPosition.X > Gamejam.gam_fScreenWidth)
      {
        ToggleDirection();
      }
      else if (ent_vPosition.X < 0)
      {
        ToggleDirection();
      }

      ent_vPosition.X += curDirection == Direction.RIGHT ? 1 : -1; 

      base.Update();
    }

    private void ToggleDirection()
    {
      if (curDirection == Direction.LEFT)
      {
        curDirection = Direction.RIGHT;
      }
      else
      {
        curDirection = Direction.LEFT;
      }
    }

    public override void Render()
    {
      Gamejam.SpriteBatch.Draw(ent_texTexture,
        GetCollision(), Color.White);

      base.Render();
    }
  }
}
