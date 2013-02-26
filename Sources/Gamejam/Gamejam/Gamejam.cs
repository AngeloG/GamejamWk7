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

    public static Vector2 ScaleVector(Vector2 v)
    {
      return v / 2f;
    }

    public Gamejam()
    {
      graphics = new GraphicsDeviceManager(this);
      graphics.PreferredBackBufferWidth = 768;
      graphics.PreferredBackBufferHeight = 1024;

      graphics.PreferredBackBufferWidth /= 2;
      graphics.PreferredBackBufferHeight /= 2;

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
