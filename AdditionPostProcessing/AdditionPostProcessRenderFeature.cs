using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AdditionPostProcessRenderFeature : ScriptableRendererFeature
{
    class CustomRenderPass : ScriptableRenderPass
    {
        RenderTargetIdentifier m_ColorAttachment;
        RenderTargetHandle m_Destination;
        
        const string k_RenderPostProcessingTag = "Render AdditionalPostProcessing Effects";
        const string k_RenderFinalPostProcessingTag = "Render Final AdditionalPostProcessing Pass";
        
        InvertColor m_InvertColor;
        
        MaterialLibrary m_Materials;
        
        RenderTargetHandle m_TemporaryColorTexture01;
        RenderTargetHandle m_TemporaryColorTexture02;
        RenderTargetHandle m_TemporaryColorTexture03;

        public CustomRenderPass(AdditionPostProcessData data)
        {
            m_Materials = new MaterialLibrary(data);
            m_TemporaryColorTexture01.Init("_TemporaryColorTexture1");
            m_TemporaryColorTexture02.Init("_TemporaryColorTexture2");
            m_TemporaryColorTexture03.Init("_TemporaryColorTexture3");
        }

        public void Setup(RenderTargetIdentifier source, RenderTargetHandle destination)
        {
            m_ColorAttachment = source;
            m_Destination = destination;
        }
        
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var stack = VolumeManager.instance.stack;
            m_InvertColor = stack.GetComponent<InvertColor>();
            var cmd = CommandBufferPool.Get(k_RenderPostProcessingTag);
            Render(cmd, ref renderingData);
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        private void Render(CommandBuffer cmd, ref RenderingData renderingData)
        {
            ref var cameraData = ref renderingData.cameraData;
            if (m_InvertColor.IsActive() && !cameraData.isSceneViewCamera)
            {
                SetupInvertColor(cmd, ref renderingData, m_Materials.invertColor);
            }
        }

        private void SetupInvertColor(CommandBuffer cmd, ref RenderingData renderingData, Material invertMaterial)
        {
            RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
            cmd.GetTemporaryRT(m_TemporaryColorTexture01.id, opaqueDesc);
            cmd.GetTemporaryRT(m_TemporaryColorTexture02.id, opaqueDesc);
            cmd.GetTemporaryRT(m_TemporaryColorTexture03.id, opaqueDesc);
            cmd.BeginSample("invertColor");
            cmd.Blit(this.m_ColorAttachment, m_TemporaryColorTexture01.Identifier());
            cmd.Blit(m_TemporaryColorTexture01.Identifier(), m_TemporaryColorTexture02.Identifier(), invertMaterial);
            cmd.Blit(m_TemporaryColorTexture02.Identifier(), m_ColorAttachment);
            cmd.Blit(m_TemporaryColorTexture02.Identifier(), this.m_Destination.Identifier());
            cmd.EndSample("invertColor");
        }
    }

    public AdditionPostProcessData postData;
    private CustomRenderPass m_ScriptablePass;
    

    public override void Create()
    {
        m_ScriptablePass = new CustomRenderPass(postData);
        m_ScriptablePass.renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
    }
    
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        var cameraColorTarget = renderer.cameraColorTarget;
        var dest = RenderTargetHandle.CameraTarget;
        if (postData == null) return;
        m_ScriptablePass.Setup(cameraColorTarget, dest);
        renderer.EnqueuePass(m_ScriptablePass);
    }
}


