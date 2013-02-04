using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ClothesShop
{
	public enum Language
	{
		BG,
		EN
	}

	public static class TranslationProvider
	{

		public static Language CurrentLanguage
		{
			get
			{
				return HttpContext.Current.Session["Language"] == null ? Language.EN : (Language)HttpContext.Current.Session["Language"];
			}
		}

		public static string HomePage
		{
			get
			{
				return CurrentLanguage == Language.BG ? "Начална Страница" : "Home Page";
			}
		}

		public static string Products
		{
			get
			{
				return CurrentLanguage == Language.BG ? "Продукти" : "Products";
			}
		}

		public static string Product
		{
			get
			{
				return CurrentLanguage == Language.BG ? "Продукт" : "Product";
			}
		}

		public static string SelectedCategory
		{
			get
			{
				return CurrentLanguage == Language.BG ? "Избрана Категория" : "Selected Category";
			}
		}

		public static string SignIn
		{
			get
			{
				return CurrentLanguage == Language.BG ? "ВХОД" : "SIGN IN";
			}
		}

		public static string Register
		{
			get
			{
				return CurrentLanguage == Language.BG ? "ЗАПИС" : "SIGN UP";
			}
		}

		public static string SignOut
		{
			get
			{
				return CurrentLanguage == Language.BG ? "Изход" : "Log Off";
			}
		}

		public static string Welcome
		{
			get
			{
				return CurrentLanguage == Language.BG ? "Добре дошли " : "Welcome ";
			}
		}

		public static string Admin
		{
			get
			{
				return CurrentLanguage == Language.BG ? "АДМИН" : "ADMIN";
			}
		}

		public static string Basket
		{
			get
			{
				return CurrentLanguage == Language.BG ? "КОШ" : "BASKET";
			}
		}

		public static string About
		{
			get
			{
				return CurrentLanguage == Language.BG ? "За нас" : "About Us";
			}
		}

		public static string AboutDescription
		{
			get
			{
				return CurrentLanguage == Language.BG ? "Това е прост магазин." : "This is a simple shop.";
			}
		}

		public static string AllRightsReserved
		{
			get
			{
				return CurrentLanguage == Language.BG ? 
					"Всички Права Запазени (Р) (За допълнителна информация, моля посетете страница {0})" : 
					"All Rights Reserved (R) (For additional info see the {0} page)";
			}
		}

		public static string Username
		{
			get
			{
				return CurrentLanguage == Language.BG ? "Потребителско име" : "User Name";
			}
		}

		public static string Password
		{
			get
			{
				return CurrentLanguage == Language.BG ? "Парола" : "Password";
			}
		}

		public static string ConfirmPassword
		{
			get
			{
				return CurrentLanguage == Language.BG ? "Потвърди Парола" : "Confirm Password";
			}
		}

		public static string AccountInfo
		{
			get
			{
				return CurrentLanguage == Language.BG ? "Потребителска информация" : "Account Information";
			}
		}

		public static string LogOnPls
		{
			get
			{
				return CurrentLanguage == Language.BG ? "Моля въведете вашето име и парола. Ако нямата такива, поля посетете <a href=\"/Account/Register/\">{0}</a>" :
					"Please enter your user name and password. <a href=\"/Account/Register/\">{0}</a> if you don't have an account.";
			}
		}

		public static string LoginFailed
		{
			get
			{
				return CurrentLanguage == Language.BG ?
					"Влизането бе неуспешно. Моля поправете грешките и опитайте пак." :
					"Login was unsuccessful. Please correct the errors and try again.";
			}
		}

		public static string CreateAccountFailed
		{
			get
			{
				return CurrentLanguage == Language.BG ?
					"Създаването неуспешно. Моля поправете грешките и опитайте пак." :
					"Error creating account. Please correct the errors and try again.";
			}
		}

		public static string CreateNewAccount
		{
			get
			{
				return CurrentLanguage == Language.BG ?
					"Създаване на Нов Потребител" :
					"Create а New Account";
			}
		}

		public static string CreateNewAccountInfo
		{
			get
			{
				return CurrentLanguage == Language.BG ?
					"<p>Използвайте формата отдолу за да си направите профил.</p>"+	
					"<p>Паролата трябва да е минимум от " + Membership.MinRequiredPasswordLength + " знака в дължина.</p>" :

					"<p>Use the form below to create a new account.</p>"+	
					"<p>Passwords are required to be a minimum of " + Membership.MinRequiredPasswordLength + " characters in length.</p>";
			}
		}

		public static string Wight
		{
			get
			{
				return CurrentLanguage == Language.BG ? "Тежина:" : "Weight:";
			}
		}

		public static string SpecialOffer
		{
			get
			{
				return CurrentLanguage == Language.BG ? "СПЕЦИАЛНА ОФЕРТА" : "SPECIAL OFFER";
			}
		}

		public static string Price
		{
			get
			{
				return CurrentLanguage == Language.BG ? "Цена:" : "Price:";
			}
		}

		public static string CatNo
		{
			get
			{
				return CurrentLanguage == Language.BG ? "Каталожен Номер:" : "Catalogue Number:";
			}
		}

		public static string Category
		{
			get
			{
				return CurrentLanguage == Language.BG ? "Категория" : "Category";
			}
		}

		public static string HasAvailable
		{
			get
			{
				return CurrentLanguage == Language.BG ? "ИМА В НАЛИЧНОСТ " : "PRODUCT IS AVAILABLE ";
			}
		}

		public static string ProductDetails
		{
			get
			{
				return CurrentLanguage == Language.BG ? "Детайли за Продукт" : "Product Details";
			}
		}


		public static object ProductsInBasket
		{
			get
			{
				return CurrentLanguage == Language.BG ? "Продукти в Кошница" : "Products in Basket";
			}
		}

		public static object Size
		{
			get
			{
				return CurrentLanguage == Language.BG ? "Размер" : "Size";
			}
		}

		public static object Quantity
		{
			get
			{
				return CurrentLanguage == Language.BG ? "Количество" : "Quantity";
			}
		}

		public static object TotalPrice
		{
			get
			{
				return CurrentLanguage == Language.BG ? "Обща цена:" : "Total price:";
			}
		}

		public static object BasketEmpty
		{
			get
			{
				return CurrentLanguage == Language.BG ? "Кошницата е празна" : "Basket is empty.";
			}
		}

		public static object Remove
		{
			get
			{
				return CurrentLanguage == Language.BG ? "Премахни" : "Remove";
			}
		}
	}
}