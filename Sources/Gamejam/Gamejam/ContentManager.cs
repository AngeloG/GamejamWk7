using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using System.IO;

namespace Gamejam
{
  public class Content
  {
    static ContentManager cm_cmContentManager;
    static GraphicsDevice cm_gdGraphicsDevice;

    static Dictionary<string, Texture2D> cm_dicTextures;
    static Dictionary<string, SoundEffect> cm_dicSounds;
    static Dictionary<string, SpriteFont> cm_dicFonts;

#if DEBUG
    static string cm_strPath = "..\\..\\Content\\";
#else
    static string cm_strPath = "..\\Content\\";
#endif

    public static void Initialize(ContentManager cmManager,
      GraphicsDevice gdDevice)
    {
      cm_cmContentManager = cmManager;
      cm_gdGraphicsDevice = gdDevice;

      cm_dicTextures = new Dictionary<string, Texture2D>();
      cm_dicSounds = new Dictionary<string, SoundEffect>();
    }

    public static Texture2D GetTexture(string strFilename)
    {
      if (cm_dicTextures.ContainsKey(strFilename)) {
        return cm_dicTextures[strFilename];
      } else {
        Texture2D texTexture = Texture2D.FromStream(cm_gdGraphicsDevice,
          File.OpenRead(cm_strPath + strFilename));
        cm_dicTextures[strFilename] = texTexture;
        return texTexture;
      }
    }

    public static SoundEffect GetSound(string strFilename)
    {
      if (cm_dicSounds.ContainsKey(strFilename)) {
        return cm_dicSounds[strFilename];
      } else {
        SoundEffect sndSound = SoundEffect.FromStream(
          File.OpenRead(cm_strPath + strFilename));
        cm_dicSounds[strFilename] = sndSound;
        return sndSound;
      }
    }

    public static SpriteFont GetFont(string strFilename)
    {
      if (cm_dicFonts.ContainsKey(strFilename)) {
        return cm_dicFonts[strFilename];
      } else {
        SpriteFont fntFont = cm_cmContentManager.Load<SpriteFont>(strFilename);
        cm_dicFonts[strFilename] = fntFont;
        return fntFont;
      }
    }
  }
}
