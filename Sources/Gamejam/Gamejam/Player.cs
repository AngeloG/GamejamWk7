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

    public int ent_iLives = 3;
    public bool ent_bAlive = true;

    public int stuckFoodCount = 0;

    private static readonly Keys[] attackKeys = new Keys[] 
    {
      Keys.Up,
      Keys.Space,
      Keys.W,
      Keys.LeftControl
    };

    public Player()
    {
      SetTexture("Spear/Spit.png");
      ent_vPosition = new Vector2(
        Gamejam.gam_fScreenWidth / 2,                 // x
        Gamejam.gam_fScreenHeight - ent_vSize.Y / 3); // y
      ent_bCanCollide = true;
    }

    public void Die()
    {
      ent_bAlive = false;
    }

    public void Hurt()
    {
      if (ent_iLives == 0) {
        Die();
      } else {
        ent_iLives--;
      }
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

          for (int i = 0; i < Gamejam.gam_aEntities.Count; i++)
          {
            if (Gamejam.gam_aEntities[i].ent_strClassName == "Food")
            {
              Food otherFood = (Food)Gamejam.gam_aEntities[i];
              if (otherFood.stabState != Food.StabState.NONE)
              {
                Gamejam.gam_aEntities.Remove(otherFood);
                i--;
                //TODO: gain score
              }
            }
          }
        }
      }

      if (Input.IsAnyKeyPressed(attackKeys)) {
        ent_bAttacking = true;
      }

      base.Update();
    }

    public override void Render()
    {
      Gamejam.SpriteBatch.Draw(ent_texTexture, GetCollision(), Color.White);

      base.Render();
    }

    public override void OnCollision(Entity entOther)
    {
      if (entOther.ent_strClassName == "Food")
      {
        Food foodOther = (Food)entOther;
        if (foodOther.stabState == Food.StabState.NONE
          && Gamejam.GetPlayer().ent_bAttacking == true)
        {
          Gamejam.PlaySound("Splash.wav");

          foodOther.stabState = Food.StabState.HIT;
          foodOther.stuckTo = this;
        }
      }

      base.OnCollisionEnter(entOther);
    }

    public override void OnCollisionLeave(Entity entOther)
    {


      base.OnCollisionLeave(entOther);
    }
  }
}
