using System;

namespace com.fourcatsgames.DI
{
	public class DIRegistration
	{
		public delegate object RegistrationFactory(DIContainer container);
		public RegistrationFactory Factory { get; set; }
		public bool IsSingle { get; set; }
		public object Instance { get; set; }
	}
}
