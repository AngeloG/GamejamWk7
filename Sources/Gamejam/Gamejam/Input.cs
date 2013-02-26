using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Gamejam
{
  public class Input
  {
    public static bool IsKeyPressed(Keys key)
    {
      return Keyboard.GetState().IsKeyDown(key);
    }

    public static bool IsKeyReleased(Keys key)
    {
      return !IsKeyPressed(key);
    }

    public static bool IsMouseLeftPressed()
    {
      return Mouse.GetState().LeftButton == ButtonState.Pressed;
    }

    public static bool IsMouseRightPressed()
    {
      return Mouse.GetState().RightButton == ButtonState.Pressed;
    }
  }
}
