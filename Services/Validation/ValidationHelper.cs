using System;
using System.Text.RegularExpressions;

namespace Services.Validation
{
    public class ValidationHelper
    {
        /// <summary>
        /// Валидация электронной почты.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Email(string value)
        {
            bool result = false;
            string pattern = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,6})+$";

            if (Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
            result = true;

            return result;
        }

        /// <summary>
        /// Валидация пароля.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Password(string value)
        {
            bool result = false;
            string pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$";

            if (Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
            result = true;

            return result;
        }

        /// <summary>
        /// Валидация на минимальное значение.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool LimitMin(string value, int quantity)
        {
            bool result = false;

            if (value.Length >= quantity)
            result = true;

            return result;
        }

        /// <summary>
		/// Проверка на максимальное значение и обрезка с возвратом проверямой строки.
		/// </summary>
		/// <param name="value">Проверяемое значение.</param>
		/// <param name="quantity">Максимальное количество символов.</param>
		/// <returns></returns>
		public Tuple<bool, string> LimitMaxCrop(string value, int quantity)
        {
            (bool, string) result = (true, value);

            if (value != null)
                if (value.Length > quantity)
                    value = value.Substring(0, quantity);
                    result.Item1 = true;

            return Tuple.Create(result.Item1, result.Item2);
        }
    }
}