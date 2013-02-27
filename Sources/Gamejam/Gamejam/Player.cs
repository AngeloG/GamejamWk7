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
    public float ent_fSpeed = 5.0f;
    public float ent_fAttackSpeed = 15.0f;
    public bool ent_bAttacking = false;

    public Player()
    {
      SetTexture("Spear/Spit.png");
      ent_vPosition = new Vector2(
        Gamejam.gam_fScreenWidth / 2,                 // x
        Gamejam.gam_fScreenHeight - ent_vSize.Y / 3); // y
    }

    public override void Update()
    {
      if (!ent_bAttacking) {
        if (Input.IsAnyKeyPressed(Keys.Left, Keys.A)) {
          ent_vVelocity.X = -ent_fSpeed;
        } else if (Input.IsAnyKeyPressed(Keys.Right, Keys.D)) {
          ent_vVelocity.X = ent_fSpeed;
        } else {
          ent_vVelocity.X = 0;
        }
        ent_vVelocity.Y = 0;
      } else {
        ent_vVelocity.X = 0;
        ent_vVelocity.Y = -ent_fAttackSpeed;

        if (ent_vPosition.Y <= 0) {
          ent_bAttacking = false;
          ent_vPosition.Y = Gamejam.gam_fScreenHeight - ent_vSize.Y / 3;
        }
      }

      if (Input.IsAnyKeyPressed(Keys.Up, Keys.W, Keys.LeftControl)) {
        ent_bAttacking = true;
      }

      base.Update();
    }

    public override void Render()
    {
      Gamejam.SpriteBatch.Draw(ent_texTexture, GetCollision(), Color.White);

      base.Render();
    }

    public override void OnCollisionEnter(Entity entOther)
    {
      Gamejam.PlaySound("Splash.wav");

      base.OnCollisionEnter(entOther);
    }

    public override void OnCollisionLeave(Entity entOther)
    {


      base.OnCollisionLeave(entOther);
    }
  }
}
