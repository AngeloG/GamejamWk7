using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Gamejam
{
  class Food : Entity
  {
    static float Gravity = 1f;
    public override void Update()
    {

      base.Update();
    }
    public override void Render()
    {
      Gamejam.SpriteBatch.Draw(Content.GetTexture("TestFood.png"), ent_vPosition, Color.White);

      base.Render();
    }
  }
}
