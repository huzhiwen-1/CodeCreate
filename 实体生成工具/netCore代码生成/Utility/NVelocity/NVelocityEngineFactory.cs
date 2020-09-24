namespace Utility.NVelocity
{
	/// <summary>
    /// NVelocity引擎工厂.
	/// </summary>
	public class NVelocityEngineFactory
	{
		/// <summary>
        /// 文件模板引擎.
		/// </summary>
		/// <param name="templateDirectory">模板目录</param>
		/// <param name="cacheTemplate">是否缓存模板</param>
		/// <returns></returns>
		public static INVelocityEngine CreateFileEngine(string templateDirectory, bool cacheTemplate)
		{
			return new NVelocityFileEngine(templateDirectory, cacheTemplate);
		}

		/// <summary>
        /// 程序集模板引擎.
		/// </summary>
		/// <param name="assemblyName">程序集名</param>
        /// <param name="cacheTemplate">是否缓存模板</param>
		/// <returns></returns>
		public static INVelocityEngine CreateAssemblyEngine(string assemblyName, bool cacheTemplate)
		{
			return new NVelocityAssemblyEngine(assemblyName, cacheTemplate);
		}

		/// <summary>
		/// 内存模板引擎.
		/// </summary>
        /// <param name="cacheTemplate">是否缓存模板</param>
		/// <returns></returns>
		public static INVelocityEngine CreateMemoryEngine(bool cacheTemplate)
		{
			return new NVelocityMemoryEngine(cacheTemplate);
		}
	}
}
