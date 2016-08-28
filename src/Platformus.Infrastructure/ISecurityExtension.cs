using Microsoft.AspNetCore.Authorization;

namespace Platformus.Security
{
  public interface ISecurityExtension
  {
     AuthorizationPolicyBuilder ConfigurePolicy(AuthorizationPolicyBuilder builder);
  }
}
