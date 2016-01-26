﻿using System.Collections.Generic;

namespace RenderArchitecture
{
    /// <summary>
    /// Entry-point for implementing rendering feature.
    /// </summary>
    public abstract class RenderFeature
    {
        public NextGenRenderSystem RenderSystem { get; internal set; }

        /// <summary>
        /// Initializes this instance.
        /// Query for specific cbuffer (either new one, like PerMaterial, or parts of an existing one, like PerObject=>Skinning)
        /// </summary>
        public virtual void Initialize()
        {
        }

        /// <summary>
        /// Extract data from entities, should be as fast as possible to not block simulation loop. It should be mostly copies, and the actual processing should be part of Prepare().
        /// </summary>
        public virtual void Extract()
        {
        }

        /// <summary>
        /// Perform effect permutations, before <see cref="Prepare"/>.
        /// </summary>
        /// <param name="renderSystem"></param>
        public virtual void PrepareEffectPermutations(NextGenRenderSystem renderSystem)
        {
        }

        /// <summary>
        /// Can perform much more work, even while game simulation keeps running.
        /// </summary>
        public virtual void Prepare()
        {
        }

        /// <summary>
        /// Performs GPU updates and/or draw.
        /// </summary>
        /// <param name="renderView"></param>
        /// <param name="renderViewStage"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        public virtual void Draw(RenderView renderView, RenderViewStage renderViewStage, int startIndex, int endIndex)
        {
        }
    }
}