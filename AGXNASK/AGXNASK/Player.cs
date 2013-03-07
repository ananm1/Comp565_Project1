
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


namespace AGXNASK {

public class Player : Agent {
   private GamePadState oldGamePadState;
   private KeyboardState oldKeyboardState;  
   private int rotate;
   private float angle;
   private Matrix initialOrientation;

   public Player(Stage theStage, string label, Vector3 pos, Vector3 orientAxis, 
   float radians, string meshFile)
   : base(theStage, label, pos, orientAxis, radians, meshFile)
      {  // change names for on-screen display of current camera
      first.Name =  "First";
      follow.Name = "Follow";
      above.Name =  "Above";
      IsCollidable = true;  // Player test collisions
      rotate = 0;
      angle = 0.01f;
      initialOrientation = agentObject.Orientation;
      }


   public override void Update(GameTime gameTime) {
      GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
      if (gamePadState.IsConnected) {
            if (gamePadState.Buttons.X == ButtonState.Pressed) stage.Exit();
            else if (gamePadState.Buttons.A == ButtonState.Pressed &&
               oldGamePadState.Buttons.A != ButtonState.Pressed) stage.nextCamera();
            else if (gamePadState.Buttons.B == ButtonState.Pressed &&
               oldGamePadState.Buttons.B != ButtonState.Pressed) stage.Fog = ! stage.Fog;
            else if (gamePadState.Buttons.RightShoulder == ButtonState.Pressed &&
               oldGamePadState.Buttons.RightShoulder != ButtonState.Pressed) 
               stage.FixedStepRendering = ! stage.FixedStepRendering;
            // allow more than one gamePadState to be pressed
            if (gamePadState.DPad.Up == ButtonState.Pressed) agentObject.Step++;
            if (gamePadState.DPad.Down == ButtonState.Pressed) agentObject.Step--;
            if (gamePadState.DPad.Left == ButtonState.Pressed) rotate++;
            if (gamePadState.DPad.Right == ButtonState.Pressed) rotate--;
            oldGamePadState = gamePadState;
            }
         else { // no gamepad assume use of keyboard
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.R) && !oldKeyboardState.IsKeyDown(Keys.R)) 
               agentObject.Orientation = initialOrientation; 
            // allow more than one keyboardState to be pressed
            if (keyboardState.IsKeyDown(Keys.Up)) agentObject.Step++;
            if (keyboardState.IsKeyDown(Keys.Down)) agentObject.Step--; 
            if (keyboardState.IsKeyDown(Keys.Left)) rotate++;
            if (keyboardState.IsKeyDown(Keys.Right)) rotate--;
            // sk 565 old not used with terrain following -- can be useful in debugging
           // if (keyboardState.IsKeyDown(Keys.PageUp)) vertical++;    
           // if (keyboardState.IsKeyDown(Keys.PageDown)) vertical--;
            oldKeyboardState = keyboardState;    // Update saved state.
            } 
      agentObject.Yaw = rotate * angle;
      base.Update(gameTime);
      rotate = agentObject.Step = 0;
      }
   }
}
