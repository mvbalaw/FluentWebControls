using System;

using StructureMap;

namespace FluentWebControls.Tools
{
	public static class IoCUtility
	{
		public static InterfaceType GetInstance<InterfaceType>(Type interfaceType)
		{
			return (InterfaceType)ObjectFactory.GetInstance(interfaceType);
		}

		public static InterfaceType GetInstance<InterfaceType>()
		{
			return ObjectFactory.GetInstance<InterfaceType>();
		}

		public static InterfaceType GetInstance<InterfaceType>(string name)
		{
			return ObjectFactory.GetNamedInstance<InterfaceType>(name);
		}

		public static void Inject<InterfaceType>(InterfaceType concrete)
		{
			ObjectFactory.Inject(concrete);
		}
	}
}