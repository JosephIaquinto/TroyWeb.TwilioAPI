using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using TroyWeb.TwilioAPI.Activities.Properties;
using TroyWeb.TwilioAPI.Wrappers.SMS;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;
using UiPath.Shared.Activities.Utilities;

namespace TroyWeb.TwilioAPI.Activities
{
    [LocalizedDisplayName(nameof(Resources.GetMessage_DisplayName))]
    [LocalizedDescription(nameof(Resources.GetMessage_Description))]
    public class GetMessage : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.GetMessage_MessageSid_DisplayName))]
        [LocalizedDescription(nameof(Resources.GetMessage_MessageSid_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> MessageSid { get; set; }

        [LocalizedDisplayName(nameof(Resources.GetMessage_AccountSid_DisplayName))]
        [LocalizedDescription(nameof(Resources.GetMessage_AccountSid_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> AccountSid { get; set; }

        [LocalizedDisplayName(nameof(Resources.GetMessage_Message_DisplayName))]
        [LocalizedDescription(nameof(Resources.GetMessage_Message_Description))]
        [LocalizedCategory(nameof(Resources.Output_Category))]
        public OutArgument<MessageResource> Message { get; set; }

        #endregion


        #region Constructors

        public GetMessage()
        {
            Constraints.Add(ActivityConstraints.HasParentType<GetMessage, TwilioApiScope>(string.Format(Resources.ValidationScope_Error, Resources.TwilioApiScope_DisplayName)));
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            metadata.AddRequiredArgument(MessageSid, nameof(MessageSid));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            // Object Container: Use objectContainer.Get<T>() to retrieve objects from the scope
            var objectContainer = context.GetFromContext<IObjectContainer>(TwilioApiScope.ParentContainerPropertyTag);

            // Inputs
            var messagesid = MessageSid.Get(context);
            var accountsid = AccountSid.Get(context);

            var message = await MessageWrappers.GetMessageAsync(objectContainer.Get<ITwilioRestClient>(), messagesid, accountsid);

            // Outputs
            return (ctx) => {
                Message.Set(ctx, message);
            };
        }

        #endregion
    }
}

