using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Gamejam
{
  public class Player : Entity
  {
    public Player()
    {
      SetTexture("Player.png");
      ent_vPosition = new Vector2(
        Gamejam.ScreenWidth / 2,    // x
        Gamejam.ScreenHeight - 30); // y
    }

    public override void Update()
    {


      base.Update();
    }

    public override void Render()
    {
      Gamejam.SpriteBatch.Draw(ent_texTexture, GetCollision(), Color.White);

      base.Render();
    }
  }
}
