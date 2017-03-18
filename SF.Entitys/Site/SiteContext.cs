﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Entitys
{
    public class SiteContext : ISiteContext
    {
        public SiteContext(ISiteSettings siteSettings)
        {
            site = siteSettings;
        }

        private readonly ISiteSettings site;

        public long Id
        {
            get { return site.Id; }
        }
        public string AliasId
        {
            get { return site.AliasId; }
        }

        public string SiteName
        {
            get { return site.SiteName; }
        }

        public string SiteFolderName
        {
            get { return site.SiteFolderName; }
        }


        public string PreferredHostName
        {
            get { return site.PreferredHostName; }
        }

        public bool IsServerAdminSite
        {
            get { return site.IsServerAdminSite; }
        }

        public string Theme
        {
            get { return site.Theme; }
        }

        public bool AllowNewRegistration
        {
            get { return site.AllowNewRegistration; }
        }
        public bool RequireConfirmedEmail
        {
            get { return site.RequireConfirmedEmail; }
        }
        public bool RequireConfirmedPhone
        {
            get { return site.RequireConfirmedPhone; }
        }

        public bool UseLdapAuth
        {
            get { return site.UseLdapAuth; }
        }
        public bool AllowDbFallbackWithLdap
        {
            get { return site.AllowDbFallbackWithLdap; }
        }
        public bool EmailLdapDbFallback
        {
            get { return site.EmailLdapDbFallback; }
        }
        public bool AutoCreateLdapUserOnFirstLogin
        {
            get { return site.AutoCreateLdapUserOnFirstLogin; }
        }

        public string LdapServer
        {
            get { return site.LdapServer; }
        }

        public string LdapDomain
        {
            get { return site.LdapDomain; }
        }

        public int LdapPort
        {
            get { return site.LdapPort; }
        }

        public string LdapRootDN
        {
            get { return site.LdapRootDN; }
        }

        public string LdapUserDNKey
        {
            get { return site.LdapUserDNKey; }
        }

        public bool ReallyDeleteUsers
        {
            get { return site.ReallyDeleteUsers; }
        }
        public bool UseEmailForLogin
        {
            get { return site.UseEmailForLogin; }
        }
        public bool DisableDbAuth
        {
            get { return site.DisableDbAuth; }
        }
        public bool EnabledAuthorization
        {
            get { return site.EnabledAuthorization; }
        }
      
        public bool RequiresQuestionAndAnswer
        {
            get { return site.RequiresQuestionAndAnswer; }
        }
        public bool RequireApprovalBeforeLogin
        {
            get { return site.RequireApprovalBeforeLogin; }
        }

        public string AccountApprovalEmailCsv
        {
            get { return site.AccountApprovalEmailCsv; }
        }

        public int MaxInvalidPasswordAttempts
        {
            get { return site.MaxInvalidPasswordAttempts; }
        }

        public int MinRequiredPasswordLength
        {
            get { return site.MinRequiredPasswordLength; }
        }

        public bool AllowPersistentLogin
        {
            get { return site.AllowPersistentLogin; }
        }
        public bool CaptchaOnRegistration
        {
            get { return site.CaptchaOnRegistration; }
        }
        public bool CaptchaOnLogin
        {
            get { return site.CaptchaOnLogin; }
        }

        public string RecaptchaPrivateKey
        {
            get { return site.RecaptchaPrivateKey; }
        }

        public string RecaptchaPublicKey
        {
            get { return site.RecaptchaPublicKey; }
        }

        public string FacebookAppId
        {
            get { return site.FacebookAppId; }
        }

        public string FacebookAppSecret
        {
            get { return site.FacebookAppSecret; }
        }

        public string MicrosoftClientId
        {
            get { return site.MicrosoftClientId; }
        }

        public string MicrosoftClientSecret
        {
            get { return site.MicrosoftClientSecret; }
        }

        public string GoogleClientId
        {
            get { return site.GoogleClientId; }
        }

        public string GoogleClientSecret
        {
            get { return site.GoogleClientSecret; }
        }

        public string TwitterConsumerKey
        {
            get { return site.TwitterConsumerKey; }
        }

        public string TwitterConsumerSecret
        {
            get { return site.TwitterConsumerSecret; }
        }

        public string OidConnectAppId
        {
            get { return site.OidConnectAppId; }
        }

        public string OidConnectAppSecret
        {
            get { return site.OidConnectAppSecret; }
        }

        public string AddThisDotComUsername
        {
            get { return site.AddThisDotComUsername; }
        }

        public string TimeZoneId
        {
            get { return site.TimeZoneId; }
        }

        public string CompanyName
        {
            get { return site.CompanyName; }
        }

        public string CompanyStreetAddress
        {
            get { return site.CompanyStreetAddress; }
        }

        public string CompanyStreetAddress2
        {
            get { return site.CompanyStreetAddress2; }
        }

        public string CompanyLocality
        {
            get { return site.CompanyLocality; }
        }

        public string CompanyRegion
        {
            get { return site.CompanyRegion; }
        }

        public string CompanyPostalCode
        {
            get { return site.CompanyPostalCode; }
        }

        public string CompanyCountry
        {
            get { return site.CompanyCountry; }
        }

        public string CompanyPhone
        {
            get { return site.CompanyPhone; }
        }

        public string CompanyFax
        {
            get { return site.CompanyFax; }
        }
        public string CompanyPublicEmail
        {
            get { return site.CompanyPublicEmail; }
        }

        public string DefaultEmailFromAddress
        {
            get { return site.DefaultEmailFromAddress; }
        }

        public string DefaultEmailFromAlias
        {
            get { return site.DefaultEmailFromAlias; }
        }

        public string SmtpUser
        {
            get { return site.SmtpUser; }
        }

        public string SmtpPassword
        {
            get { return site.SmtpPassword; }
        }
        public int SmtpPort
        {
            get { return site.SmtpPort; }
        }

        public string SmtpPreferredEncoding
        {
            get { return site.SmtpPreferredEncoding; }
        }

        public string SmtpServer
        {
            get { return site.SmtpServer; }
        }

        public bool SmtpRequiresAuth
        {
            get { return site.SmtpRequiresAuth; }
        }
        public bool SmtpUseSsl
        {
            get { return site.SmtpUseSsl; }
        }

        public string DkimPublicKey
        {
            get { return site.DkimPublicKey; }
        }

        public string DkimPrivateKey
        {
            get { return site.DkimPrivateKey; }
        }

        public string DkimDomain
        {
            get { return site.DkimDomain; }
        }

        public string DkimSelector
        {
            get { return site.DkimSelector; }
        }

        public bool SignEmailWithDkim
        {
            get { return site.SignEmailWithDkim; }
        }

        public string SmsClientId
        {
            get { return site.SmsClientId; }
        }

        public string SmsSecureToken
        {
            get { return site.SmsSecureToken; }
        }

        public string SmsFrom
        {
            get { return site.SmsFrom; }
        }

        public string GoogleAnalyticsProfileId
        {
            get { return site.GoogleAnalyticsProfileId; }
        }

        public string RegistrationAgreement
        {
            get { return site.RegistrationAgreement; }
        }

        public string RegistrationPreamble
        {
            get { return site.RegistrationPreamble; }
        }

        public string LoginInfoTop
        {
            get { return site.LoginInfoTop; }
        }

        public string LoginInfoBottom
        {
            get { return site.LoginInfoBottom; }
        }

        public bool SiteIsClosed
        {
            get { return site.SiteIsClosed; }
        }

        public string SiteIsClosedMessage
        {
            get { return site.SiteIsClosedMessage; }
        }

        public string PrivacyPolicy
        {
            get { return site.PrivacyPolicy; }
        }

        //public bool IsDataProtected { get; set; } = false;

        public DateTime CreatedUtc
        {
            get { return site.CreatedUtc; }
        }

    }
}
