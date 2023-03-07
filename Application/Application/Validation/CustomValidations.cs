using FluentValidation;

namespace Application.Validation
{
    public static class CustomValidations
    {
        public static IRuleBuilderOptions<T, DateTime> IsPersonAtLeast13YerasOld<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
        {
            return ruleBuilder.Must(x => CheckBirthDate(x)).WithMessage("The person who creates account must be at least 13 years old");
        }

        private static bool CheckBirthDate(DateTime birthDate)
        {
            int MINIMAL_AGE = 13;
            return (DateTime.Now.Year - birthDate.Year) > MINIMAL_AGE;
        }

        public static IRuleBuilderOptions<T, string> IsNipValid<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(x => CheckNip(x)).WithMessage("A Nip must be valid");
        }

        private static bool CheckNip(string nip)
        {
            int sum = 0;
            int[] WEIGHTS = { 6, 5, 7, 2, 3, 4, 5, 6, 7 };
            int MODULO_NUMBER_NIP = 11;
            int CONROL_NUMBER = int.Parse(char.ToString(nip[^1]));
            for (int i = 0; i < nip.Length - 1; i++)
            {
                sum += int.Parse(nip[i].ToString()) * WEIGHTS[i];
            }
            Console.WriteLine(sum);
            Console.WriteLine(CONROL_NUMBER % MODULO_NUMBER_NIP);
            return sum != (CONROL_NUMBER % MODULO_NUMBER_NIP);
        }

        public static IRuleBuilderOptions<T, string> IsWebsiteLinkValid<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(x => Uri.TryCreate(x, UriKind.Absolute, out _)).WithMessage("The you must enter valid URL");
        }
    }
}