﻿// <auto-generated>
// Do not edit this file yourself!
//
// This code was generated by Xenko Shader Mixin Code Generator.
// To generate it yourself, please install SiliconStudio.Xenko.VisualStudio.Package .vsix
// and re-save the associated .xkfx.
// </auto-generated>

using System;
using SiliconStudio.Core;
using SiliconStudio.Xenko.Rendering;
using SiliconStudio.Xenko.Graphics;
using SiliconStudio.Xenko.Shaders;
using SiliconStudio.Core.Mathematics;
using Buffer = SiliconStudio.Xenko.Graphics.Buffer;

using SiliconStudio.Xenko.Rendering;
using SiliconStudio.Xenko.Rendering.Materials;
namespace RenderArchitecture.Effects
{
    internal static partial class ShaderMixins
    {
        internal partial class TestEffect  : IShaderMixinBuilder
        {
            public void Generate(ShaderMixinSource mixin, ShaderMixinContext context)
            {
                context.Mixin(mixin, "TransformationBase");
                context.Mixin(mixin, "NormalStream");
                context.Mixin(mixin, "TransformationWAndVP");
                if (context.GetParam(MaterialKeys.HasNormalMap))
                {
                    context.Mixin(mixin, "NormalFromNormalMapping");
                }
                else
                {
                    context.Mixin(mixin, "NormalFromMesh");
                }
                var extensionPixelStageSurfaceShaders = context.GetParam(MaterialKeys.PixelStageSurfaceShaders);
                if (extensionPixelStageSurfaceShaders != null)
                {
                    context.Mixin(mixin, "MaterialSurfacePixelStageCompositor");

                    {
                        var __mixinToCompose__ = (extensionPixelStageSurfaceShaders);
                        var __subMixin = new ShaderMixinSource();
                        context.PushComposition(mixin, "materialPixelStage", __subMixin);
                        context.Mixin(__subMixin, __mixinToCompose__);
                        context.PopComposition();
                    }

                    {
                        var __mixinToCompose__ = context.GetParam(MaterialKeys.PixelStageStreamInitializer);
                        var __subMixin = new ShaderMixinSource();
                        context.PushComposition(mixin, "streamInitializerPixelStage", __subMixin);
                        context.Mixin(__subMixin, __mixinToCompose__);
                        context.PopComposition();
                    }
                    var extensionPixelStageSurfaceFilter = context.GetParam(MaterialKeys.PixelStageSurfaceFilter);
                    if (extensionPixelStageSurfaceFilter != null)
                    {
                        context.Mixin(mixin, (extensionPixelStageSurfaceFilter));
                    }
                }
                var directLightGroups = context.GetParam(LightingKeys.DirectLightGroups);
                if (directLightGroups != null)
                {
                    foreach(var directLightGroup in directLightGroups)

                    {

                        {
                            var __mixinToCompose__ = (directLightGroup);
                            var __subMixin = new ShaderMixinSource();
                            context.PushCompositionArray(mixin, "directLightGroups", __subMixin);
                            context.Mixin(__subMixin, __mixinToCompose__);
                            context.PopComposition();
                        }
                    }
                }
                var environmentLights = context.GetParam(LightingKeys.EnvironmentLights);
                if (environmentLights != null)
                {
                    foreach(var environmentLight in environmentLights)

                    {

                        {
                            var __mixinToCompose__ = (environmentLight);
                            var __subMixin = new ShaderMixinSource();
                            context.PushCompositionArray(mixin, "environmentLights", __subMixin);
                            context.Mixin(__subMixin, __mixinToCompose__);
                            context.PopComposition();
                        }
                    }
                }
                if (context.ChildEffectName == "GBuffer")
                {
                    context.Mixin(mixin, "GBuffer");
                    return;
                }
                if (context.ChildEffectName == "ShadowMapCaster")
                {
                    context.Mixin(mixin, "ShadowMapCaster");
                    return;
                }
            }

            [ModuleInitializer]
            internal static void __Initialize__()

            {
                ShaderMixinManager.Register("TestEffect", new TestEffect());
            }
        }
    }
}
