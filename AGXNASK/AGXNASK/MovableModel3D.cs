

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



public class MovableModel3D : Model3D {

//   public MovableModel3D(Stage theStage, string label, Vector3 position, 
//      Vector3 orientAxis, float radians, string meshFile)
//      : base(theStage, label, position, orientAxis, radians, meshFile) 
     public MovableModel3D(Stage theStage, string label, string meshFile)    
        : base(theStage, label, meshFile) 
         { }     

   public void reset() {
      foreach (Object3D obj in instance) obj.reset();
      }

   ///<summary>
   ///  pass through
   ///</summary>
   // override virtual DrawableGameComponent methods                   
   public override void Update(GameTime gameTime) {
      foreach (Object3D obj in instance) obj.updateBoundingSphere(); 
      base.Update(gameTime);
      }

   }
}
