using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Gamejam
{
  public class Gamejam : Microsoft.Xna.Framework.Game
  {
    public GraphicsDeviceManager graphics;
    public SpriteBatch spriteBatch;

    public static float gam_fScreenWidth = 768;
    public static float gam_fScreenHeight = 1024;
    public static float gam_fGameScale = 0.75f;

    public static Vector2 ScaleVector(Vector2 v)
    {
      return v * gam_fGameScale;
    }

    public Gamejam()
    {
      graphics = new GraphicsDeviceManager(this);

      float fScreenW = gam_fScreenWidth * gam_fGameScale;
      float fScreenH = gam_fScreenHeight * gam_fGameScale;

      graphics.PreferredBackBufferWidth = (int)fScreenW;
      graphics.PreferredBackBufferHeight = (int)fScreenH;

      // we only use the Content object for actual content that has to be
      // compiled, we don't use it for things like textures/sounds, as these
      // are handled by ContentManager.
#if DEBUG
      Content.RootDirectory = "..\\..\\Content";
#else
      Content.RootDirectory = "..\\Content";
#endif
    }

    protected override void Initialize()
    {


      base.Initialize();
    }

    protected override void LoadContent()
    {
      spriteBatch = new SpriteBatch(GraphicsDevice);


    }

    protected override void UnloadContent()
    {

    }

    protected override void Update(GameTime gameTime)
    {
      if (Keyboard.GetState().IsKeyDown(Keys.Escape)) {
        this.Exit();
      }



      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.White);
      spriteBatch.Begin();



      spriteBatch.End();
      base.Draw(gameTime);
    }
  }
}
