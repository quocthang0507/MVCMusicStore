using MvcContrib.TestHelper;
using MVCMusicStore.Test.Fakes;
using System;
using System.Web.Mvc;

namespace MVSMusicStore.Test
{
	class ControllerFactory
	{

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
