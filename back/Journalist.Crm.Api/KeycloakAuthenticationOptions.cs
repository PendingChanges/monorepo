namespace Journalist.Crm.Api;

public class KeycloakAuthenticationOptions
{
    public static readonly string Section = "Keycloak";

    public TimeSpan TokenClockSkew { get; set; }
    public bool? VerifyTokenAudience { get; set; }
    public string? SslRequired { get; set; }
    public string? KeycloakUrlRealm { get; set; }
    public string? Resource { get; set; }
}
