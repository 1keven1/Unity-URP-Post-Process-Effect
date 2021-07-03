# 文件介绍

- AdditionPostProcessAsset文件夹是所有后处理需要的文件；
- PostProcessControl.cs是控制后处理效果的脚本，也挂在Volume上；

# 制作教程网页

更新中......

# 使用步骤

- 首先把所有文件拷贝到Asset目录底下

- 然后找到自己的UniversalRenderPipelineAsset

  在

  Edit -> Project Settings -> Graphics -> Scriptable Render Pipeline Settings

  和

  Project Settings -> Quality -> Rendering

  中

- 在自己的UniversalRenderPipelineAsset的细节面板的Renderer List中找到自己的RendererData

- 在自己的Renderer Data细节面板中点击Add Renderer Feature，添加Addition Post Process Render Feature

- 在资源面板右键，Creat -> Rendering -> Universial Render Pipeline -> Additional Post-process Data创建资源，这个资源创建在Asset -> Settings文件夹里，找到这个文件

-  在Additional Post-process Data细节面板点开Shaders属性，给Shader赋值，Shader在Override文件夹里

- 在自己的Renderer Data细节面板中New Addition Post Process Render Feature的Post Data属性中选择AdditionalPostProcessData

- 到这里画面应该正常了，如果还是黑的肯定有问题了。

- 在场景中新建一个Volume -> Global Volume，细节面板中的Volume组件里点击Profile的New

- 点击Add Override，其中Addition-post-processing即为我写的后期处理效果。

- 将相机细节面板的Rendering -> Post Processing勾上

# 后期加入别的效果

- 实现shader和VolumeComponent的CS文件；
- 在AdditionPostProcessData和MaterialLibrary中加入新的shader；
- 在ScriptableRenderPass中加入材质变量、效果Setup函数，然后在Render函数中调用；

# 使用自带的脚本实验

把PostProcessControl.cs挂在Volume上，开始游戏按i切换是否反色。

