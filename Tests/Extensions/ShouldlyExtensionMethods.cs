using System;
using System.Reactive;
using Shouldly;

namespace Tests.Extensions
{
	public static class ShouldlyExtensionMethods
	{
		public static void ShouldBeOnNext<T>(this Notification<T> value, T expectedValue)
		{
			value.Kind.ShouldBe(NotificationKind.OnNext);
			value.HasValue.ShouldBeTrue();
			value.Exception.ShouldBeNull();
			value.Value.ShouldBe(expectedValue);
		}

		public static void ShouldBeOnError<T>(this Notification<T> value, Type type)
		{
			value.Kind.ShouldBe(NotificationKind.OnError);
			value.HasValue.ShouldBeFalse();
			value.Exception.ShouldBeOfType(type);
		}
	}
}
