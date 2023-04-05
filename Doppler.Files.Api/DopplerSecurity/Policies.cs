namespace Doppler.Files.Api.DopplerSecurity;

public static class Policies
{
    public const string OnlySuperUser = nameof(OnlySuperUser);
    public const string OwnResourceOrSuperUser = nameof(OwnResourceOrSuperUser);
}
