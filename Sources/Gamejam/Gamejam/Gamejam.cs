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
using System.Diagnostics;
using System.IO;

namespace Gamejam
{
  public class Gamejam : Microsoft.Xna.Framework.Game
  {
    public GraphicsDeviceManager graphics;
    public SpriteBatch spriteBatch;

    public static Random rnd = new Random();

    public static float gam_fScreenWidth = 768;
    public static float gam_fScreenHeight = 1024;
    public static float gam_fGameScale = 0.75f;

    public static List<Entity> gam_aEntities = new List<Entity>();

    public static Vector2 ScaleVector(Vector2 v)
    {
      return v * gam_fGameScale;
    }

    public static void Assert(bool bCondition)
    {
      Debug.Assert(bCondition);
    }

    public static Player GetPlayer()
    {
      for (int i = 0; i < gam_aEntities.Count(); i++) {
        if (gam_aEntities[i].ent_strClassName == "Player") {
          return (Player)gam_aEntities[i];
        }
      }
      Assert(false);
      return null;
    }

    public static void PlaySound(string strFilename)
    {
      CContent.GetSound(strFilename).Play();
    }

    public static Gamejam Instance;

    public static SpriteBatch SpriteBatch
    {
      get { return Instance.spriteBatch; }
    }

    public Gamejam()
    {
      Instance = this;

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

      CContent.Initialize(Content, GraphicsDevice);

      // add player
      Player ply = new Player();
      ply.Initialize();

      //testing food rendering
      Food testFood = new Food();
      testFood.Initialize();
    }

    protected override void LoadContent()
    {
      spriteBatch = new SpriteBatch(GraphicsDevice);

      // precache all files
      string[] astrFiles = Directory.GetFiles(Content.RootDirectory);
      for (int i = 0; i < astrFiles.Length; i++) {
        string strFileName = astrFiles[i];
        strFileName = strFileName.Replace(Content.RootDirectory + "\\", "");

        Console.Write("Precaching " + strFileName + "... ");

        bool bFound = false;

        // if .xnb, this is _probably_ a font...
        if (strFileName.EndsWith(".xnb")) {
          CContent.GetFont(strFileName.Replace(".xnb", ""));
          bFound = true;
        }

        // if .wav, it's a sound
        if (strFileName.EndsWith(".wav")) {
          CContent.GetSound(strFileName);
          bFound = true;
        }

        // if .png, it's a texture
        if (strFileName.EndsWith(".png")) {
          CContent.GetTexture(strFileName);
          bFound = true;
        }

        if (bFound) {
          Console.WriteLine("Done");
        } else {
          Console.WriteLine("Not a precachable file");
        }
      }
    }

    protected override void UnloadContent()
    {

    }

    protected override void Update(GameTime gameTime)
    {
      if (Keyboard.GetState().IsKeyDown(Keys.Escape)) {
        this.Exit();
      }

      for (int i = 0; i < gam_aEntities.Count(); i++) {
        gam_aEntities[i].Update();
      }

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.White);
      spriteBatch.Begin();

      for (int i = 0; i < gam_aEntities.Count(); i++) {
        gam_aEntities[i].Render();
      }

      spriteBatch.End();
      base.Draw(gameTime);
    }
  }
}
