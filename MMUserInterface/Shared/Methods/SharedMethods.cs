namespace MMUserInterface.Shared.Methods;

public static class SharedMethods
{
    public static byte[] GetUTF8Bytes(string bytesFromString) =>
        Encoding.UTF8.GetBytes(bytesFromString);

    public static string GetBase64String(byte[] fileBytes) =>
        Convert.ToBase64String(fileBytes);
}
