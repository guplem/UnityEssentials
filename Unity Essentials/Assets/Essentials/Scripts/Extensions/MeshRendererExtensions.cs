namespace UnityEngine
{
    public static class MeshRendererExtensions
    {
        /// <summary>
        /// Returns the first instantiated Material assigned to the renderer.
        /// <para>Any changes to this material will duplicate the material and will not affect all elements linked to the original.</para>
        /// <para>Will duplicate the material.</para>
        /// </summary>
        /// <param name="meshRenderer"></param>
        /// <returns>Returns the first instantiated Material assigned to the renderer.</returns>
        public static Material materialOfObject(this MeshRenderer meshRenderer)
        {
            return meshRenderer.material;
        }
        
        /// <summary>
        /// Returns all the instantiated materials of this object.
        /// <para>Any changes to this materials will duplicate the materials and will not affect all elements linked to the originals.</para>
        /// <para>Will duplicate the materials.</para>
        /// </summary>
        /// <param name="meshRenderer"></param>
        /// <returns></returns>
        public static Material[] materialsOfObject(this MeshRenderer meshRenderer)
        {
            return meshRenderer.materials;
        }
        
        /// <summary>
        /// The shared material of this object.
        /// <para>Any changes to this material will affect all elements linked to the original.</para>
        /// <para>Will modify the *asset*.</para>
        /// </summary>
        /// <param name="meshRenderer"></param>
        /// <returns>The shared material of this object.</returns>
        public static Material materialAsset(this MeshRenderer meshRenderer)
        {
            return meshRenderer.sharedMaterial;
        }
        
        /// <summary>
        /// The shared materials of this object.
        /// <para>Any changes to this materials will affect all elements linked to the originals.</para>
        /// <para>Will modify the *assets*.</para>
        /// </summary>
        /// <param name="meshRenderer"></param>
        /// <returns>The shared materials of this object.</returns>
        public static Material[] materialsAsset(this MeshRenderer meshRenderer)
        {
            return meshRenderer.sharedMaterials;
        }
        
    }
}

