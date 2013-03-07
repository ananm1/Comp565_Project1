
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


public abstract class Agent : MovableModel3D {
   protected Object3D agentObject = null;
   protected Camera agentCamera, first, follow, above;
   public enum CameraCase { FirstCamera, FollowCamera, AboveCamera }



   public Agent(Stage stage, string label, Vector3 position, Vector3 orientAxis, 
      float radians, string meshFile) 
      : base(stage, label, meshFile)
      {
      addObject(position,orientAxis, radians);
      agentObject = instance.First<Object3D>(); 
      first =  new Camera(stage, agentObject, Camera.CameraEnum.FirstCamera); 
      follow = new Camera(stage, agentObject, Camera.CameraEnum.FollowCamera);
      above =  new Camera(stage, agentObject, Camera.CameraEnum.AboveCamera);
      stage.addCamera(first);
      stage.addCamera(follow);
      stage.addCamera(above);
      agentCamera = first;
      }
 
   // Properties  
 
  public Object3D AgentObject {
      get { return agentObject; }}
   
   public Camera AvatarCamera {
      get { return agentCamera; }
      set { agentCamera = value;}}

   public Camera First {
      get { return first; }}

   public Camera Follow {
      get { return follow; }}

   public Camera Above {
      get { return above; }}
            
   // Methods

   public override string ToString() {
      return agentObject.Name;
      }
      
   public void updateCamera() {
      agentCamera.updateViewMatrix();
      }
      
   public override void Update(GameTime gameTime) { 
      agentObject.updateMovableObject();
      base.Update(gameTime); 
      // Agent is in correct (X,Z) position on the terrain 
      // set height to be on terrain -- this is a crude "first approximation" solution.
      stage.setSurfaceHeight(agentObject);
      }
              
}}
