namespace FabricaNube.Utils
{
    public static class CodeGenerator
    {
        public static string GenerateCode(string prefix)
        {
            return $"{prefix}-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
        }
    }

}
