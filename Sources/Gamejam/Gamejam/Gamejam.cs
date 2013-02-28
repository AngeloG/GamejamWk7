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
  public enum GameState
  {
    Game,
    StartScreen,
    HighScores,
  }

  public class Gamejam : Microsoft.Xna.Framework.Game
  {
    public GraphicsDeviceManager graphics;
    public SpriteBatch spriteBatch;

    public static Random rnd = new Random();

    public static float gam_fScreenWidth = 768;
    public static float gam_fScreenHeight = 1024;
    public static float gam_fGameScale = 0.75f;
    public static float gam_fSpies = -150;

    public static GameState gam_gsGameState = GameState.StartScreen;

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

      IsMouseVisible = true;
    }

    protected override void Initialize()
    {
      base.Initialize();

      CContent.Initialize(Content, GraphicsDevice);

#if !DEBUG
      // start music
      MediaPlayer.IsRepeating = true;
      MediaPlayer.Play(CContent.GetSong("BackgroundMusic.mp3"));
#endif

      // precache all files
      string[] astrFiles = Directory.GetFiles(Content.RootDirectory, "*.*",
        SearchOption.AllDirectories);

      for (int i = 0; i < astrFiles.Length; i++) {
        string strFileName = astrFiles[i];
        strFileName = strFileName.Replace(Content.RootDirectory + "\\", "");

        Console.Write("Precaching " + strFileName + "... ");

        bool bFound = false;

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


    }

    protected override void UnloadContent()
    {

    }

    public Rectangle GetScreenRectangle()
    {
      return new Rectangle(
        0, 0, // x, y
        (int)(gam_fScreenWidth * gam_fGameScale),   // w
        (int)(gam_fScreenHeight * gam_fGameScale)); // h
    }

    public bool IsAreaClicked(Rectangle rectArea)
    {
      // scale area to game window
      int iLeft = (int)((float)rectArea.Left * gam_fGameScale);
      int iTop = (int)((float)rectArea.Top * gam_fGameScale);
      int iWidth = (int)((float)rectArea.Width * gam_fGameScale);
      int iHeight = (int)((float)rectArea.Height * gam_fGameScale);
      rectArea = new Rectangle(iLeft, iTop, iWidth, iHeight);

      // check click
      return Input.IsMouseLeftPressed() &&
        rectArea.Contains(Input.GetMousePosition());
    }

    protected override void Update(GameTime gameTime)
    {
      if (Keyboard.GetState().IsKeyDown(Keys.Escape)) {
        this.Exit();
      }

      switch (gam_gsGameState) {
        case GameState.Game:
          for (int i = 0; i < gam_aEntities.Count(); i++) {
            gam_aEntities[i].Update();
          }
          break;

        case GameState.StartScreen:
          // start game button
          if (IsAreaClicked(new Rectangle(200, 705, 368, 149))) {
            gam_gsGameState = GameState.Game;
          }

          // show pony button
          if (IsAreaClicked(new Rectangle(333, 147, 18, 24))) {
            if (gam_fSpies == -150) {
              gam_fSpies = -149;
              PlaySound("Yay.wav");
            }
          }

          if (gam_fSpies > -150) {
            gam_fSpies += 10f;
            if (gam_fSpies == 321f) {
              PlaySound("Squee.wav");
            }
            if (gam_fSpies > gam_fScreenWidth) {
              gam_fSpies = -150;
            }
          }
          break;

        case GameState.HighScores:

          break;
      }

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.White);
      spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

      switch (gam_gsGameState) {
        case GameState.Game:
          // render background
          spriteBatch.Draw(CContent.GetTexture("Background/Background.png"),
            GetScreenRectangle(), Color.White);

          // render entities
          for (int i = 0; i < gam_aEntities.Count(); i++) {
            gam_aEntities[i].Render();
          }
          break;

        case GameState.StartScreen:
          // render background
          spriteBatch.Draw(CContent.GetTexture("Background/StartScherm.png"),
            GetScreenRectangle(), Color.White);

          Rectangle rectPinkie = new Rectangle(
            (int)(gam_fSpies * gam_fGameScale), // x
            (int)(100f * gam_fGameScale),       // y
            (int)(150f * gam_fGameScale),       // w
            (int)(150f * gam_fGameScale));      // h

          // render spy
          spriteBatch.Draw(CContent.GetTexture("Pinkie.png"), rectPinkie,
            Color.White);
          break;

        case GameState.HighScores:

          break;
      }

      spriteBatch.End();
      base.Draw(gameTime);
    }
  }
}
