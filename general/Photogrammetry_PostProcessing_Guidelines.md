Photogrammetry Post-Processing Guidelines
===
_by Georgios Adamopoulos_  

About
---
Good quality photogrammetry or LIDAR scanning typically results in multi-GB meshes.  

Only a lunatic would dare to put such a large mesh in a game, let alone an Oculus Quest game.  

Thankfully there are workflows that allow us to _dramatically_ reduce the poly-count of a mesh, without a **perceptible** loss in visual quality.  

These workflows can be implemented in various software, and you can find variations of them in online tutorials.  

We will attempt to present them in a general way, providing as many alternatives as possible.  


[Step 0: Read the Guide](https://unity3d.com/files/solutions/photogrammetry/Unity-Photogrammetry-Workflow_2017-07_v2.pdf)
---
Pay special attention to chapter 5.

Step 1: High - To - Low
---
### Software needed
* **Recommended:** [**Instant Meshes**](https://github.com/wjakob/instant-meshes) (free)  
  + [Windows](https://instant-meshes.s3.eu-central-1.amazonaws.com/Release/instant-meshes-windows.zip)
  + [Mac](https://instant-meshes.s3.eu-central-1.amazonaws.com/instant-meshes-macos.zip)
  + [Linux](https://instant-meshes.s3.eu-central-1.amazonaws.com/instant-meshes-linux.zip)  

or  
* **Any major 3D package (Maya, 3ds Max, Cinema4D, Modo etc)**  
or  
* [**Blender**](https://www.blender.org/) (free)  


### Description
First step in our process is to reduce the polygon count of our mesh.  

Lots of software can do that, but we find Instant Meshes to be one of the fastest, most robust, and easiest to use.   
On top of that it's free & open source! Yay!  

Instant Meshes tries to align the generated polygons to the curvature field of your original mesh, attempting to preserve features and sharp creases.  

As a rule of thumb, try to reduce the triangle count to **1/10** of the original, and adjust accordingly, until you are satisfied with the quality. Keep in mind, the polygon count of the mesh **can** and **should** be as low as possible.  

Don't worry if your model looks like it lost all of its beauty. We will get that back in a while!  

_Patience you must have, young padawan!_

### Usage of Instant Meshes
[Video](https://www.youtube.com/watch?v=U6wtw6W4x3I)  
To get started, launch the binary and select a dataset using the "Open mesh" button on the top left (the application must be located in the same directory as the 'datasets' folder, otherwise the panel will be empty).

The standard workflow is to solve for an orientation field (first blue button) and a position field (second blue button) in sequence, after which the 'Export mesh' button becomes active. Many user interface elements display a descriptive message when hovering the mouse cursor above for a second.

A range of additional information about the input mesh, the computed fields, and the output mesh can be visualized using the check boxes accessible via the 'Advanced' panel.

Clicking the left mouse button and dragging rotates the object; right-dragging (or shift+left-dragging) translates, and the mouse wheel zooms. The fields can also be manipulated using brush tools that are accessible by clicking the first icon in each 'Tool' row.

<img src="https://raw.githubusercontent.com/GeorgeAdamon/dfpi/master/general/Photogrammetry_Resources_Screenshots/InstantMeshes.png" />

Step 2: UV Unwrapping
---
### Software Needed
* **Recommended:** [**Rizom UV**](https://www.rizom-lab.com/rizomuv-vs/) (70% student discount)  
or  
* **Any major commercial 3D package (Maya, 3ds Max, Cinema4D, Modo etc)**  
or  
* [**Blender**](https://www.blender.org/) (free)  


### Description
Once our low-poly mesh is exported, we need to make sure its UV coordinates are not overlapping.  

We do not necessarily need to create a perfect UV unwrapping, minimizing distortion, hiding seams, packing as tightly as possible etc, although learning how to do the above will certainly help in your career.  

For now what's enough is for the UVs to be auto-split and auto-packed by our software.

### Automatic UV-Unwrapping in Cinema4D (Process is similar for other packages)

#### Switch Layout to UV Edit  

<img src="https://raw.githubusercontent.com/GeorgeAdamon/dfpi/master/general/Photogrammetry_Resources_Screenshots/UV 1.png" />

#### Prepare for Auto-Layout

<img src="https://raw.githubusercontent.com/GeorgeAdamon/dfpi/master/general/Photogrammetry_Resources_Screenshots/UV 2.png" />

#### Cubic Auto-Layout

<img src="https://raw.githubusercontent.com/GeorgeAdamon/dfpi/master/general/Photogrammetry_Resources_Screenshots/UV 3.png" />

#### Angle Auto-Layout

<img src="https://raw.githubusercontent.com/GeorgeAdamon/dfpi/master/general/Photogrammetry_Resources_Screenshots/UV 4.png" />


Step 3: Texture Transfer
---
### Software Needed
* **Recommended:** [**Substance Designer**](https://www.substance3d.com/education/) (Free educational version)  
or  
* **Maya**  
or  
* [**Blender**](https://www.blender.org/) (free)  


### Description
Once our low-poly mesh is uv-unwrapped, and we have made sure that **it perfectly aligns** with the original (you can test by copy-pasting the original inside the file containing the low-poly), we are ready to perform the last, magical step:  

Transfering high quality information from the high-poly to the low-poly model in the form of textures!  

### Process

#### New Package
<img src="https://raw.githubusercontent.com/GeorgeAdamon/dfpi/master/general/Photogrammetry_Resources_Screenshots/Substance1.png" />

#### Linking High-Res Mesh
<img src="https://raw.githubusercontent.com/GeorgeAdamon/dfpi/master/general/Photogrammetry_Resources_Screenshots/Substance2.png" />

<img src="https://raw.githubusercontent.com/GeorgeAdamon/dfpi/master/general/Photogrammetry_Resources_Screenshots/Substance3.png" />

#### Linking Low-Res Mesh
<img src="https://raw.githubusercontent.com/GeorgeAdamon/dfpi/master/general/Photogrammetry_Resources_Screenshots/Substance4.png" />

#### Linking Original Color Texture
<img src="https://raw.githubusercontent.com/GeorgeAdamon/dfpi/master/general/Photogrammetry_Resources_Screenshots/Substance4-5.png" />

#### Linking Complete
<img src="https://raw.githubusercontent.com/GeorgeAdamon/dfpi/master/general/Photogrammetry_Resources_Screenshots/Substance5.png" />

#### Start Baking
<img src="https://raw.githubusercontent.com/GeorgeAdamon/dfpi/master/general/Photogrammetry_Resources_Screenshots/Substance6.png" />

#### Select High-Res Mesh
<img src="https://raw.githubusercontent.com/GeorgeAdamon/dfpi/master/general/Photogrammetry_Resources_Screenshots/Substance7.png" />

#### Set Baking Settings
<img src="https://raw.githubusercontent.com/GeorgeAdamon/dfpi/master/general/Photogrammetry_Resources_Screenshots/Substance8.png" />

#### Select Original Texture
<img src="https://raw.githubusercontent.com/GeorgeAdamon/dfpi/master/general/Photogrammetry_Resources_Screenshots/Substance9.png" />

#### Baking
<img src="https://raw.githubusercontent.com/GeorgeAdamon/dfpi/master/general/Photogrammetry_Resources_Screenshots/Substance10.png" />

#### Finish
<img src="https://raw.githubusercontent.com/GeorgeAdamon/dfpi/master/general/Photogrammetry_Resources_Screenshots/Substance11.png" />

### Results

<img src="https://raw.githubusercontent.com/GeorgeAdamon/dfpi/master/general/Photogrammetry_Resources_Screenshots/Substance12.png" />

<img src="https://raw.githubusercontent.com/GeorgeAdamon/dfpi/master/general/Photogrammetry_Resources_Screenshots/Substance13.png" />

<img src="https://raw.githubusercontent.com/GeorgeAdamon/dfpi/master/general/Photogrammetry_Resources_Screenshots/Substance14.png" />

<img src="https://raw.githubusercontent.com/GeorgeAdamon/dfpi/master/general/Photogrammetry_Resources_Screenshots/Substance15.png" />

Step 4: Finally
---
Now you can bring your **low-poly** model in Unity, along with the textures you just generated, make a new Material and attach the color texture to **Albedo**, and the normal texture to **Normal Map**, the Ambient Occlusion texture in the **Occlusion Map** or optionally use the other textures (thickness, curvature etc) to create some interesting maps in Photoshop, and use them instead of the color texture.

Word of Advice! 
---
Substance Designer is an extremely powerful app. [**Spend time to learn it**](https://academy.substance3d.com/search?software=substance%20designer)
