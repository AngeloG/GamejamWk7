using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Gamejam
{
  public class Player : Entity
  {
    public float ent_fSpeed = 3.0f;

    public Player()
    {
      SetTexture("Player.png");
      ent_vPosition = new Vector2(
        Gamejam.gam_fScreenWidth / 2,    // x
        Gamejam.gam_fScreenHeight - 30); // y
    }

    public override void Update()
    {
      if (Input.IsAnyKeyPressed(Keys.Left, Keys.A)) {
        ent_vVelocity.X = -ent_fSpeed;
      } else if (Input.IsAnyKeyPressed(Keys.Right, Keys.D)) {
        ent_vVelocity.X = ent_fSpeed;
      } else {
        ent_vVelocity.X = 0;
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
