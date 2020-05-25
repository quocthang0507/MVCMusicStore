using MvcContrib.TestHelper;
using MVCMusicStore.Test.Fakes;
using System;
using System.Web.Mvc;

namespace MVSMusicStore.Test
{
	public class ControllerFactory
	{
		/// <summary>
		/// Khởi tạo một controller dùng trong kiểm thử
		/// </summary>
		/// <typeparam name="TController"></typeparam>
		/// <param name="ctor"></param>
		/// <param name="formValues"></param>
		/// <param name="store"></param>
		/// <param name="user"></param>
		/// <returns></returns>
		public static TController GetWiredUpController<TController>(Func<FakeDataStore, TController> ctor, FormCollection formValues = null, FakeDataStore store = null, FakeUser user = null) where TController : MVCMusicStore.Controllers.ControllerBase
		{
			store = store ?? MusicStoreEntitiesFactory.GetEmpty();
			TController controller = ctor(store);
			TestControllerBuilder _builder = new TestControllerBuilder();
			_builder.HttpContext.User = user ?? new FakeUser();
			_builder.InitializeController(controller);
			if (formValues != null)
			{
				controller.ValueProvider = formValues.ToValueProvider();
			}
			return controller;
		}
	}
}
