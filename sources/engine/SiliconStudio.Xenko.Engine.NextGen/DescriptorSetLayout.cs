using SiliconStudio.Xenko.Graphics;
using SiliconStudio.Xenko.Shaders;

namespace RenderArchitecture
{
    // D3D11 version
    /// <summary>
    /// Defines a list of descriptor layout. This is used to allocate a <see cref="DescriptorSet"/>.
    /// </summary>
    public class DescriptorSetLayout
    {
        internal readonly int ElementCount;
        internal readonly Entry[] Entries;

        private DescriptorSetLayout(int elementCount, Entry[] entries)
        {
            ElementCount = elementCount;
            Entries = entries;
        }

        public static DescriptorSetLayout New(GraphicsDevice device, DescriptorSetLayoutBuilder builder)
        {
            return new DescriptorSetLayout(builder.ElementCount, builder.Entries.ToArray());
        }

        internal struct Entry
        {
            public string Name;
            public EffectParameterClass Class;
            public int ArraySize;
        }
    }
}