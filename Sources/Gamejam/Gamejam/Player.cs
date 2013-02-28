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

    public int ent_iCombo = 0;

    private int score = 0;

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

    public void GainScore(int addedScore)
    {
      score += addedScore;
    }

    public int GetScore()
    {
      return score;
    }

    private int entitiesDestroyed = 0;

    public override void Update()
    {
      if (!ent_bAlive) {
        return;
      }

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
          ent_iCombo = 0;

          entitiesDestroyed = 0;
          for (int i = 0; i < Gamejam.gam_aEntities.Count; i++)
          {
            if (Gamejam.gam_aEntities[i].ent_strClassName == "Food")
            {
              Food otherFood = (Food)Gamejam.gam_aEntities[i];
              if (otherFood.stabState != Food.StabState.NONE)
              {
                Gamejam.gam_aEntities.Remove(otherFood);
                i--;
                entitiesDestroyed++;
              }
            }
          }
          GainScore((int)Math.Pow(entitiesDestroyed, 2));
#if DEBUG
          Console.WriteLine("Score added: {0}. Total score now {1}",
            (int)Math.Pow(entitiesDestroyed, 2),
            score);
#endif
        }
      }

      if (Input.IsAnyKeyPressed(attackKeys)) {
        ent_bAttacking = true;
      }

      base.Update();
    }

    public override void Render()
    {
      if (!ent_bAlive) {
        return;
      }

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
          ent_iCombo++;

          if (ent_iCombo % 2 == 0 && ent_iCombo <= 6) {
            new Decal("Combo/Combo" + ent_iCombo + "x.png",
              1, ent_vPosition).Initialize();
          }

          Gamejam.PlaySound("Splash.wav");

          new Decal("Particles/Splash_0" + (Gamejam.rnd.Next(4) + 1) + ".png",
            0.2, entOther.ent_vPosition).Initialize();

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
