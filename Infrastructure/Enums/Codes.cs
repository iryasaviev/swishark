namespace Infrastructure.Enums
{
    public class Codes
    {
        /// <summary>
        /// Номера ошибок.
        /// </summary>
        public enum States
        {
            /// <summary>
            /// Успешно.
            /// </summary>
            Success = 200,



            /// <summary>
            /// Неприемлемая ошибка клиента, необходима длина.
            /// </summary>
            ErrorValidationLimitMin = 4611,

            /// <summary>
            /// Неприемлемая ошибка клиента, поля запроса слишком большое.
            /// </summary>
            ErrorValidationLimitMax = 4631,

            /// <summary>
            /// Неприемлемая ошибка клиента, неподдерживаемый тип данных - 03.
            /// </summary>
            ErrorValidationEmail = 461503,

            /// <summary>
            /// Неприемлемая ошибка клиента, неподдерживаемый тип данных - 04.
            /// </summary>
            ErrorValidationPassword = 461504,



            /// <summary>
            /// Неприемлемая ошибка клиента, не найдено.
            /// </summary>
            ErrorAccountDoesNotExist = 4604,

            /// <summary>
            /// Неприемлемая ошибка клиента, конфликт.
            /// </summary>
            ErrorAccountEmailIsBusy = 4609,

            /// <summary>
            /// Неверный пароль.
            /// </summary>
            ErrorAccountIncorrectPassword = 4616
        }
    }
}