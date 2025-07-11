using Kinde.Api.Enums;

namespace Kinde.Api.Models.Configuration
{
    /// <summary>
    /// Options for generating a profile URL
    /// </summary>
    public class GenerateProfileUrlOptions
    {
        /// <summary>
        /// The domain of the Kinde instance
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// URL to redirect to after completing the profile flow
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Sub-navigation section to display
        /// </summary>
        public PortalPage SubNav { get; set; } = PortalPage.Profile;
    }
} 