namespace Utility.NVelocity
{
	/// <summary>
    /// NVelocity���湤��.
	/// </summary>
	public class NVelocityEngineFactory
	{
		/// <summary>
        /// �ļ�ģ������.
		/// </summary>
		/// <param name="templateDirectory">ģ��Ŀ¼</param>
		/// <param name="cacheTemplate">�Ƿ񻺴�ģ��</param>
		/// <returns></returns>
		public static INVelocityEngine CreateFileEngine(string templateDirectory, bool cacheTemplate)
		{
			return new NVelocityFileEngine(templateDirectory, cacheTemplate);
		}

		/// <summary>
        /// ����ģ������.
		/// </summary>
		/// <param name="assemblyName">������</param>
        /// <param name="cacheTemplate">�Ƿ񻺴�ģ��</param>
		/// <returns></returns>
		public static INVelocityEngine CreateAssemblyEngine(string assemblyName, bool cacheTemplate)
		{
			return new NVelocityAssemblyEngine(assemblyName, cacheTemplate);
		}

		/// <summary>
		/// �ڴ�ģ������.
		/// </summary>
        /// <param name="cacheTemplate">�Ƿ񻺴�ģ��</param>
		/// <returns></returns>
		public static INVelocityEngine CreateMemoryEngine(bool cacheTemplate)
		{
			return new NVelocityMemoryEngine(cacheTemplate);
		}
	}
}
