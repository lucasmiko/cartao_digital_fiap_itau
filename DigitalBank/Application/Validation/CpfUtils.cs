public static class CpfUtils
{
    public static bool IsValid(string? cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) return false;

        var digitsOnly = new string(cpf.Where(char.IsDigit).ToArray());
        if (digitsOnly.Length != 11) return false;

        // Rejeita CPFs com todos os dígitos iguais (ex.: 000... / 111...)
        if (digitsOnly.Distinct().Count() == 1) return false;

        var nums = digitsOnly.Select(c => c - '0').ToArray();

        // 1º dígito verificador
        int sum = 0;
        for (int i = 0, weight = 10; i < 9; i++, weight--)
            sum += nums[i] * weight;

        int d1 = sum % 11;
        d1 = d1 < 2 ? 0 : 11 - d1;
        if (nums[9] != d1) return false;

        // 2º dígito verificador
        sum = 0;
        for (int i = 0, weight = 11; i < 10; i++, weight--)
            sum += nums[i] * weight;

        int d2 = sum % 11;
        d2 = d2 < 2 ? 0 : 11 - d2;
        if (nums[10] != d2) return false;

        return true;
    }
}