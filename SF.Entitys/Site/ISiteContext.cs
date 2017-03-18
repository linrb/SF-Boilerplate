﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Entitys
{
    /// <summary>
    /// read only immutable version of ISiteSettings
    /// </summary>
    public interface ISiteContext
    {
        // from ISiteInfo but without setters
        long Id { get; }
        string AliasId { get; }
        string SiteName { get; }
        string PreferredHostName { get; }
        string SiteFolderName { get; }
        bool IsServerAdminSite { get; }

        // from ISiteSettings but without setters
        bool UseLdapAuth { get; }
        bool AllowDbFallbackWithLdap { get; }
        bool EmailLdapDbFallback { get; }
        bool AutoCreateLdapUserOnFirstLogin { get; }
        string LdapServer { get; }
        string LdapDomain { get; }
        int LdapPort { get; }
        string LdapRootDN { get; }
        string LdapUserDNKey { get; }
        bool DisableDbAuth { get; }
        bool EnabledAuthorization { get; }
        bool RequireConfirmedEmail { get; } 
        bool RequireConfirmedPhone { get; }
        bool RequireApprovalBeforeLogin { get; } 
        string AccountApprovalEmailCsv { get; }
        int MaxInvalidPasswordAttempts { get; }
        int MinRequiredPasswordLength { get; } 
        bool UseEmailForLogin { get; }
        bool RequiresQuestionAndAnswer { get; }
        bool ReallyDeleteUsers { get; }
        bool AllowNewRegistration { get; }
        bool AllowPersistentLogin { get; }
        bool CaptchaOnLogin { get; }
        bool CaptchaOnRegistration { get; }
        string RecaptchaPrivateKey { get; }
        string RecaptchaPublicKey { get; }

        //company info
        string CompanyCountry { get; }
        string CompanyFax { get; }
        string CompanyLocality { get; }
        string CompanyName { get; }
        string CompanyPhone { get; }
        string CompanyPostalCode { get; }
        string CompanyPublicEmail { get; }
        string CompanyRegion { get; }
        string CompanyStreetAddress { get; }
        string CompanyStreetAddress2 { get; }
        string LoginInfoBottom { get;  }
        string LoginInfoTop { get; }
        string RegistrationAgreement { get; }
        string RegistrationPreamble { get; }
        string TimeZoneId { get; }
        bool SiteIsClosed { get; }
        string SiteIsClosedMessage { get; }
        string DefaultEmailFromAddress { get; }
        string DefaultEmailFromAlias { get; }
        string SmtpPassword { get; }
        int SmtpPort { get; }
        string SmtpPreferredEncoding { get; }
        bool SmtpRequiresAuth { get; }
        string SmtpServer { get; }
        string SmtpUser { get; }
        bool SmtpUseSsl { get; }
        string DkimPublicKey { get; }
        string DkimPrivateKey { get; } 
        string DkimDomain { get; }
        string DkimSelector { get; }
        bool SignEmailWithDkim { get; }
        string SmsClientId { get; }
        string SmsSecureToken { get; } 
        string SmsFrom { get; }
        string PrivacyPolicy { get; }
        string Theme { get; }
        string GoogleAnalyticsProfileId { get; }

        //social login stuff
        string FacebookAppId { get; }
        string FacebookAppSecret { get; }
        string MicrosoftClientId { get; }
        string MicrosoftClientSecret { get; }
        string GoogleClientId { get; }
        string GoogleClientSecret { get; }
        string TwitterConsumerKey { get; }
        string TwitterConsumerSecret { get; }
        string OidConnectAppId { get;  }
        string OidConnectAppSecret { get; } 
        string AddThisDotComUsername { get; }
        //bool IsDataProtected { get; }
        DateTime CreatedUtc { get; }

    }
}
