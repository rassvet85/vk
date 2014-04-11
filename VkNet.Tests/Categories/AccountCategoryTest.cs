﻿using System;
using Moq;
using NUnit.Framework;
using VkNet.Categories;
using VkNet.Enums;
using VkNet.Exception;
using VkNet.Utils;

namespace VkNet.Tests.Categories
{
	[TestFixture]
	public class AccountCategoryTest
	{
		private AccountCategory GetMockedAccountCategory(string url, string json, string version = "5.9")
		{
			return GetMockedAccountCategoryAndMockOfBrowser(url, json, version).Item1;
		}

		private Tuple<AccountCategory, Mock<IBrowser>> GetMockedAccountCategoryAndMockOfBrowser(string url, string json, string version = "5.9")
		{
			var mock = new Mock<IBrowser>(MockBehavior.Strict);
			mock.Setup(m => m.GetJson(url)).Returns(json);
			return new Tuple<AccountCategory, Mock<IBrowser>>(
				new AccountCategory(new VkApi { AccessToken = "token", Browser = mock.Object, ApiVersion = version })
				, mock);
		}


		#region SetNameInMenu

		[Test]
		[ExpectedException(typeof(AccessTokenInvalidException))]
		public void SetNameInMenu_AccessTokenInvalid_ThrowAccessTokenInvalidException()
		{
			var account = new AccountCategory(new VkApi());
			account.SetNameInMenu("name");
		}

		[Test]
		public void SetNameInMenu_EmptyName_ThrowArgumentNullException()
		{
			var account = new AccountCategory(new VkApi { AccessToken = "token", Browser = null });

			// ReSharper disable AssignNullToNotNullAttribute
			Assert.That(() => account.SetNameInMenu(null), Throws.InstanceOf<ArgumentNullException>());
			Assert.That(() => account.SetNameInMenu(string.Empty), Throws.InstanceOf<ArgumentNullException>());
			// ReSharper restore AssignNullToNotNullAttribute
		}


		[Test]
		public void SetNameInMenu_SetsCorrectly_ReturnTrue()
		{
			const string url = "https://api.vk.com/method/account.setNameInMenu?name=example&access_token=token";
			const string json = @"{ 'response': 1 }";
			var account = GetMockedAccountCategory(url, json);

			Assert.That(account.SetNameInMenu("example"), Is.True);
		}

		[Test]
		public void SetNameInMenu_NotSets_ReturnFalse()
		{
			const string url = "https://api.vk.com/method/account.setNameInMenu?name=example&access_token=token";
			const string json = @"{ 'response': 0 }";
			var account = GetMockedAccountCategory(url, json);

			Assert.That(account.SetNameInMenu("example"), Is.False);
		}

		#endregion

		#region SetOnline

		[Test]
		[ExpectedException(typeof(AccessTokenInvalidException))]
		public void SetOnline_AccessTokenInvalid_ThrowAccessTokenInvalidException()
		{
			var account = new AccountCategory(new VkApi());
			account.SetOnline();
		}

		[Test]
		public void SetOnline_SetsCorrectly_ReturnTrue()
		{
			const string url = "https://api.vk.com/method/account.setOnline?access_token=token";
			const string json = @"{ 'response': 1 }";
			var account = GetMockedAccountCategory(url, json);

			Assert.That(account.SetOnline(), Is.True);
		}

		[Test]
		public void SetOnline_NotSets_ReturnFalse()
		{
			const string url = "https://api.vk.com/method/account.setOnline?access_token=token";
			const string json = @"{ 'response': 0 }";
			var account = GetMockedAccountCategory(url, json);

			Assert.That(account.SetOnline(), Is.False);
		}

		[Test]
		public void SetOnline_WithVoipParameter()
		{
			const string url = "https://api.vk.com/method/account.setOnline?voip=1&access_token=token";
			const string json = @"{ 'response': 1 }";
			var account = GetMockedAccountCategoryAndMockOfBrowser(url, json);

			account.Item1.SetOnline(true);
			account.Item2.VerifyAll();
		}

		#endregion

		#region SetOffline

		[Test]
		[ExpectedException(typeof(AccessTokenInvalidException))]
		public void SetOffline_AccessTokenInvalid_ThrowAccessTokenInvalidException()
		{
			var account = new AccountCategory(new VkApi());
			account.SetOffline();
		}

		[Test]
		public void SetOffline_SetsCorrectly_ReturnTrue()
		{
			const string url = "https://api.vk.com/method/account.setOffline?access_token=token";
			const string json = @"{ 'response': 1 }";
			var account = GetMockedAccountCategory(url, json);

			Assert.That(account.SetOffline(), Is.True);
		}

		[Test]
		public void SetOffline_NotSets_ReturnFalse()
		{
			const string url = "https://api.vk.com/method/account.setOffline?access_token=token";
			const string json = @"{ 'response': 0 }";
			var account = GetMockedAccountCategory(url, json);

			Assert.That(account.SetOffline(), Is.False);
		}

		#endregion

		#region RegisterDevice

		[Test]
		[ExpectedException(typeof(AccessTokenInvalidException))]
		public void RegisterDevice_AccessTokenInvalid_ThrowAccessTokenInvalidException()
		{
			var account = new AccountCategory(new VkApi());
			// ReSharper disable AssignNullToNotNullAttribute
			account.RegisterDevice("tokenVal", null, null);
			// ReSharper restore AssignNullToNotNullAttribute
		}

		[Test]
		public void RegisterDevice_NullOrEmptyToken_ThrowArgumentNullException()
		{
			var mock = new Mock<IBrowser>(MockBehavior.Strict);
			var account = new AccountCategory(new VkApi { AccessToken = "token", Browser = null });

			// ReSharper disable AssignNullToNotNullAttribute
			Assert.That(() => account.RegisterDevice(null, "example", "example"), Throws.InstanceOf<ArgumentNullException>());
			Assert.That(() => account.RegisterDevice(string.Empty, "example", "example"), Throws.InstanceOf<ArgumentNullException>());
			// ReSharper restore AssignNullToNotNullAttribute
		}


		[Test]
		public void RegisterDevice_SetsCorrectly_ReturnTrue()
		{
			const string url = "https://api.vk.com/method/account.registerDevice?token=tokenVal&device_model=deviceModelVal&system_version=systemVersionVal&access_token=token";
			const string json = @"{ 'response': 1 }";
			var account = GetMockedAccountCategory(url, json);

			Assert.That(account.RegisterDevice("tokenVal", "deviceModelVal", "systemVersionVal"), Is.True);
		}

		[Test]
		public void RegisterDevice_SetsCorrectly_ReturnFalse()
		{
			const string url = "https://api.vk.com/method/account.registerDevice?token=tokenVal&device_model=deviceModelVal&system_version=systemVersionVal&access_token=token";
			const string json = @"{ 'response': 0 }";
			var account = GetMockedAccountCategory(url, json);

			Assert.That(account.RegisterDevice("tokenVal", "deviceModelVal", "systemVersionVal"), Is.False);
		}

		[Test]
		public void RegisterDevice_ParametersAreEqualsToNullOrEmptyExceptToken_NotThrowsException()
		{
			const string url = "https://api.vk.com/method/account.registerDevice?token=tokenVal&access_token=token";
			const string json = @"{ 'response': 1 }";
			var account = GetMockedAccountCategory(url, json);

			Assert.That(() => account.RegisterDevice("tokenVal", null, null, null, null), Throws.Nothing);
			Assert.That(() => account.RegisterDevice("tokenVal", string.Empty, string.Empty, null, null), Throws.Nothing);
		}

		[Test]
		public void RegisterDevice_ExplicitNoTextAndSomeSubscribes_ParametersAddsToUrlCorrectly()
		{
			const string url = "https://api.vk.com/method/account.registerDevice?token=tokenVal&device_model=deviceModelVal&system_version=systemVersionVal&no_text=1&subscribe=msg,friend,call&access_token=token";
			const string json = @"{ 'response': 1 }";
			var account = GetMockedAccountCategoryAndMockOfBrowser(url, json);

			account.Item1.RegisterDevice("tokenVal", "deviceModelVal", "systemVersionVal", true,
									SubscribeFilter.Message | SubscribeFilter.Friend | SubscribeFilter.Call);
			account.Item2.VerifyAll();
		}

		[Test]
		public void RegisterDevice_ExplicitNoTextAndAllSubscribes_ParametersAddsToUrlCorrectly()
		{
			const string url = "https://api.vk.com/method/account.registerDevice?token=tokenVal&device_model=deviceModelVal&system_version=systemVersionVal&no_text=1&subscribe=msg,friend,call,reply,mention,group,like&access_token=token";
			const string json = @"{ 'response': 1 }";
			var account = GetMockedAccountCategoryAndMockOfBrowser(url, json);

			account.Item1.RegisterDevice("tokenVal", "deviceModelVal", "systemVersionVal", true, SubscribeFilter.All);
			account.Item2.VerifyAll();
		}

		#endregion

		#region UnregisterDevice

		[Test]
		[ExpectedException(typeof(AccessTokenInvalidException))]
		public void UnregisterDevice_AccessTokenInvalid_ThrowAccessTokenInvalidException()
		{
			var account = new AccountCategory(new VkApi());
			// ReSharper disable AssignNullToNotNullAttribute
			account.UnregisterDevice("tokenVal");
			// ReSharper restore AssignNullToNotNullAttribute
		}

		[Test]
		public void UnregisterDevice_NullOrEmptyToken_ThrowArgumentNullException()
		{
			var mock = new Mock<IBrowser>(MockBehavior.Strict);
			var account = new AccountCategory(new VkApi { AccessToken = "token", Browser = null });

			// ReSharper disable AssignNullToNotNullAttribute
			Assert.That(() => account.UnregisterDevice(null), Throws.InstanceOf<ArgumentNullException>());
			Assert.That(() => account.UnregisterDevice(string.Empty), Throws.InstanceOf<ArgumentNullException>());
			// ReSharper restore AssignNullToNotNullAttribute
		}


		[Test]
		public void UnregisterDevice_SetsCorrectly_ReturnTrue()
		{
			const string url = "https://api.vk.com/method/account.unregisterDevice?token=tokenVal&access_token=token";
			const string json = @"{ 'response': 1 }";
			var account = GetMockedAccountCategory(url, json);

			Assert.That(account.UnregisterDevice("tokenVal"), Is.True);
		}

		[Test]
		public void UnregisterDevice_SetsCorrectly_ReturnFalse()
		{
			const string url = "https://api.vk.com/method/account.unregisterDevice?token=tokenVal&access_token=token";
			const string json = @"{ 'response': 0 }";
			var account = GetMockedAccountCategory(url, json);

			Assert.That(account.UnregisterDevice("tokenVal"), Is.False);


		}

		#endregion

		#region SetSilenceMode

		[Test]
		[ExpectedException(typeof(AccessTokenInvalidException))]
		public void SetSilenceMode_AccessTokenInvalid_ThrowAccessTokenInvalidException()
		{
			var account = new AccountCategory(new VkApi());
			// ReSharper disable AssignNullToNotNullAttribute
			account.SetSilenceMode("tokenVal");
			// ReSharper restore AssignNullToNotNullAttribute
		}

		[Test]
		public void SetSilenceMode_NullOrEmptyToken_ThrowArgumentNullException()
		{
			var mock = new Mock<IBrowser>(MockBehavior.Strict);
			var account = new AccountCategory(new VkApi { AccessToken = "token", Browser = null });

			// ReSharper disable AssignNullToNotNullAttribute
			Assert.That(() => account.SetSilenceMode(null), Throws.InstanceOf<ArgumentNullException>());
			Assert.That(() => account.SetSilenceMode(string.Empty), Throws.InstanceOf<ArgumentNullException>());
			// ReSharper restore AssignNullToNotNullAttribute
		}


		[Test]
		public void SetSilenceMode_SetsCorrectly_ReturnTrue()
		{
			const string url = "https://api.vk.com/method/account.setSilenceMode?token=tokenVal&access_token=token";
			const string json = @"{ 'response': 1 }";
			var account = GetMockedAccountCategory(url, json);

			Assert.That(account.SetSilenceMode("tokenVal"), Is.True);
		}

		[Test]
		public void SetSilenceMode_SetsCorrectly_ReturnFalse()
		{
			const string url = "https://api.vk.com/method/account.setSilenceMode?token=tokenVal&access_token=token";
			const string json = @"{ 'response': 0 }";
			var account = GetMockedAccountCategory(url, json);

			Assert.That(account.SetSilenceMode("tokenVal"), Is.False);
		}

		[Test]
		public void SetSilenceMode_AllParametersAddsToUrlCorrectly()
		{
			{
				const string url = "https://api.vk.com/method/account.setSilenceMode?token=tokenVal&time=10&chat_id=15&user_id=42&sound=1&access_token=token";
				const string json = @"{ 'response': 0 }";
				var account = GetMockedAccountCategoryAndMockOfBrowser(url, json);

				account.Item1.SetSilenceMode("tokenVal", 10, 15, 42, true);
				account.Item2.VerifyAll();
			}

			{
				const string url = "https://api.vk.com/method/account.setSilenceMode?token=tokenVal&time=-1&user_id=10&sound=0&access_token=token";
				const string json = @"{ 'response': 0 }";
				var account = GetMockedAccountCategoryAndMockOfBrowser(url, json);

				account.Item1.SetSilenceMode("tokenVal", -1, userID: 10, sound:false);
				account.Item2.VerifyAll();
			}


		}

		#endregion



	}
}