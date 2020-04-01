using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using TroyWeb.TwilioAPI.Activities.Properties;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;
using UiPath.Shared.Activities.Utilities;

namespace TroyWeb.TwilioAPI.Activities
{
    [LocalizedDisplayName(nameof(Resources.UpdateAccount_DisplayName))]
    [LocalizedDescription(nameof(Resources.UpdateAccount_Description))]
    public class UpdateAccount : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.UpdateAccount_AccountSid_DisplayName))]
        [LocalizedDescription(nameof(Resources.UpdateAccount_AccountSid_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> AccountSid { get; set; }

        [LocalizedDisplayName(nameof(Resources.UpdateAccount_FriendlyName_DisplayName))]
        [LocalizedDescription(nameof(Resources.UpdateAccount_FriendlyName_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> FriendlyName { get; set; }

        [LocalizedDisplayName(nameof(Resources.UpdateAccount_Status_DisplayName))]
        [LocalizedDescription(nameof(Resources.UpdateAccount_Status_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> Status { get; set; }

        [LocalizedDisplayName(nameof(Resources.UpdateAccount_Account_DisplayName))]
        [LocalizedDescription(nameof(Resources.UpdateAccount_Account_Description))]
        [LocalizedCategory(nameof(Resources.Output_Category))]
        public OutArgument<object> Account { get; set; }

        #endregion


        #region Constructors

        public UpdateAccount()
        {
            Constraints.Add(ActivityConstraints.HasParentType<UpdateAccount, TwilioApiScope>(string.Format(Resources.ValidationScope_Error, Resources.TwilioApiScope_DisplayName)));
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (AccountSid == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(AccountSid)));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            // Object Container: Use objectContainer.Get<T>() to retrieve objects from the scope
            var objectContainer = context.GetFromContext<IObjectContainer>(TwilioApiScope.ParentContainerPropertyTag);

            // Inputs
            var accountsid = AccountSid.Get(context);
            var friendlyname = FriendlyName.Get(context);
            var status = Status.Get(context);
    
            ///////////////////////////
            // Add execution logic HERE
            ///////////////////////////

            // Outputs
            return (ctx) => {
                Account.Set(ctx, null);
            };
        }

        #endregion
    }
}

