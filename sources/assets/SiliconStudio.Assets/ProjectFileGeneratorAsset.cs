﻿using SiliconStudio.Core;

namespace SiliconStudio.Assets
{
    [DataContract("ProjectSourceCodeWithFileGeneratorAsset")]
    public abstract class ProjectSourceCodeWithFileGeneratorAsset : ProjectSourceCodeAsset, IProjectFileGeneratorAsset
    {
        /// <inheritdoc/>
        [DataMember(Mask = DataMemberAttribute.IgnoreMask)]
        [Display(Browsable = false)]
        public abstract string Generator { get; set; }

        /// <inheritdoc/>
        [DataMember(Mask = DataMemberAttribute.IgnoreMask)]
        [Display(Browsable = false)]
        public string GeneratedAbsolutePath { get; set; }

        /// <inheritdoc/>
        [DataMember(Mask = DataMemberAttribute.IgnoreMask)]
        [Display(Browsable = false)]
        public string GeneratedInclude { get; set; }

        /// <inheritdoc/>
        public abstract void SaveGeneratedAsset();
    }
}